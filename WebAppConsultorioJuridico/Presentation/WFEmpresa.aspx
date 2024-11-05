<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEmpresa.aspx.cs" Inherits="Presentation.WFEmpresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <asp:TextBox ID="TBid" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Número nit--%> 
        <asp:Label ID="Label1" runat="server" Text="Ingresa el número nit"></asp:Label>
        <asp:TextBox ID="TBNumeronit" runat="server" />
        <br />
        <%--Nombre--%>
        <asp:Label ID="Label2" runat="server" Text="Ingresa el nombre"></asp:Label>
        <asp:TextBox ID="TBNombre" runat="server" />
        <br />
        <%--Misión--%>
        <asp:Label ID="Label3" runat="server" Text="Ingresa la misión"></asp:Label>
        <asp:TextBox ID="TBMision" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Visión--%>
        <asp:Label ID="Label5" runat="server" Text="Ingresa la visión"></asp:Label>
        <asp:TextBox ID="TBVision" runat="server" Visible="false"></asp:TextBox>
        <br />  
        <%--Dirreción--%>
        <asp:Label ID="Label6" runat="server" Text="Ingresa la dirreción"></asp:Label>
        <asp:TextBox ID="TBdirrecion" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Teléfono--%>
        <asp:Label ID="Label7" runat="server" Text="Ingresa el teléfono"></asp:Label>
        <asp:TextBox ID="TBTlefono" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Teléfono2--%>
        <asp:Label ID="Label8" runat="server" Text="Ingresa el teléfono2"></asp:Label>
        <asp:TextBox ID="TBTelefono2" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Correo--%>
        <asp:Label ID="Label9" runat="server" Text="Ingresa el correo"></asp:Label>
        <asp:TextBox ID="TBCorreo" runat="server" Visible="false"></asp:TextBox>
    
    
    </div>
    <div>
        <%--Botones Guardar y Actualizar--%>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" />
        <br />
        <asp:Label ID="LblMsj" runat="server" Text=""></asp:Label>
    </div>
    <br />
    
    
    <%--Tabla Empresa--%>
    <div>
        <asp:GridView ID="GVEmpresa" runat="server" CssClass="table table-hover" OnSelectedIndexChanged="GVEmpresa_SelectedIndexChanged"
            DataKeyNames="idempresa" OnRowDeleting="GVEmpresa_RowDeleting">
            <Columns>
                <asp:BoundField DataField="numeronit" HeaderText="Numero nit"/>
                <asp:BoundField DataField="nombre" HeaderText="Numero nombre"/>
                <asp:BoundField DataField="mision" HeaderText="Numero mision"/>
                <asp:BoundField DataField="vision" HeaderText="Numero vision"/>
                <asp:BoundField DataField="dirrecion" HeaderText="Numero dirreción"/>
                <asp:BoundField DataField="telefono" HeaderText="Numero teléfono"/>
                <asp:BoundField DataField="telefono2" HeaderText="Numero teléfono 2"/>
                <asp:BoundField DataField="correo" HeaderText="Numero correo"/>

                <asp:CommandField ShowSelectButton="true" />
                <asp:CommandField ShowDeleteButton="false" />
            </Columns>

        </asp:GridView>
    </div>

</asp:Content>
