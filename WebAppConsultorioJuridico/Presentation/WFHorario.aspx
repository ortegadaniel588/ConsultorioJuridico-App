<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFHorario.aspx.cs" Inherits="Presentation.WFHorario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="TBId" runat="server" Visible="false"></asp:TextBox>
    <%--Empleado--%>
    <asp:Label ID="Label1" runat="server" Text="Seleccione el Empleado"></asp:Label>
    <asp:DropDownList ID="DDLEmpleados" runat="server"></asp:DropDownList>
    <br />
    <%--Fecha--%>
    <asp:Label ID="Label2" runat="server" Text="Seleccione la Fecha"></asp:Label>
    <asp:TextBox ID="TBFecha" runat="server"></asp:TextBox>
    <br />
    <%--Hora de Inicio--%>
    <asp:Label ID="Label3" runat="server" Text="Ingrese la Hora de Inicio"></asp:Label>
    <asp:TextBox ID="TBHoraInicio" runat="server"></asp:TextBox>
    <br />
    <%--Hora de Fin--%>
    <asp:Label ID="Label4" runat="server" Text="Ingrese la Hora de Fin"></asp:Label>
    <asp:TextBox ID="TBHoraFin" runat="server"></asp:TextBox>
    <br />
    <%--Estado--%>
    <asp:Label ID="Label5" runat="server" Text="Seleccione el Estado"></asp:Label>
    <asp:DropDownList ID="DDLEstado" runat="server"></asp:DropDownList>
    <br />
    <%--Botones Guardar y Actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%--Lista de Horarios--%>
    <div>
        <asp:GridView ID="GVHorarios" runat="server"></asp:GridView>
    </div>
</asp:Content>