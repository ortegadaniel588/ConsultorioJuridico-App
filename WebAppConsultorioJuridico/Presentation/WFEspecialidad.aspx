<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEspecialidad.aspx.cs" Inherits="Presentation.WFEspecialidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <%--ID--%>
        <asp:HiddenField ID="TBId" runat="server" />
        <%--Nombre--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el Nombre"></asp:Label>
        <asp:TextBox ID="TBNombre" runat="server"></asp:TextBox>
        <br />
        <%--Descripción--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la Descripción"></asp:Label>
        <asp:TextBox ID="TBDescripcion" runat="server"></asp:TextBox>
        <br />
        <%--Botones Guardar y Actualizar--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>

    <%--Lista de Especialidades--%>
    <div>
        <table id="especialidadesTable" class="display">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nombre</th>
                    <th>Descripción</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#especialidadesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFEspecialidad.aspx/ListEspecialidades", // Se invoca el WebMethod Listar Especialidades
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d); // Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data; // Obtiene la lista 
                    }
                },
                "columns": [
                    { "data": "idespecialidad" },
                    { "data": "nombre" },
                    { "data": "descripcion" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.idespecialidad}">Editar</button>
                                    <button class="delete-btn" data-id="${row.idespecialidad}">Eliminar</button>`;
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

            $('#especialidadesTable').on('click', '.edit-btn', function () {
                var rowData = $('#especialidadesTable').DataTable().row($(this).parents('tr')).data();
                loadEspecialidadData(rowData);
            });

            $('#especialidadesTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id'); // Obtener el ID de la especialidad
                if (confirm("¿Estás seguro de que deseas eliminar esta especialidad?")) {
                    deleteEspecialidad(id); // Invoca a la función para eliminar la especialidad
                }
            });
        });

        // Cargar los datos en los TextBox para actualizar
        function loadEspecialidadData(rowData) {
            $('#<%= TBId.ClientID %>').val(rowData.idespecialidad);
            $('#<%= TBNombre.ClientID %>').val(rowData.nombre);
            $('#<%= TBDescripcion.ClientID %>').val(rowData.descripcion);
        }

        // Función para eliminar una especialidad
        function deleteEspecialidad(id) {
            $.ajax({
                type: "POST",
                url: "WFEspecialidad.aspx/DeleteEspecialidad", // Se invoca el WebMethod Eliminar una Especialidad
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#especialidadesTable').DataTable().ajax.reload(); // Recargar la tabla después de eliminar
                    alert("Especialidad eliminada exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar la especialidad.");
                }
            });
        }
    </script>
</asp:Content>