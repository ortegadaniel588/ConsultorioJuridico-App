<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEmpresa.aspx.cs" Inherits="Presentation.WFEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Empresa
        </div>
        <div class="card-body">
            <form id="FrmEmpresa" runat="server">
                <asp:HiddenField ID="EmpresaID" runat="server" />
                <div class="row m-1">
                    <div class="col-4">
                        <%--Número nit--%>
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Ingresa el número nit"></asp:Label>
                        <asp:TextBox ID="TBNumeronit" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator ID="RFVCode" runat="server" ControlToValidate="TBNumeronit" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-8">
                        <%--Nombre--%>
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Ingresa el nombre"></asp:Label>
                        <asp:TextBox ID="TBNombre" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TBNombre" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div class="row m-1">
                    <div class="col-6">
                        <%--Misión--%>
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Ingresa la misión"></asp:Label>
                        <asp:TextBox ID="TBMision" CssClass="form-control" runat="server" TextMode="MultiLine" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TBMision" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-6">
                        <%--Visión--%>
                        <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Ingresa la visión"></asp:Label>
                        <asp:TextBox ID="TBVision" CssClass="form-control" runat="server" TextMode="MultiLine" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TBVision" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div class="row m-1">
                    <div class="col-6">
                        <%--Dirección--%>
                        <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="Ingresa la dirección"></asp:Label>
                        <asp:TextBox ID="TBDireccion" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TBDireccion" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-6">
                        <%--Teléfono--%>
                        <asp:Label ID="Label7" CssClass="form-label" runat="server" Text="Ingresa el teléfono"></asp:Label>
                        <asp:TextBox ID="TBTelefono" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TBTelefono" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div class="row m-1">
                    <div class="col-6">
                        <%--Teléfono2--%>
                        <asp:Label ID="Label8" CssClass="form-label" runat="server" Text="Ingresa el teléfono2"></asp:Label>
                        <asp:TextBox ID="TBTelefono2" CssClass="form-control" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TBTelefono2" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>

                    </div>
                    <div class="col-6">
                        <%--Correo--%>
                        <asp:Label ID="Label9" CssClass="form-label" runat="server" Text="Ingresa el correo"></asp:Label>
                        <asp:TextBox ID="TBCorreo" CssClass="form-control" TextMode="Email" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TBCorreo" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div class="row m-1">
                    <div class="col">
                        <%--Botones Guardar y Actualizar--%>
                        <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
                        <asp:Label ID="LblMsj" CssClass="form-label" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <div class="card m-1">
        <asp:Panel ID="PanelAdmin" runat="server">
            <div class="card-header">
                Lista de Consultorios
            </div>
            <div class="table-responsive">
                <table id="empresaTable" class="table table-hover display" style="width: 100%">
                    <thead>
                        <tr>
                            <th>EmpresaID</th>
                            <th>Número nit</th>
                            <th>Nombre</th>
                            <th>Misión</th>
                            <th>Visión</th>
                            <th>Dirección</th>
                            <th>Teléfono</th>
                            <th>Teléfono2</th>
                            <th>Correo</th>
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
            $('#empresaTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFEmpresa.aspx/listEmpresas",// Se invoca el WebMethod Listar Empresa
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de Empresa del resultado
                    }
                },
                "columns": [
                    { "data": "EmpresaID" },
                    { "data": "Numeronit" },
                    { "data": "Nombre" },
                    { "data": "Mision" },
                    { "data": "Vision" },
                    { "data": "Direccion" },
                    { "data": "Telefono" },
                    { "data": "Telefono2" },
                    { "data": "Correo" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.EmpresaID}">Editar</button>
                              <button class="delete-btn" data-id="${row.EmpresaID}">Eliminar</button>`;
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

            // Editar un empresa
            $('#empresaTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#empresaTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadEmpresaData(rowData);
            });

            // Eliminar un empresa
            $('#empresaTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del Consultorio
                if (confirm("¿Estás seguro de que deseas eliminar este Consultorio?")) {
                    deleteEmpresa(id);// Invoca a la función para eliminar el Consultorio
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadEmpresaData(rowData) {
            $('#<%= EmpresaID.ClientID %>').val(rowData.EmpresaID);
            $('#<%= TBNumeronit.ClientID %>').val(rowData.Numeronit);
            $('#<%= TBNombre.ClientID %>').val(rowData.Nombre);
            $('#<%= TBMision.ClientID %>').val(rowData.Mision);
            $('#<%= TBVision.ClientID %>').val(rowData.Vision);
            $('#<%= TBDireccion.ClientID %>').val(rowData.Direccion);
            $('#<%= TBTelefono.ClientID %>').val(rowData.Telefono);
            $('#<%= TBTelefono2.ClientID %>').val(rowData.Telefono2);
            $('#<%= TBCorreo.ClientID %>').val(rowData.Correo);
        }

        // Función para eliminar un Consultorio
        function deleteEmpresa(id) {
            $.ajax({
                type: "POST",
                url: "WFEmpresa.aspx/deleteEmpresa",// Se invoca el WebMethod Eliminar un Consultorio
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#empresaTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Consultorio eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el consultorio.");
                }
            });
        }
    </script>
</asp:Content>
