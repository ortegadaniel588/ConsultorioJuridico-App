<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFExpediente.aspx.cs" Inherits="Presentation.WFExpediente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div>
            <asp:HiddenField ID="ExpedienteID" runat="server" />
            <br />
            <%--Caso--%>
            <asp:Label ID="Label1" runat="server" Text="Seleccione el caso"></asp:Label>
            <asp:DropDownList ID="DDCaso_idcaso" runat="server" CssClass="form-select"></asp:DropDownList>
            <br />
            <%--Código--%>
            <asp:Label ID="Label2" runat="server" Text="Ingrese el código"></asp:Label>

            <asp:TextBox ID="TBCodigo" runat="server"></asp:TextBox>
            <br />
            <%--Acción realizada--%>
            <asp:Label ID="Label4" runat="server" Text="Ingrese la acción realizada"></asp:Label>
            <asp:TextBox ID="TBAccionrealizada" runat="server"></asp:TextBox>
            <br />
            <%--Razón--%>
            <asp:Label ID="Label5" runat="server" Text="Ingrese la razón"></asp:Label>
            <asp:TextBox ID="TBRazon" runat="server"></asp:TextBox>
            <br />
            <%--Relevancia--%>
            <asp:Label ID="Label3" runat="server" Text="Seleccione la relevancia"></asp:Label>
            <asp:DropDownList ID="DDLRelevancia" runat="server" CssClass="form-select">
                <asp:ListItem Text="Seleccione la relevancia" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="alta" Value="1"></asp:ListItem>
                <asp:ListItem Text="media" Value="2"></asp:ListItem>
                <asp:ListItem Text="baja" Value="3"></asp:ListItem>
            </asp:DropDownList>
            <br />
            <%--Evidencia--%>
            <asp:Label ID="Label7" runat="server" Text="Ingrese la evidencia"></asp:Label>
            <asp:TextBox ID="TBEvidencia" runat="server"></asp:TextBox>

            <br />
            <%--Comentario--%>
            <asp:Label ID="Label8" runat="server" Text="Ingrese un comentario"></asp:Label>
            <asp:TextBox ID="TBComentario" runat="server"></asp:TextBox>
            <br />
            <%--Estado--%>

            <asp:Label ID="Label9" runat="server" Text="Seleccione el estado"></asp:Label>
            <asp:DropDownList ID="DDLEstado" runat="server" CssClass="form-select">
                <asp:ListItem Text="Seleccione el estado" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="pendiente" Value="1"></asp:ListItem>
                <asp:ListItem Text="en progreso" Value="2"></asp:ListItem>
                <asp:ListItem Text="finalizada" Value="3"></asp:ListItem>
                <asp:ListItem Text="cancelada" Value="3"></asp:ListItem>
            </asp:DropDownList>
            <br />
        </div>

        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <br />
            <asp:Label ID="LblMsj" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>

    <%--lista de productos--%>
    <h2>Lista de los EXpedientes</h2>
    <table id="expedienteTable" class="display" style="width: 100%">
        <thead>

            <tr>
                <th>casoID</th>
                <th>FKCaso</th>
                <th>Caso</th>
                <th>Codigo</th>
                <th>Fecha creación</th>
                <th>Acción realizada</th>
                <th>Razón</th>
                <th>Relevancia</th>
                <th>Evidencia</th>
                <th>Comentario</th>
                <th>Estado</th>
                <th>Opciones</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <script src="resources/js/datatables.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#expedienteTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFExpediente.aspx/ListExpedientes",// Se invoca el WebMethod Listar Expediente
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
                    { "data": "ExpedienteID" },
                    { "data": "FKCaso", "visible": false },
                    { "data": "Caso" },
                    { "data": "Codigo" },
                    { "data": "Fechacreacion" },
                    { "data": "Accionrealizada" },
                    { "data": "Razon" },
                    { "data": "Relevancia" },
                    { "data": "Evidencia" },
                    { "data": "Comentario" },
                    { "data": "Estado" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.ExpedienteID}">Editar</button>
                              <button class="delete-btn" data-id="${row.ExpedienteID}">Eliminar</button>`;
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

            // Editar un expediente
            $('#expedienteTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#expedienteTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadExpedienteData(rowData);
            });

            // Eliminar un expediente
            $('#expedienteTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del expediente
                if (confirm("¿Estás seguro de que deseas eliminar este expediente?")) {
                    deleteExpediente(id);// Invoca a la función para eliminar el expediente
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadExpedienteData(rowData) {
            $('#<%= ExpedienteID.ClientID %>').val(rowData.ExpedienteID);
            $('#<%= DDCaso_idcaso.ClientID %>').val(rowData.FKCaso);
            $('#<%= TBCodigo.ClientID %>').val(rowData.Codigo);
            $('#<%= TBAccionrealizada.ClientID %>').val(rowData.Accionrealizada);
            $('#<%= TBRazon.ClientID %>').val(rowData.Razon);
            $('#<%= DDLRelevancia.ClientID %>').val(rowData.Relevancia);
            $('#<%= TBEvidencia.ClientID %>').val(rowData.Evidencia);
            $('#<%= TBComentario.ClientID %>').val(rowData.Comentario);
            $('#<%= DDLEstado.ClientID %>').val(rowData.Estado);
        }

        // Función para eliminar un Expediente
        function deleteExpediente(id) {
            $.ajax({
                type: "POST",
                url: "WFExpediente.aspx/DeleteExpediente",// Se invoca el WebMethod Eliminar un Expediente
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#expedienteTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Expediente eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el expediente.");
                }
            });
        }
    </script>
</asp:Content>
