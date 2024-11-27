<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFHorario.aspx.cs" Inherits="Presentation.WFHorario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Horarios
        </div>
        <div class="card-body">
            <form id="FrmHorario" runat="server">
                <asp:HiddenField ID="HFHorarioID" runat="server" />
                
                <div class="row m-1">
                    <div class="col-3">
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Empleado:"></asp:Label>
                        <asp:DropDownList ID="DDLEmpleados" CssClass="form-control" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVEmpleado" 
                            runat="server" 
                            ControlToValidate="DDLEmpleados" 
                            ForeColor="Red" 
                            Display="Dynamic" 
                            ErrorMessage="Seleccione un empleado">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-3">
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Fecha:"></asp:Label>
                        <asp:TextBox ID="TBFecha" CssClass="form-control" runat="server" type="date"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVFecha" 
                            runat="server" 
                            ControlToValidate="TBFecha" 
                            ForeColor="Red" 
                            Display="Dynamic" 
                            ErrorMessage="Seleccione una fecha">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-3">
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Hora Inicio:"></asp:Label>
                        <asp:TextBox ID="TBHoraInicio" CssClass="form-control" runat="server" type="time"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVHoraInicio" 
                            runat="server" 
                            ControlToValidate="TBHoraInicio" 
                            ForeColor="Red" 
                            Display="Dynamic" 
                            ErrorMessage="Ingrese hora inicio">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-3">
                        <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Hora Fin:"></asp:Label>
                        <asp:TextBox ID="TBHoraFin" CssClass="form-control" runat="server" type="time"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVHoraFin" 
                            runat="server" 
                            ControlToValidate="TBHoraFin" 
                            ForeColor="Red" 
                            Display="Dynamic" 
                            ErrorMessage="Ingrese hora fin">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row m-1">
                    <div class="col">
                        <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
                        <asp:Label ID="LblMsg" CssClass="form-label" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <div class="card m-1">
        <div class="card-header">
            Lista de Horarios
        </div>
        <div class="card-body">
            <table id="tblHorarios" class="table table-hover display" style="width: 100%">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Empleado</th>
                        <th>Fecha</th>
                        <th>Hora Inicio</th>
                        <th>Hora Fin</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';

            $('#tblHorarios').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFHorario.aspx/ListHorarios",
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
                    { "data": "id" },
                    { "data": "empleado" },
                    { "data": "fecha" },
                    { "data": "horaInicio" },
                    { "data": "horaFin" },
                    {
                        "data": null,
                        "render": function (row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.id}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.id}">Eliminar</button>`;
                            }
                            return buttons;
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

            $('#tblHorarios').on('click', '.edit-btn', function () {
                var rowData = $('#tblHorarios').DataTable().row($(this).parents('tr')).data();
                loadHorarioData(rowData);
            });

            $('#tblHorarios').on('click', '.delete-btn', function () {
                const id = $(this).data('id');
                if (confirm("¿Estás seguro de que deseas eliminar este horario?")) {
                    deleteHorario(id);
                }
            });
        });

        function loadHorarioData(rowData) {
            $('#<%= HFHorarioID.ClientID %>').val(rowData.id);
            $('#<%= DDLEmpleados.ClientID %>').val(rowData.empleadoId);
            $('#<%= TBFecha.ClientID %>').val(rowData.fecha);
            $('#<%= TBHoraInicio.ClientID %>').val(rowData.horaInicio);
            $('#<%= TBHoraFin.ClientID %>').val(rowData.horaFin);
            $('#<%= BtnSave.ClientID %>').hide();
            $('#<%= BtnUpdate.ClientID %>').show();
        }

        function deleteHorario(id) {
            $.ajax({
                type: "POST",
                url: "WFHorario.aspx/DeleteHorario",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#tblHorarios').DataTable().ajax.reload();
                    alert("Horario eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el horario.");
                }
            });
        }
    </script>
</asp:Content>