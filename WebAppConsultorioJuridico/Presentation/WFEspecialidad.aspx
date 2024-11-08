<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEspecialidad.aspx.cs" Inherits="Presentation.WFEspecialidad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="TBId" runat="server" Visible="false"></asp:TextBox>
    <%--Nombre--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese el Nombre"></asp:Label>
    <asp:TextBox ID="TBNombre" runat="server"></asp:TextBox>
    <br />
    <%--Descripción--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese la Descripción"></asp:Label>
    <asp:TextBox ID="TBDescripcion" runat="server"></asp:TextBox>
    <br />
    <%--Botones Guardar y Actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%--Lista de Especialidades--%>
    <div>
        <asp:GridView ID="GVEspecialidades" runat="server" AutoGenerateColumns="False" OnRowCommand="GVEspecialidades_RowCommand">
            <Columns>
                <asp:BoundField DataField="idespecialidad" HeaderText="ID" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                <asp:ButtonField ButtonType="Button" CommandName="Edit" Text="Editar" />
                <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Eliminar" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>