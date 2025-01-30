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
            <form id="FrmCita" runat="server" class="container mt-4">
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

    <asp:Panel ID="PanelAdmin" runat="server">
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
    </asp:Panel>

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
                            return `
                                        <button class="btn btn-link btn-lg px-0 edit-btn" data-id="${row.id}" title="Editar" style="color:#fd7e14">
                                            <i class="lni lni-pencil-1"></i>
                                        </button>
                                        <button class="btn btn-link btn-lg text-danger px-0 delete-btn" data-id="${row.id}" title="Eliminar">
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