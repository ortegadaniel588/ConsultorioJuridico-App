<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFCita.aspx.cs" Inherits="Presentation.WFCita" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Citas
        </div>
        <div class="card-body">
            <form runat="server">
                <asp:HiddenField ID="TBId" runat="server" />
                
                <div class="row m-1">
                    <div class="col-4">
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Horario:"></asp:Label>
                        <asp:DropDownList ID="DDLHorarios" CssClass="form-control" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVHorario" 
                            runat="server" 
                            ControlToValidate="DDLHorarios" 
                            ForeColor="Red" 
                            Display="Dynamic" 
                            ErrorMessage="Seleccione un horario">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Asunto:"></asp:Label>
                        <asp:TextBox ID="TBAsunto" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVAsunto" 
                            runat="server" 
                            ControlToValidate="TBAsunto" 
                            ForeColor="Red" 
                            Display="Dynamic" 
                            ErrorMessage="Ingrese el asunto">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Estado:"></asp:Label>
                        <asp:DropDownList ID="DDLEstado" CssClass="form-control" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVEstado" 
                            runat="server" 
                            ControlToValidate="DDLEstado" 
                            ForeColor="Red" 
                            Display="Dynamic" 
                            ErrorMessage="Seleccione un estado">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row m-1">
                    <div class="col">
                        <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
                        <asp:Label ID="LblMsg" CssClass="form-label text-success" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <div class="card m-1">
        <div class="card-header">
            Lista de Citas
        </div>
        <div class="card-body">
            <table id="citasTable" class="table table-hover display" style="width: 100%">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Horario</th>
                        <th>Asunto</th>
                        <th>Estado</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>

    <script src="resources/js/datatables.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#citasTable').DataTable({
                "ajax": {
                    "url": "WFCita.aspx/ListCitas",
                    "type": "POST",
                    "contentType": "application/json",
                    "dataType": "json",
                    "dataSrc": "d.data"
                },
                "columns": [
                    { "data": "id" },
                    { "data": "horario" },
                    { "data": "asunto" },
                    { "data": "estado" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button type="button" class="btn btn-info btn-sm edit-btn" data-id="${row.id}">
                                        <i class="fas fa-edit"></i> Editar
                                    </button>
                                    <button type="button" class="btn btn-danger btn-sm delete-btn" data-id="${row.id}">
                                        <i class="fas fa-trash"></i> Eliminar
                                    </button>`;
                        }
                    }
                ],
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.24/i18n/Spanish.json"
                }
            });

            $('#citasTable').on('click', '.edit-btn', function () {
                var rowData = $('#citasTable').DataTable().row($(this).parents('tr')).data();
                loadCitaData(rowData);
            });

            $('#citasTable').on('click', '.delete-btn', function () {
                if (confirm('¿Está seguro de eliminar esta cita?')) {
                    deleteCita($(this).data('id'));
                }
            });
        });

        function loadCitaData(rowData) {
            $('#<%= TBId.ClientID %>').val(rowData.id);
            $('#<%= DDLHorarios.ClientID %>').val(rowData.horarioId);
            $('#<%= TBAsunto.ClientID %>').val(rowData.asunto);
            $('#<%= DDLEstado.ClientID %>').val(rowData.estado);
            $('#<%= BtnSave.ClientID %>').hide();
            $('#<%= BtnUpdate.ClientID %>').show();
        }

        function deleteCita(id) {
            $.ajax({
                type: "POST",
                url: "WFCita.aspx/DeleteCita",
                data: JSON.stringify({ id: id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        $('#citasTable').DataTable().ajax.reload();
                        alert('Cita eliminada correctamente');
                    }
                }
            });
        }
    </script>
</asp:Content>