<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFCita.aspx.cs" Inherits="Presentation.WFCita" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="TBId" runat="server" Visible="false"></asp:TextBox>
    <%--Horario--%>
    <asp:Label ID="Label1" runat="server" Text="Seleccione el Horario"></asp:Label>
    <asp:DropDownList ID="DDLHorarios" runat="server"></asp:DropDownList>
    <br />
    <%--Asunto--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese el Asunto"></asp:Label>
    <asp:TextBox ID="TBAsunto" runat="server"></asp:TextBox>
    <br />
    <%--Estado--%>
    <asp:Label ID="Label3" runat="server" Text="Seleccione el Estado"></asp:Label>
    <asp:DropDownList ID="DDLEstado" runat="server">
        <asp:ListItem Value="programada">Programada</asp:ListItem>
        <asp:ListItem Value="confirmada">Confirmada</asp:ListItem>
        <asp:ListItem Value="pendiente">Pendiente</asp:ListItem>
        <asp:ListItem Value="cancelada">Cancelada</asp:ListItem>
        <asp:ListItem Value="reprogramada">Reprogramada</asp:ListItem>
        <asp:ListItem Value="no asistida">No Asistida</asp:ListItem>
        <asp:ListItem Value="en curso">En Curso</asp:ListItem>
        <asp:ListItem Value="completada">Completada</asp:ListItem>
    </asp:DropDownList>
    <br />
    <%--Botones Guardar y Actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%--Lista de Citas--%>
    <div>
        <asp:GridView ID="GVCitas" runat="server" AutoGenerateColumns="False" OnRowCommand="GVCitas_RowCommand">
            <Columns>
                <asp:BoundField DataField="idcita" HeaderText="ID" />
                <asp:BoundField DataField="horario_idhorario" HeaderText="Horario" />
                <asp:BoundField DataField="asunto" HeaderText="Asunto" />
                <asp:BoundField DataField="estado" HeaderText="Estado" />
                <asp:ButtonField ButtonType="Button" CommandName="Edit" Text="Editar" />
                <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Eliminar" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>