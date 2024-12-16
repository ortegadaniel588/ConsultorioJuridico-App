<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAsignarredsocial.aspx.cs" Inherits="Presentation.WFAsignarredsocial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Asignación de Redes Sociales
        </div>
        <div class="card-body">
            <form id="FrmAsignarRedSocial" runat="server">
                <%--Asignarredsocial ID--%>
                <asp:HiddenField ID="AsignarredsocialID" runat="server" />
                <div class="row m-1">
                    <div class="col-6">
                        <%--Empresa--%>
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Seleccione la empresa"></asp:Label>
                        <asp:DropDownList ID="DDLEmpresa_idempresa" CssClass="form-select" runat="server"></asp:DropDownList>
                        <%--Valida que se seleccione una empresa--%>
                        <asp:RequiredFieldValidator ID="RFVEmpresa" runat="server" ControlToValidate="DDLEmpresa_idempresa" InitialValue="" ErrorMessage="Debes seleccionar una Empresa." ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-6">
                        <%--Red Social--%>
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Seleccione la red social"></asp:Label>
                        <asp:DropDownList ID="DDLRedsocial_idredsocial" CssClass="form-select" runat="server"></asp:DropDownList>
                        <%--Valida que se seleccione una red social--%>
                        <asp:RequiredFieldValidator ID="RFVRedsocial" runat="server" ControlToValidate="DDLRedsocial_idredsocial" InitialValue="" ErrorMessage="Debes seleccionar una Red Social." ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col-12">
                        <%--Url--%>
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Ingrese la URL del perfil"></asp:Label>
                        <asp:TextBox ID="TBUrl" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TBUrl" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div class="row m-1">
                    <div class="col">
                        <%--Botones Guardar y Actualizar--%>
                        <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnUpdate" runat="server" CssClass="btn btn-primary" Text="Actualizar" OnClick="BtnUpdate_Click" />
                        <asp:Label ID="LblMsj" CssClass="form-label" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <div class="card m-1">
        <asp:Panel ID="PanelAdmin" runat="server">

            <div class="card-header">
                Lista de Asignaciones de Redes Sociales
            </div>
            <div class="table-responsive">
                <%--Lista de Asignaciones de Redes Sociales--%>
                <table id="asignarredessocialesList" class="table table-hover display" style="width: 100%">
                    <thead>
                        <tr>
                            <th>AsignarredsocialID</th>
                            <th>FkEmpresa</th>
                            <th>Empresa</th>
                            <th>FKRedsocial</th>
                            <th>Redsocial</th>
                            <th>Url</th>
                            <th>Opciones</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </asp:Panel>
    </div>


    <script src="resources/js/datatables.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#asignarredessocialesList').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFAsignarredsocial.aspx/listAsignarredessociales",// Se invoca el WebMethod Listar Productos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de asignarredessociales del resultado
                    }
                },
                "columns": [
                    { "data": "AsignarredsocialID" },
                    { "data": "FkEmpresa", "visible": false },
                    { "data": "EmpresaNombre" }, // Agregar columna para mostrar el nombre de la empresa
                    { "data": "FKRedsocial", "visible": false },
                    { "data": "RedsocialNombre" },
                    { "data": "Url" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="btn btn-link btn-lg edit-btn" data-id="${row.AsignarredsocialID}" style="color:#fd7e14" title="Editar"><i class="lni lni-pencil-1"></i></button>
                              <button class="btn btn-link btn-lg text-danger px-0 delete-btn" data-id="${row.AsignarredsocialID}" title="Eliminar"><i class="lni lni-trash-3"></i></button>`;
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

            // Editar un caso
            $('#asignarredessocialesList').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#asignarredessocialesList').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadAsignarredsocialData(rowData);
            });

            // Eliminar un caso
            $('#asignarredessocialesList').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del caso
                if (confirm("¿Estás seguro de que deseas eliminar esta asignación de red social?")) {
                    deleteAsignarredsocial(id);// Invoca a la función para eliminar el asignarredsocial
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadAsignarredsocialData(rowData) {
            $('#<%= AsignarredsocialID.ClientID %>').val(rowData.AsignarredsocialID);
            $('#<%= DDLEmpresa_idempresa.ClientID %>').val(rowData.FkEmpresa);
            $('#<%= DDLRedsocial_idredsocial.ClientID %>').val(rowData.FKRedsocial);
            $('#<%= TBUrl.ClientID %>').val(rowData.Url);
        }

        // Función para eliminar un producto
        function deleteAsignarredsocial(id) {
            $.ajax({
                type: "POST",
                url: "WFAsignarredsocial.aspx/DeleteAsignarredsocial",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#asignarredessocialesList').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Asignacion eliminada exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar la asignación.");
                }
            });
        }
    </script>

</asp:Content>
