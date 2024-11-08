<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFCaso.aspx.cs" Inherits="Presentation.WFCaso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div>
            <%--CODIGO--%>
            <asp:HiddenField ID="CasoID" runat="server" />
            <asp:Label ID="Label1" runat="server" Text="Ingrese el código"></asp:Label>
            <asp:TextBox ID="TBCodigo" runat="server"></asp:TextBox>
            <%--EMPRESA--%>
            <asp:Label ID="Label2" runat="server" Text="Seleccione la empresa"></asp:Label>
            <asp:DropDownList ID="DDLEpresa" runat="server"></asp:DropDownList>
            <%--FCIERRE--%>
            <asp:Label ID="Label3" runat="server" Text="Fecha de cierre"></asp:Label>
            <asp:TextBox ID="TBFechacierre" runat="server"></asp:TextBox>
            <%--ASUNTO--%>
            <asp:Label ID="Label4" runat="server" Text="Ingrese el asunto"></asp:Label>
            <asp:TextBox ID="TBAsunto" runat="server"></asp:TextBox>
            <%--TIPO--%>
            <asp:Label ID="Label5" runat="server" Text="Selleccione el tipo"></asp:Label>
            <asp:DropDownList ID="DDLTipo" runat="server"></asp:DropDownList>
            <%--ESTADO--%>
            <asp:Label ID="Label6" runat="server" Text="Seleccione el estado"></asp:Label>
            <asp:DropDownList ID="DDLEstado" runat="server"></asp:DropDownList>
            <%--COMPLEJIDAD--%>
            <asp:Label ID="Label7" runat="server" Text="Seleccione su complejidad"></asp:Label>
            <asp:DropDownList ID="DDLComplejidad" runat="server">
                <asp:ListItem Text="alta" Value="1"></asp:ListItem>
                <asp:ListItem Text="media" Value="2"></asp:ListItem>
                <asp:ListItem Text="baja" Value="3"></asp:ListItem>
            </asp:DropDownList>
            <%--EMPLEADO--%>
            <asp:Label ID="Label8" runat="server" Text="Seleccione un abogado"></asp:Label>
            <asp:DropDownList ID="DDLEmpleado" runat="server"></asp:DropDownList>

        </div>
        <%--Botone guradar y actualizar--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Button" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Button" />
            <asp:Label ID="LblMsj" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>
    <%--    lista de productos--%>

    <%--lista de productos--%>
    <h2>Lista de los Casos</h2>
    <table id="casosTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Código</th>
                <th>Empresa</th>
                <th>Fecha apertura</th>
                <th>Fecha cierre</th>
                <th>Asunto</th>
                <th>Tipo</th>
                <th>Estado</th>
                <th>Complejidad</th>
                <th>Empleado</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
    <div>
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
            $('#casosTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#casosTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadCasoData(rowData);
            });

            // Eliminar un caso
            $('#casosTable').on('click', '.delete-btn', function () {
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
