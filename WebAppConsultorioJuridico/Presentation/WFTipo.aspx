<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFTipo.aspx.cs" Inherits="Presentation.WFTipo" %>

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
            <form id="FrmTipo" runat="server">
                <%--Id--%>
                <asp:HiddenField ID="TipoID" runat="server" />
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
                <table id="TipoTable" class="table table-hover display" style="width: 100%">
                    <thead>
                        <tr>
                            <th>TipoID</th>
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
            $('#TipoTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFTipo.aspx/ListTipo",// Se invoca el WebMethod Listar Tipo
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
                    { "data": "TipoID" },
                    { "data": "Nombre" },
                    { "data": "Descripcion" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="btn btn-link btn-lg px-0 edit-btn" data-id="${row.TipoID}" title="Editar" style="color:#fd7e14">
                                            <i class="lni lni-pencil-1"></i>
                                    </button>
                                    <button class="btn btn-link btn-lg text-danger px-0 delete-btn" data-id="${row.TipoID}" title="Eliminar">
                                            <i class="lni lni-trash-3"></i>
                                    </button>`;
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

            // Editar un Tipo
            $('#TipoTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#TipoTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadTipoData(rowData);
            });

            // Eliminar un Tipo
            $('#TipoTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del tipo
                if (confirm("¿Estás seguro de que deseas eliminar este tipo?")) {
                    deleteTipo(id);// Invoca a la función para eliminar el tipo
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadTipoData(rowData) {
            $('#<%= TipoID.ClientID %>').val(rowData.TipoID);
            $('#<%= TBNombre.ClientID %>').val(rowData.Nombre);
            $('#<%= TBDescripcion.ClientID %>').val(rowData.Descripcion);
        }

        // Función para eliminar un Tipo
        function deleteTipo(idtipo) {
            $.ajax({
                type: "POST",
                url: "WFTipo.aspx/deleteTipo",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ idtipo: idtipo }),
                success: function (response) {
                    $('#TipoTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Tipo eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el Tipo.");
                }
            });
        }
    </script>
</asp:Content>
