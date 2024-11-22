<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEspecialidad.aspx.cs" Inherits="Presentation.WFEspecialidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="FrmEspecialidad" runat="server">
        <%--ID--%>
        <asp:HiddenField ID="HFEspecialidadID" runat="server" />

        <%--Nombre--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el Nombre"></asp:Label>
        <asp:TextBox ID="TBNombre" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RFVNombre" runat="server" ControlToValidate="TBNombre" 
            ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio"></asp:RequiredFieldValidator>
        <br />

        <%--Descripción--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la Descripción"></asp:Label>
        <asp:TextBox ID="TBDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        <br />

        <%--Botones--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CssClass="btn btn-success" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" CssClass="text-info"></asp:Label>
        </div>
        <br />
    </form>

    <%--Lista de Especialidades--%>
    <div>
        <table id="tblEspecialidades" class="table table-striped table-bordered">
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
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.id}">Editar</button>
                                   <button class="delete-btn" data-id="${row.id}">Eliminar</button>`;
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