<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEspecialidad.aspx.cs" Inherits="Presentation.WFEspecialidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Especialidades
        </div>
        <div class="card-body">
            <form id="FrmEspecialidad" runat="server">
                <%--ID--%>
                <asp:HiddenField ID="HFEspecialidadID" runat="server" />
                
                <div class="row m-1">
                    <div class="col-6">
                        <%--Nombre--%>
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Ingrese el Nombre"></asp:Label>
                        <asp:TextBox ID="TBNombre" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVNombre" 
                            runat="server" 
                            ControlToValidate="TBNombre" 
                            ForeColor="Red" 
                            Display="Dynamic" 
                            ErrorMessage="Este campo es obligatorio">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-6">
                        <%--Descripción--%>
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Ingrese la Descripción"></asp:Label>
                        <asp:TextBox ID="TBDescripcion" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>

                <div class="row m-1">
                    <div class="col">
                        <%--Botones--%>
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
            Lista de Especialidades
        </div>
        <div class="card-body">
            <table id="tblEspecialidades" class="table table-hover display" style="width: 100%">
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
    </div>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#tblEspecialidades').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFEspecialidad.aspx/ListEspecialidades",
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
                    { "data": "nombre" },
                    { "data": "descripcion" },
                    {
                        "data": null,
                        "render": function (row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.EspecialidadID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.EspecialidadID}">Eliminar</button>`;
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

            $('#tblEspecialidades').on('click', '.edit-btn', function () {
                var rowData = $('#tblEspecialidades').DataTable().row($(this).parents('tr')).data();
                loadEspecialidadData(rowData);
            });

            $('#tblEspecialidades').on('click', '.delete-btn', function () {
                const id = $(this).data('id');
                if (confirm("¿Estás seguro de que deseas eliminar esta especialidad?")) {
                    deleteEspecialidad(id);
                }
            });
        });

        function loadEspecialidadData(rowData) {
            $('#<%= HFEspecialidadID.ClientID %>').val(rowData.id);
            $('#<%= TBNombre.ClientID %>').val(rowData.nombre);
            $('#<%= TBDescripcion.ClientID %>').val(rowData.descripcion);
        }

        function deleteEspecialidad(id) {
            $.ajax({
                type: "POST",
                url: "WFEspecialidad.aspx/DeleteEspecialidad",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#tblEspecialidades').DataTable().ajax.reload();
                    alert("Especialidad eliminada exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar la especialidad.");
                }
            });
        }
    </script>
</asp:Content>