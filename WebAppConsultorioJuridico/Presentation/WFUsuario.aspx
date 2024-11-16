﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeFile="WFUsuario.aspx.cs" Inherits="Presentation.WFUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <%--Id--%>
        <asp:HiddenField ID="HFUserId" runat="server" />
        <%--Correo--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el correo"></asp:Label>
        <asp:TextBox ID="TBMail" runat="server" TextMode="Email"></asp:TextBox><br />
        <%--Contraseña--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la contraseña"></asp:Label>
        <asp:TextBox ID="TBContrasena" runat="server"
            TextMode="Password"></asp:TextBox><br />
        <%--Estados--%>
        <asp:Label ID="Label3" runat="server" Text="Estado"></asp:Label>
        <asp:DropDownList ID="DDLState" runat="server">
            <asp:ListItem Value="0">Seleccione</asp:ListItem>
            <asp:ListItem Value="Activo">Activo</asp:ListItem>
            <asp:ListItem Value="Inactivo">Inactivo</asp:ListItem>
        </asp:DropDownList><br />
        <%-- Fecha--%>
        <asp:Label ID="Label4" runat="server" Text="Fecha de creación"></asp:Label>
        <asp:TextBox ID="TBDate" runat="server" TextMode="Date"></asp:TextBox>
        <br />
        <%--Rol--%>
        <asp:Label ID="Label5" runat="server" Text="Rol"></asp:Label>
        <asp:DropDownList ID="DDLRol" runat="server"></asp:DropDownList>
        <br />
        <%--Empleado--%>
        <asp:Label ID="Label6" runat="server" Text="Persona"></asp:Label>
        <asp:DropDownList ID="DDLPersona" runat="server"></asp:DropDownList>
        <br />
        <%--Botones--%>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <br />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </form>
    <%--Lista de Productos--%>
    <h2>Lista de Usuarios</h2>
    <table id="usersTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Correo</th>
                <th>Contraseña</th>
                <th>Salt</th>
                <th>Estado</th>
                <th>Fecha de Creación</th>
                <th>FkRol</th>
                <th>Rol</th>
                <th>FkPersona</th>
                <th>Persona</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <%--Usuarios--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#usersTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFUsers.aspx/ListUsers",// Se invoca el WebMethod Listar Usuarios
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de productos del resultado
                    }
                },
                "columns": [
                    { "data": "UserID" },
                    { "data": "Mail" },
                    { "data": "Password" },
                    { "data": "Salt" },
                    { "data": "State" },
                    { "data": "Date" },
                    { "data": "FkRol", "visible": false },
                    { "data": "NameRol" },
                    { "data": "FkEmployee", "visible": false },
                    { "data": "NameEmployee" },
                    {
                        "data": null,
                        "render": function (row) {
                            //alert(JSON.stringify(row, null, 2));
                            return `<button class="edit-btn" data-id="${row.UserID}">Editar</button>`;
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

            // Editar un producto
            $('#usersTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#usersTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadUsersData(rowData);
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadUsersData(rowData) {
            $('#<%= HFUserId.ClientID %>').val(rowData.UserID);
            $('#<%= TBMail.ClientID %>').val(rowData.Mail);
            $('#<%= DDLState.ClientID %>').val(rowData.State);
            $('#<%= TBDate.ClientID %>').val(rowData.Date);
            $('#<%= DDLRol.ClientID %>').val(rowData.FkRol);
            $('#<%= DDLPersona.ClientID %>').val(rowData.FkPersona);
        }
    </script>
</asp:Content>
