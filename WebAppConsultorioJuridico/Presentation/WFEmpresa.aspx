
<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEmpresa.aspx.cs" Inherits="Presentation.WFEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <asp:HiddenField ID="EmpresaID" runat="server" />
        <br />
        <%--Número nit--%>
        <asp:Label ID="Label1" runat="server" Text="Ingresa el número nit"></asp:Label>
        <asp:TextBox ID="TBNumeronit" runat="server" />
        <br />
        <%--Nombre--%>
        <asp:Label ID="Label2" runat="server" Text="Ingresa el nombre"></asp:Label>
        
        <asp:TextBox ID="TBNombre" runat="server"></asp:TextBox>
        <br />
        <%--Misión--%>
        <asp:Label ID="Label3" runat="server" Text="Ingresa la misión"></asp:Label>
        <asp:TextBox ID="TBMision" runat="server" ></asp:TextBox>
        <br />
        <%--Visión--%>
        <asp:Label ID="Label5" runat="server" Text="Ingresa la visión"></asp:Label>
        <asp:TextBox ID="TBVision" runat="server" ></asp:TextBox>
        <br />
        <%--Dirreción--%>
        <asp:Label ID="Label6" runat="server" Text="Ingresa la dirreción"></asp:Label>
        <asp:TextBox ID="TBDireccion" runat="server" ></asp:TextBox>
        <br />
        <%--Teléfono--%>
        <asp:Label ID="Label7" runat="server" Text="Ingresa el teléfono"></asp:Label>
        <asp:TextBox ID="TBTlefono" runat="server" ></asp:TextBox>
        <br />
        <%--Teléfono2--%>
        <asp:Label ID="Label8" runat="server" Text="Ingresa el teléfono2"></asp:Label>
        <asp:TextBox ID="TBTelefono2" runat="server" ></asp:TextBox>
        <br />
        <%--Correo--%>
        <asp:Label ID="Label9" runat="server" Text="Ingresa el correo"></asp:Label>
        <asp:TextBox ID="TBCorreo" runat="server" ></asp:TextBox>


    </div>
    <div>
        <%--Botones Guardar y Actualizar--%>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" />
        <br />
        <asp:Label ID="LblMsj" runat="server" Text=""></asp:Label>
    </div>
    <br />


    <%--Tabla Empresa--%>
    <h2>Lista de los Empresa</h2>
    <table id="empresaTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>EmpresaID</th>
                <th>Número nit</th>

                <th>Nombre</th>
                <th>Misión</th>
                <th>Visión</th>
                <th>Dirreción</th>
                <th>Teléfono</th>
                <th>Teléfono2</th>
                <th>Correo</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
    <script src="resources/js/datatables.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#empresaTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFEmpresa.aspx/ListEmpresas",// Se invoca el WebMethod Listar Productos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de productos del resultado
                    }
                },
                "columns": [
                    { "data": "EmpresaID" },
                    { "data": "Numeronit"},
                    { "data": "Nombre" },
                    { "data": "Mision" },
                    { "data": "Vision" },
                    { "data": "Direccion" },
                    { "data": "Telefono" },
                    { "data": "Telefono2" },
                    { "data": "Correo" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.EmpresaID}">Editar</button>
                              <button class="delete-btn" data-id="${row.EmpresaID}">Eliminar</button>`;
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

            // Editar un caso
            $('#empresaTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#expedienteTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadEmpresaData(rowData);
            });

            // Eliminar un caso
            $('#empresaTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del caso
                if (confirm("¿Estás seguro de que deseas eliminar este empresa?")) {
                    deleteEmpresa(id);// Invoca a la función para eliminar el caso
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadEmpresaData(rowData) {
            $('#<%= EmpresaID.ClientID %>').val(rowData.EmpresaID);
            $('#<%= TBNumeronit.ClientID %>').val(rowData.Numeronit);
            $('#<%= TBNombre.ClientID %>').val(rowData.Nombre);
            $('#<%= TBMision.ClientID %>').val(rowData.Mision);
            $('#<%= TBVision.ClientID %>').val(rowData.Vision);
            $('#<%= TBDireccion.ClientID %>').val(rowData.Dirrecion);
            $('#<%= TBTlefono.ClientID %>').val(rowData.Telefono);
            $('#<%= TBTelefono2.ClientID %>').val(rowData.Telefono2);
            $('#<%= TBCorreo.ClientID %>').val(rowData.Correo);
        }

        // Función para eliminar un producto
        /*function deleteEmpresa(id) {
            $.ajax({
                type: "POST",
                url: "WFEmpresa.aspx/DeleteEmpresa",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#empresaTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Producto eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar la empresa.");
                }
            });
        }*/
    </script>
</asp:Content>
