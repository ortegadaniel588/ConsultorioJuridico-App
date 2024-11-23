<%@ Page Title="Gestión de Expedientes" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFExpediente.aspx.cs" Inherits="Presentation.WFExpediente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Expedientes
        </div>
        <div class="card-body">
            <form id="FrmExpediente" runat="server">
                <%--Id--%>
                <asp:HiddenField ID="HFExpedienteID" runat="server" />
                <div class="row m-1">
                    <div class="col-4">
                        <%--Caso--%>
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Seleccione el caso"></asp:Label>
                        <asp:DropDownList ID="DDCaso_idcaso" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-4">
                        <%--Código--%>
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Ingrese el código"></asp:Label>
                        <asp:TextBox ID="TBCodigo" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVCodigo" runat="server" ControlToValidate="TBCodigo" ErrorMessage="El código es obligatorio." ForeColor="Red" />
                    </div>
                    <div class="col-4">
                        <%--Estado--%>
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Seleccione el estado"></asp:Label>
                        <asp:DropDownList ID="DDLEstado" CssClass="form-select" runat="server">
                            <asp:ListItem Text="Pendiente" Value="Pendiente"></asp:ListItem>
                            <asp:ListItem Text="Resuelto" Value="Resuelto"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col-6">
                        <%--Razón--%>
                        <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Ingrese la razón"></asp:Label>
                        <asp:TextBox ID="TBRazon" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-6">
                        <%--Evidencia--%>
                        <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Ingrese la evidencia"></asp:Label>
                        <asp:TextBox ID="TBEvidencia" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col">
                        <%--Botones Guardar y Actualizar--%>
                        <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
                        <asp:Label ID="LblMsg" CssClass="form-label" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <div class="card m-1">
        <asp:Panel ID="PanelExpedientes" runat="server">
            <div class="card-header">
                Lista de Expedientes
            </div>
            <div class="card-body">
                <table id="expedientesTable" class="table table-hover display" style="width: 100%">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Caso</th>
                            <th>Código</th>
                            <th>Razón</th>
                            <th>Evidencia</th>
                            <th>Estado</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </asp:Panel>
    </div>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#expedientesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFExpediente.aspx/ListExpedientes",
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);
                    },
                    "dataSrc": function (json) {
                        return json.d.data;
                    }
                },
                "columns": [
                    { "data": "ExpedienteID" },
                    { "data": "Caso" },
                    { "data": "Codigo" },
                    { "data": "Razon" },
                    { "data": "Evidencia" },
                    { "data": "Estado" },
                    {
                        "data": null,
                        "render": function (row) {
                            return `
                                <button class="btn btn-info edit-btn" data-id="${row.ExpedienteID}">Editar</button>
                                <button class="btn btn-danger delete-btn" data-id="${row.ExpedienteID}">Eliminar</button>
                            `;
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

            $('#expedientesTable').on('click', '.edit-btn', function () {
                const rowData = $('#expedientesTable').DataTable().row($(this).parents('tr')).data();
                loadExpedienteData(rowData);
            });

            $('#expedientesTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');
                if (confirm("¿Estás seguro de que deseas eliminar este expediente?")) {
                    deleteExpediente(id);
                }
            });
        });

        function loadExpedienteData(rowData) {
            $('#<%= HFExpedienteID.ClientID %>').val(rowData.ExpedienteID);
            $('#<%= DDCaso_idcaso.ClientID %>').val(rowData.CasoID);
            $('#<%= TBCodigo.ClientID %>').val(rowData.Codigo);
            $('#<%= TBRazon.ClientID %>').val(rowData.Razon);
            $('#<%= TBEvidencia.ClientID %>').val(rowData.Evidencia);
            $('#<%= DDLEstado.ClientID %>').val(rowData.Estado);
        }

        function deleteExpediente(id) {
            $.ajax({
                type: "POST",
                url: "WFExpediente.aspx/DeleteExpediente",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function () {
                    $('#expedientesTable').DataTable().ajax.reload();
                    alert("Expediente eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el expediente.");
                }
            });
        }
    </script>
</asp:Content>
