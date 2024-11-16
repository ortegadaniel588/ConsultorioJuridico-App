<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeFile="WFPersona.aspx.cs" Inherits="Presentation.WFPersona" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="FrmPersona" runat="server">
        <%--ID--%>
        <asp:HiddenField ID="HFPersonaID" runat="server" />

        <%--Nombres--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese los Nombres"></asp:Label>
        <asp:TextBox ID="TBNames" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RFVNames" runat="server" ControlToValidate="TBNames" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio"></asp:RequiredFieldValidator>
        <br />

        <%--Apellidos--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese los Apellidos"></asp:Label>
        <asp:TextBox ID="TBLastNames" runat="server" CssClass="form-control"></asp:TextBox>
        <br />

        <%--Tipo Documento--%>
        <asp:Label ID="Label3" runat="server" Text="Seleccione el Tipo de Documento"></asp:Label>
        <asp:DropDownList ID="DDLDocumentTypes" runat="server" CssClass="form-control">
            <asp:ListItem Value="cedula">Cédula</asp:ListItem>
            <asp:ListItem Value="pasaporte">Pasaporte</asp:ListItem>
            <asp:ListItem Value="tarjeta de identidad">Tarjeta de Identidad</asp:ListItem>
        </asp:DropDownList>
        <br />

        <%--Documento--%>
        <asp:Label ID="Label4" runat="server" Text="Ingrese el Documento"></asp:Label>
        <asp:TextBox ID="TBDocument" runat="server" CssClass="form-control"></asp:TextBox>
        <br />

        <%--Género--%>
        <asp:Label ID="Label5" runat="server" Text="Seleccione el Género"></asp:Label>
        <asp:DropDownList ID="DDLGender" runat="server" CssClass="form-control">
            <asp:ListItem Value="masculino">Masculino</asp:ListItem>
            <asp:ListItem Value="femenino">Femenino</asp:ListItem>
            <asp:ListItem Value="prefiero no decir">Prefiero no decir</asp:ListItem>
        </asp:DropDownList>
        <br />

        <%--Estado Civil--%>
        <asp:Label ID="Label6" runat="server" Text="Seleccione el Estado Civil"></asp:Label>
        <asp:DropDownList ID="DDLMaritalStatus" runat="server" CssClass="form-control">
            <asp:ListItem Value="soltero">Soltero</asp:ListItem>
            <asp:ListItem Value="casado">Casado</asp:ListItem>
            <asp:ListItem Value="viudo">Viudo</asp:ListItem>
            <asp:ListItem Value="union libre">Unión Libre</asp:ListItem>
        </asp:DropDownList>
        <br />

        <%--Lugar Nacimiento--%>
        <asp:Label ID="Label7" runat="server" Text="Ingrese el Lugar de Nacimiento"></asp:Label>
        <asp:TextBox ID="TBBirthPlace" runat="server" CssClass="form-control"></asp:TextBox>
        <br />

        <%--Fecha Nacimiento--%>
        <asp:Label ID="Label8" runat="server" Text="Ingrese la Fecha de Nacimiento"></asp:Label>
        <asp:TextBox ID="TBBirthDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
        <br />

        <%--Teléfono 1--%>
        <asp:Label ID="Label9" runat="server" Text="Ingrese el Teléfono 1"></asp:Label>
        <asp:TextBox ID="TBPhone1" runat="server" CssClass="form-control"></asp:TextBox>
        <br />

        <%--Teléfono 2--%>
        <asp:Label ID="Label10" runat="server" Text="Ingrese el Teléfono 2"></asp:Label>
        <asp:TextBox ID="TBPhone2" runat="server" CssClass="form-control"></asp:TextBox>
        <br />

        <%--Correo--%>
        <asp:Label ID="Label11" runat="server" Text="Ingrese el Correo Electrónico"></asp:Label>
        <asp:TextBox ID="TBEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
        <br />

        <%--Dirección--%>
        <asp:Label ID="Label12" runat="server" Text="Ingrese la Dirección"></asp:Label>
        <asp:TextBox ID="TBAddress" runat="server" CssClass="form-control"></asp:TextBox>
        <br />

        <%--Estrato Socioeconómico--%>
        <asp:Label ID="Label13" runat="server" Text="Seleccione el Estrato Socioeconómico"></asp:Label>
        <asp:DropDownList ID="DDLSocioeconomicStatus" runat="server" CssClass="form-control">
            <asp:ListItem Value="1">1</asp:ListItem>
            <asp:ListItem Value="2">2</asp:ListItem>
            <asp:ListItem Value="3">3</asp:ListItem>
            <asp:ListItem Value="4">4</asp:ListItem>
            <asp:ListItem Value="5">5</asp:ListItem>
            <asp:ListItem Value="6">6</asp:ListItem>
        </asp:DropDownList>
        <br />

        <%--Ocupación--%>
        <asp:Label ID="Label14" runat="server" Text="Ingrese la Ocupación"></asp:Label>
        <asp:TextBox ID="TBOccupation" runat="server" CssClass="form-control"></asp:TextBox>
        <br />

        <%--Nivel Educativo--%>
        <asp:Label ID="Label15" runat="server" Text="Seleccione el Nivel Educativo"></asp:Label>
        <asp:DropDownList ID="DDLEducationLevel" runat="server" CssClass="form-control">
            <asp:ListItem Value="ninguno">Ninguno</asp:ListItem>
            <asp:ListItem Value="primaria">Primaria</asp:ListItem>
            <asp:ListItem Value="secundaria">Secundaria</asp:ListItem>
            <asp:ListItem Value="técnica">Técnica</asp:ListItem>
            <asp:ListItem Value="técnologica">Tecnológica</asp:ListItem>
            <asp:ListItem Value="pregrado">Pregrado</asp:ListItem>
            <asp:ListItem Value="posgrado">Posgrado</asp:ListItem>
        </asp:DropDownList>
        <br />

        <%--Botones Guardar y Actualizar--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CssClass="btn btn-success" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" CssClass="text-info"></asp:Label>
        </div>
        <br />
    </form>

    <%--Lista de Personas--%>
    <div>
        <table id="tblPersonas" class="table table-striped table-bordered">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nombres</th>
                    <th>Apellidos</th>
                    <th>Documento</th>
                    <th>Teléfono</th>
                    <th>Correo</th>
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
            $('#tblPersonas').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFPersona.aspx/ListPersonas",
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
                    { "data": "idpersona" },
                    { "data": "nombres" },
                    { "data": "apellidos" },
                    { "data": "documento" },
                    { "data": "telefono" },
                    { "data": "correo" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.idpersona}">Editar</button>
                                    <button class="delete-btn" data-id="${row.idpersona}">Eliminar</button>`;
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

            $('#tblPersonas').on('click', '.edit-btn', function () {
                var rowData = $('#tblPersonas').DataTable().row($(this).parents('tr')).data();
                loadPersonaData(rowData);
            });

            $('#tblPersonas').on('click', '.delete-btn', function () {
                const id = $(this).data('id');
                if (confirm("¿Estás seguro de que deseas eliminar esta persona?")) {
                    deletePersona(id);
                }
            });
        });

        function loadPersonaData(rowData) {
            $('#<%= HFPersonaID.ClientID %>').val(rowData.idpersona);
            $('#<%= TBNames.ClientID %>').val(rowData.nombres);
            $('#<%= TBLastNames.ClientID %>').val(rowData.apellidos);
            $('#<%= DDLDocumentTypes.ClientID %>').val(rowData.tipodocumento);
            $('#<%= TBDocument.ClientID %>').val(rowData.documento);
            $('#<%= DDLGender.ClientID %>').val(rowData.genero);
            $('#<%= DDLMaritalStatus.ClientID %>').val(rowData.estadocivil);
            $('#<%= TBBirthPlace.ClientID %>').val(rowData.lugarNacimiento);
            $('#<%= TBBirthDate.ClientID %>').val(rowData.fechaNacimiento.split('T')[0]);
            $('#<%= TBPhone1.ClientID %>').val(rowData.telefono1);
            $('#<%= TBPhone2.ClientID %>').val(rowData.telefono2);
            $('#<%= TBEmail.ClientID %>').val(rowData.correo);
            $('#<%= TBAddress.ClientID %>').val(rowData.direccion);
            $('#<%= DDLSocioeconomicStatus.ClientID %>').val(rowData.estrato);
            $('#<%= TBOccupation.ClientID %>').val(rowData.ocupacion);
            $('#<%= DDLEducationLevel.ClientID %>').val(rowData.nivelEducacion);
        }

        function deletePersona(id) {
            $.ajax({
                type: "POST",
                url: "WFPersona.aspx/DeletePersona",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#tblPersonas').DataTable().ajax.reload();
                    alert("Persona eliminada exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar la persona.");
                }
            });
        }
    </script>
</asp:Content>