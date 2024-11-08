<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFUsuario.aspx.cs" Inherits="Presentation.WFUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="TBId" runat="server" Visible="false"></asp:TextBox>
    <%--Usuario--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese el Usuario"></asp:Label>
    <asp:TextBox ID="TBUsuario" runat="server"></asp:TextBox>
    <br />
    <%--Contraseña--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese la Contraseña"></asp:Label>
    <asp:TextBox ID="TBContrasena" runat="server" TextMode="Password"></asp:TextBox>
    <br />
    <%--Rol--%>
    <asp:Label ID="Label3" runat="server" Text="Seleccione el Rol"></asp:Label>
    <asp:DropDownList ID="DDLRol" runat="server"></asp:DropDownList>
    <br />
    <%--Persona--%>
    <asp:Label ID="Label4" runat="server" Text="Seleccione la Persona"></asp:Label>
    <asp:DropDownList ID="DDLPersona" runat="server"></asp:DropDownList>
    <br />
    <%--Estado--%>
    <asp:Label ID="Label5" runat="server" Text="Seleccione el Estado"></asp:Label>
    <asp:DropDownList ID="DDLEstado" runat="server">
        <asp:ListItem Value="activo">Activo</asp:ListItem>
        <asp:ListItem Value="inactivo">Inactivo</asp:ListItem>
    </asp:DropDownList>
    <br />
    <%--Botones Guardar y Actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%--Lista de Usuarios--%>
    <div>
        <asp:GridView ID="GVUsuarios" runat="server" AutoGenerateColumns="False" OnRowCommand="GVUsuarios_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                <asp:BoundField DataField="rol" HeaderText="Rol" />
                <asp:BoundField DataField="persona" HeaderText="Persona" />
                <asp:BoundField DataField="estado" HeaderText="Estado" />
                <asp:ButtonField ButtonType="Button" CommandName="Edit" Text="Editar" />
                <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Eliminar" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>