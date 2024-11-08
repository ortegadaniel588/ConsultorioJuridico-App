<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFCaso.aspx.cs" Inherits="Presentation.WFCaso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:TextBox ID="TBId" runat="server"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el código"></asp:Label>
        <asp:TextBox ID="TBCodigo" runat="server"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="Seleccione la empresa"></asp:Label>
        <asp:DropDownList ID="DDLEpresa" runat="server"></asp:DropDownList>
        <asp:Label ID="Label3" runat="server" Text="Fecha de cierre"></asp:Label>
        <asp:TextBox ID="TBFechacierre" runat="server"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Text="Ingrese el asunto"></asp:Label>
        <asp:TextBox ID="TBAsunto" runat="server"></asp:TextBox>
        <asp:Label ID="Label5" runat="server" Text="Selleccione el tipo"></asp:Label>
        <asp:DropDownList ID="DDLTipo" runat="server"></asp:DropDownList>
        <asp:Label ID="Label6" runat="server" Text="Seleccione el estado"></asp:Label>
        <asp:DropDownList ID="DDLEstado" runat="server"></asp:DropDownList>
        <asp:Label ID="Label7" runat="server" Text="Seleccione su complejidad"></asp:Label>
        <asp:DropDownList ID="DDLComplejidad" runat="server">
            <asp:ListItem Text="alta" Value="1"></asp:ListItem>
            <asp:ListItem Text="media" Value="2"></asp:ListItem>
            <asp:ListItem Text="baja" Value="3"></asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label8" runat="server" Text="Seleccione un abogado"></asp:Label>
        <asp:DropDownList ID="DDLEmpleado" runat="server"></asp:DropDownList>

    </div>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Button" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Button" />
        <asp:Label ID="LblMsj" runat="server" Text=""></asp:Label>
    </div>
    <div>
        <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover"
            DataKeyNames="idcaso" OnRowDeleting="GVCaso_RowDeleting">
            <Columns>
                <asp:BoundField DataField="_id" HeaderText="id" />
                <asp:BoundField DataField="_codigo" HeaderText="codigo" />
                <asp:BoundField DataField="_empresa" HeaderText="empresa" />
                <asp:BoundField DataField="_fechaapertura" HeaderText="fechaapertura" />
                <asp:BoundField DataField="_fechacierre" HeaderText="fechacierre" />
                <asp:BoundField DataField="_asunto" HeaderText="asunto" />
                <asp:BoundField DataField="_tipo" HeaderText="tipo" />
                <asp:BoundField DataField="_estado" HeaderText="estado" />
                <asp:BoundField DataField="_complejidad" HeaderText="complejidad" />
                <asp:BoundField DataField="_empleado" HeaderText="empleado" />

                <asp:CommandField ShowSelectButton="true" />
                <asp:CommandField ShowDeleteButton="false" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
