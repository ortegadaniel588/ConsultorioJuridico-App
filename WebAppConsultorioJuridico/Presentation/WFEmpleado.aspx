<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEmpleado.aspx.cs" Inherits="Presentation.WFEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Empleados
        </div>
        <div class="card-body">
            <form id="FrmEmpleado" runat="server" class="container mt-4">
                <asp:HiddenField ID="HFEmpleadoID" runat="server" />

                <div class="row m-1">
                    <div class="col-6">
                        <asp:Label ID="Label1" runat="server" Text="Seleccione el Usuario"></asp:Label>
                        <asp:DropDownList ID="DDLUsuarios" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVUsuario" runat="server" ControlToValidate="DDLUsuarios" ForeColor="Red" Display="Dynamic" ErrorMessage="Seleccione un usuario"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-6">
                        <asp:Label ID="Label2" runat="server" Text="Seleccione la Especialidad"></asp:Label>
                        <asp:DropDownList ID="DDLEspecialidades" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVEspecialidad" runat="server" ControlToValidate="DDLEspecialidades" ForeColor="Red" Display="Dynamic" ErrorMessage="Seleccione una especialidad"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row m-1">
                    <div class="col">
                        <asp:Button ID="BtnSave" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CssClass="btn btn-success" OnClick="BtnUpdate_Click" />
                        <asp:Label ID="LblMsg" runat="server" CssClass="text-info"></asp:Label>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <asp:Panel ID="PanelAdmin" runat="server">
        <div class="card m-1">
            <div class="card-header">
                Lista de Empleados
            </div>
            <div class="card-body">
                <table id="tblEmpleados" class="table table-striped table-bordered">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Usuario</th>
                            <th>Especialidad</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </asp:Panel>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblEmpleados').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFEmpleado.aspx/ListEmpleados",
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
                    { "data": "idempleado" },
                    { "data": "usuario" },
                    { "data": "especialidad" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `
                                        <button class="btn btn-link btn-lg px-0 edit-btn" data-id="${row.idempleado}" title="Editar" style="color:#fd7e14">
                                            <i class="lni lni-pencil-1"></i>
                                        </button>
                                        <button class="btn btn-link btn-lg text-danger px-0 delete-btn" data-id="${row.idempleado}" title="Eliminar">
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

            $('#tblEmpleados').on('click', '.edit-btn', function () {
                var rowData = $('#tblEmpleados').DataTable().row($(this).parents('tr')).data();
                loadEmpleadoData(rowData);
            });

            $('#tblEmpleados').on('click', '.delete-btn', function () {
                const id = $(this).data('id');
                if (confirm("¿Estás seguro de que deseas eliminar este empleado?")) {
                    deleteEmpleado(id);
                }
            });
        });

        function loadEmpleadoData(rowData) {
            $('#<%= HFEmpleadoID.ClientID %>').val(rowData.idempleado);
            $('#<%= DDLUsuarios.ClientID %>').val(rowData.usuarioId);
            $('#<%= DDLEspecialidades.ClientID %>').val(rowData.especialidadId);
        }

        function deleteEmpleado(id) {
            $.ajax({
                type: "POST",
                url: "WFEmpleado.aspx/DeleteEmpleado",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#tblEmpleados').DataTable().ajax.reload();
                    alert("Empleado eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el empleado.");
                }
            });
        }
    </script>
</asp:Content>