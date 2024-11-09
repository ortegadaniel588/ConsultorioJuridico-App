<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFCita.aspx.cs" Inherits="Presentation.WFCita" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <%--ID--%>
        <asp:HiddenField ID="TBId" runat="server" />
        <%--Horario ID--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el Horario ID"></asp:Label>
        <asp:TextBox ID="TBHorarioId" runat="server"></asp:TextBox>
        <br />
        <%--Asunto--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese el Asunto"></asp:Label>
        <asp:TextBox ID="TBAsunto" runat="server"></asp:TextBox>
        <br />
        <%--Estado--%>
        <asp:Label ID="Label3" runat="server" Text="Ingrese el Estado"></asp:Label>
        <asp:TextBox ID="TBEstado" runat="server"></asp:TextBox>
        <br />
        <%--Botones Guardar y Actualizar--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>

    <%--Lista de Citas--%>
    <div>
        <table id="citasTable" class="display">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Horario ID</th>
                    <th>Asunto</th>
                    <th>Estado</th>
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
            $('#citasTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFCita.aspx/ListCitas", // Se invoca el WebMethod Listar Citas
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
                    { "data": "idcita" },
                    { "data": "horarioid" },
                    { "data": "asunto" },
                    { "data": "estado" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.idcita}">Editar</button>
                                    <button class="delete-btn" data-id="${row.idcita}">Eliminar</button>`;
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

            $('#citasTable').on('click', '.edit-btn', function () {
                var rowData = $('#citasTable').DataTable().row($(this).parents('tr')).data();
                loadCitaData(rowData);
            });

            $('#citasTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id'); // Obtener el ID de la cita
                if (confirm("¿Estás seguro de que deseas eliminar esta cita?")) {
                    deleteCita(id); // Invoca a la función para eliminar la cita
                }
            });
        });

        // Cargar los datos en los TextBox para actualizar
        function loadCitaData(rowData) {
            $('#<%= TBId.ClientID %>').val(rowData.idcita);
            $('#<%= TBHorarioId.ClientID %>').val(rowData.horarioid);
            $('#<%= TBAsunto.ClientID %>').val(rowData.asunto);
            $('#<%= TBEstado.ClientID %>').val(rowData.estado);
        }

        // Función para eliminar una cita
        function deleteCita(id) {
            $.ajax({
                type: "POST",
                url: "WFCita.aspx/DeleteCita", // Se invoca el WebMethod Eliminar una Cita
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#citasTable').DataTable().ajax.reload(); // Recargar la tabla después de eliminar
                    alert("Cita eliminada exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar la cita.");
                }
            });
        }
    </script>
</asp:Content>