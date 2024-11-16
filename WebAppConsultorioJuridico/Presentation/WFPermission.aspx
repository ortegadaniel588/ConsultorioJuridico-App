﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeFile="WFPermission.aspx.cs" Inherits="Presentation.WFPermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="FrmPermission" runat="server">
        <%--Id--%>
        <asp:HiddenField ID="HFPermisoID" runat="server" />
        <%--Nombre--%>
        <asp:Label ID="Label1" runat="server" Text="">Permiso</asp:Label>
        <asp:DropDownList ID="DDLNombrePer" runat="server">
            <asp:ListItem Value="0">Seleccione</asp:ListItem>
            <asp:ListItem Value="CREAR">Crear</asp:ListItem>
            <asp:ListItem Value="ACTUALIZAR">Actualizar</asp:ListItem>
            <asp:ListItem Value="MOSTRAR">Mostrar</asp:ListItem>
            <asp:ListItem Value="ELIMINAR">Eliminar</asp:ListItem>
        </asp:DropDownList>
        <%--Valida que el DropDownList este seleccionado con algun valor--%>
        <asp:RequiredFieldValidator ID="RFVNombrePer" runat="server"
            ControlToValidate="DDLNombrePer"
            InitialValue="0"
            ErrorMessage="Debes seleccionar un Permiso."
            ForeColor="Red">
        </asp:RequiredFieldValidator>
        <br />
        <%--Descripcion--%>
        <asp:Label ID="Label2" runat="server" Text="">Descripcion</asp:Label>
        <asp:TextBox ID="TBDescripcion" runat="server"></asp:TextBox>
        <%--Valida que el TextBox este lleno--%>
        <asp:RequiredFieldValidator ID="RFVDescripcion"
            runat="server"
            ControlToValidate="TBDescripcion"
            ForeColor="Red"
            Display="Dynamic"
            ErrorMessage="Este campo es obligatorio">
        </asp:RequiredFieldValidator>
        <br />
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </form>
    <asp:Panel ID="PanelAdmin" runat="server">
        <%--Lista de Permisos--%>
        <h2>Lista de Permisos</h2>
        <table id="permissionTable" class="display" style="width: 100%">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nombre</th>
                    <th>Descripcion</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </asp:Panel>
    <%--Datatables--%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Permisos--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#permissionTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFPermission.aspx/ListPermissions",// Se invoca el WebMethod Listar Permisos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de permisos del resultado
                    }
                },
                "columns": [
                    { "data": "PermisoID" },
                    { "data": "NamePermiso" },
                    { "data": "Description" },
                    {
                        "data": null,
                        "render": function (row) {
                            return `<button class="edit-btn" data-id="${row.PermisoID}">Editar</button>
                         <button class="delete-btn" data-id="${row.PermisoID}">Eliminar</button>`;
                        }
                    }
                ],
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página",
                    "zeroRecords": "No se encontraron resultados",
                    "info": "Mostrando página _PAGE_ de _PAGES_",
                    "infoEmpty": "No hay registros disponibles",
                    "infoFiltered": "(filtrado de _MAX_ registros totales)",
                    "search": "Buscar:",
                    "paginate": {
                        "first": "Primero",
                        "last": "Último",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                }

            });

            // Editar un permiso rol
            $('#permissionTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#permissionTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                permissionsData(rowData);
            });

            // Eliminar un permiso rol
            $('#permissionTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del permiso rol
                if (confirm("¿Estás seguro de que deseas eliminar este permiso?")) {
                    deletePermissions(id);// Invoca a la función para eliminar el permiso
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function permissionsData(rowData) {
            $('#<%= HFPermisoID.ClientID %>').val(rowData.PermisoID);
            $('#<%= DDLNombrePer.ClientID %>').val(rowData.NamePermiso);
            $('#<%= TBDescripcion.ClientID %>').val(rowData.Description);
        }

        // Función para eliminar un producto
        function deletePermissions(id) {
            $.ajax({
                type: "POST",
                url: "WFPermission.aspx/DeletePermission",// Se invoca el WebMethod Eliminar un Permiso
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#permissionTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Permiso eliminado exitosamente.");
                    //alert(JSON.stringify(response));
                },
                error: function () {
                    alert("Error al eliminar el permiso.");
                }
            });
        }
    </script>
</asp:Content>
