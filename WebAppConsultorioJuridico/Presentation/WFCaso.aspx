<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFCaso.aspx.cs" Inherits="Presentation.WFCaso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Casos
        </div>
        <div class="card-body">
            <form id="FrmCaso" runat="server">
                <%--ID Caso--%>
                <asp:HiddenField ID="CasoID" runat="server" />
                <div class="row m-1">
                    <div class="col-4">
                        <%--Código--%>
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Ingrese el Código"></asp:Label>
                        <asp:TextBox ID="TBCodigo" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVCodigo"
                            runat="server"
                            ControlToValidate="TBCodigo"
                            ForeColor="Red"
                            Display="Dynamic"
                            ErrorMessage="El código es obligatorio.">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <%--Nombre--%>
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Ingrese el Nombre"></asp:Label>
                        <asp:TextBox ID="TBNombre" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-4">
                        <%--Empresa--%>
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Seleccione la Empresa"></asp:Label>
                        <asp:DropDownList ID="DDLEmpresa" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col-4">
                        <%--Fecha de Cierre--%>
                        <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Fecha de Cierre"></asp:Label>
                        <asp:TextBox ID="TBFechacierre" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-4">
                        <%--Asunto--%>
                        <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Ingrese el Asunto"></asp:Label>
                        <asp:TextBox ID="TBAsunto" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-4">
                        <%--Tipo--%>
                        <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="Seleccione el Tipo"></asp:Label>
                        <asp:DropDownList ID="DDLTipo" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col-4">
                        <%--Estado--%>
                        <asp:Label ID="Label7" CssClass="form-label" runat="server" Text="Seleccione el Estado"></asp:Label>
                        <asp:DropDownList ID="DDLEstado" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-4">
                        <%--Complejidad--%>
                        <asp:Label ID="Label8" CssClass="form-label" runat="server" Text="Seleccione la Complejidad"></asp:Label>
                        <asp:DropDownList ID="DDLComplejidad" CssClass="form-select" runat="server">
                            <asp:ListItem Text="Alta" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Media" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Baja" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col">
                        <%--Botones--%>
                        <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnUpdate" runat="server" CssClass="btn btn-primary" Text="Actualizar" OnClick="BtnUpdate_Click" />
                        <asp:Label ID="LblMsg" CssClass="form-label" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <div class="card m-1">
        <%--Panel Lista de Casos--%>
        <asp:Panel ID="PanelAdmin" runat="server">
            <div class="card-header">
                Lista de Casos
            </div>
            <div class="card-body">
                <table id="casesTable" class="table table-hover display" style="width: 100%">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Código</th>
                            <th>Nombre</th>
                            <th>Empresa</th>
                            <th>Fecha de Cierre</th>
                            <th>Asunto</th>
                            <th>Tipo</th>
                            <th>Estado</th>
                            <th>Complejidad</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </asp:Panel>
    </div>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Casos--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#casesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFCaso.aspx/ListCases", // WebMethod para listar casos
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
                    { "data": "CasoID" },
                    { "data": "Codigo" },
                    { "data": "Nombre" },
                    { "data": "Empresa" },
                    { "data": "FechaCierre" },
                    { "data": "Asunto" },
                    { "data": "Tipo" },
                    { "data": "Estado" },
                    { "data": "Complejidad" }
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
        });
    </script>
</asp:Content>
