<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRedsocial.aspx.cs" Inherits="Presentation.WFRedsocial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="card m-1">
    <div class="card-header">
        Gestión de Redes Sociales
    </div>
    <div class="card-body">
        <form id="FrmRdsocial" runat="server">
            <%--Id--%>
            <asp:HiddenField ID="RedsocialID" runat="server" />
            <div class="row m-1">
                <div class="col-6">
                    <%--Nombre--%>
                    <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Ingrese el nombre"></asp:Label>
                    <asp:TextBox ID="TBNombre" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TBNombre" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                </div>
                <div class="col-6">
                    <%--Descripción--%>
                    <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Ingrese la descripción"></asp:Label>
                    <asp:TextBox ID="TBDescripcion" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TBDescripcion" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>

                </div>
            </div>
            
            <div class="row m-1">
                <div class="col">
                    <%--Botones Guardar y Actualizar--%>
                    <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                    <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
                    <asp:Label ID="LblMsj" CssClass="form-label" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </form>
    </div>
</div>

<div class="card m-1">
    <%--Panel para la gestión de Redes Sociales--%>
    <asp:Panel ID="PanelAdmin" runat="server">
        <div class="card-header">
            Lista de Redes Sociales
        </div>
        <div class="table-responsive">
            <%--Lista de Redes Sociales--%>
            <table id="redessocialesTable" class="table table-hover display" style="width: 100%">
                <thead>
                    <tr>
                        <th>RedsocialID</th>
                        <th>Nombre</th>
                        <th>Descripción</th>
                        <th>Opciones</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </asp:Panel>
</div>


    <script src="resources/js/datatables.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#redessocialesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFRedsocial.aspx/ListRedessociales",// Se invoca el WebMethod Listar Social
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de redes socilaes del resultado
                    }
                },
                "columns": [
                    { "data": "RedsocialID" },
                    { "data": "Nombre" },
                    { "data": "Descripcion" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.RedsocialID}">Editar</button>
                              <button class="delete-btn" data-id="${row.RedsocialID}">Eliminar</button>`;
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

            // Editar una red social
            $('#redessocialesTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#redessocialesTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadRedsocialData(rowData);
            });

            // Eliminar una red social
            $('#redessocialesTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID de la red social
                if (confirm("¿Estás seguro de que deseas eliminar esta red social?")) {
                    deleteRedsocial(id);// Invoca a la función para eliminar la red social
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadRedsocialData(rowData) {
            $('#<%= RedsocialID.ClientID %>').val(rowData.RedsocialID);
            $('#<%= TBNombre.ClientID %>').val(rowData.Nombre);
            $('#<%= TBDescripcion.ClientID %>').val(rowData.Descripcion);
        }

        // Función para eliminar un la redsocial
        function deleteRedsocial(id) {
            $.ajax({
                type: "POST",
                url: "WFRedsocial.aspx/deleteRedsocial",// Se invoca el WebMethod Eliminar una red social
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#redessocialesTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Red social eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar la red social.");
                }
            });
        }
    </script>
</asp:Content>
