<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFSeguimiento.aspx.cs" Inherits="Presentation.WFSeguimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">    
<div>
        <%--ID--%>
        <asp:HiddenField ID="SeguimientoID" runat="server" />        <br />
        <%--Caso--%>
        <asp:Label ID="Label1" runat="server" Text="Seleccione el caso"></asp:Label>
        <asp:DropDownList ID="DDCaso_idcaso" runat="server" CssClass="form-select"></asp:DropDownList>
        <br />
        <%--Fechaactu de actualización--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la fecha de actualización"></asp:Label>
        <asp:TextBox ID="TBFechaactualizacion" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Proceso--%>
        <asp:Label ID="Label3" runat="server" Text="Ingrese el proceso"></asp:Label>
        <asp:TextBox ID="TBProceso" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Descripción--%>
        <asp:Label ID="Label4" runat="server" Text="Ingrese la acción Descripción"></asp:Label>
        <asp:TextBox ID="TBDescripcion" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Estado--%>
        <asp:Label ID="Label5" runat="server" Text="Ingrese el estado"></asp:Label>
        <asp:TextBox ID="TBEstado" runat="server" Visible="false"></asp:TextBox>
        <br />
    </div>

    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" />
        <br />
        <asp:Label ID="LblMsj" runat="server" Text=""></asp:Label>
    </div>
    <br />
    </form>
    <%--lista de productos--%>

    <div>
        <h2>Lista de los Seguimientos</h2>
        <table id="seguimientosTable" class="display" style="width: 100%">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Caso</th>
                    <th>Fecha actualización</th>
                    <th>Proceso</th>
                    <th>Descripción</th>
                    <th>Estado</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <script src="resources/js/datatables.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#productsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFCaso.aspx/ListCasos",// Se invoca el WebMethod Listar Productos
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
                    { "data": "CasoID" },
                    { "data": "Codigo" },
                    { "data": "Empresa" },
                    { "data": "Fechaapertura" },
                    { "data": "Fechacierra" },
                    { "data": "Asunto" },
                    { "data": "Tipo" },
                    { "data": "Estado", "visible": false },
                    { "data": "Complejidad" },
                    { "data": "Empleado", "visible": false },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.CasoID}">Editar</button>
                                  <button class="delete-btn" data-id="${row.CasoID}">Eliminar</button>`;
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
            $('#seguimientosTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#casosTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadCasoData(rowData);
            });

            // Eliminar un caso
            $('#seguimientosTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del caso
                if (confirm("¿Estás seguro de que deseas eliminar este caso?")) {
                    deleteCaso(id);// Invoca a la función para eliminar el caso
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadCasoData(rowData) {
            $('#<%= CasoID.ClientID %>').val(rowData.CasoID);
            $('#<%= TBCodigo.ClientID %>').val(rowData.Codigo);
            $('#<%= DDLEpresa.ClientID %>').val(rowData.Empresa);
            $('#<%= TBFechacierre.ClientID %>').val(rowData.Fechacierra);
            $('#<%= TBAsunto.ClientID %>').val(rowData.Asunto);
            $('#<%= DDLTipo.ClientID %>').val(rowData.Tipo);
            $('#<%= DDLEstado.ClientID %>').val(rowData.Estado);
            $('#<%= DDLComplejidad.ClientID %>').val(rowData.Complejidad);
            $('#<%= DDLEmpleado.ClientID %>').val(rowData.Empleado);
        }

            // Función para eliminar un producto
            /*function deleteCaso(id) {
                $.ajax({
                    type: "POST",
                    url: "WFCaso.aspx/DeleteCaso",// Se invoca el WebMethod Eliminar un Producto
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ id: id }),
                    success: function (response) {
                        $('#casosTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                        alert("Producto eliminado exitosamente.");
                    },
                    error: function () {
                        alert("Error al eliminar el producto.");
                    }
                });
            }*/
    </script>
</asp:Content>
