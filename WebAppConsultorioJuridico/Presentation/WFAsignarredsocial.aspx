<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAsignarredsocial.aspx.cs" Inherits="Presentation.WFAsignarredsocial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:TextBox ID="TBid" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Empresa_idempresa--%>
        <asp:Label ID="Label1" runat="server" Text="Seleccione la empresa"></asp:Label>
        <asp:DropDownList ID="DDLEmpresa_idempresa" runat="server" CssClass="form-select"></asp:DropDownList>
        <br />
        <%--Redsocial_idredsocial--%>
        <asp:Label ID="Label2" runat="server" Text="Seleccione la red social"></asp:Label>
        <asp:DropDownList ID="DDLRedsocial_idredsocial" runat="server" CssClass="form-select"></asp:DropDownList>
        <br />
        <%--Url--%>
        <asp:Label ID="Label3" runat="server" Text="Ingrese la url del perfil"></asp:Label>
        <asp:TextBox ID="TBUrl" runat="server" Visible="false"></asp:TextBox>
        <br />
    </div>

    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" />
        <br />
        <asp:Label ID="LblMsj" runat="server" Text=""></asp:Label>
    </div>
    <br />

    <div>
        <asp:GridView ID="GVAsignarRedSocial" runat="server" CssClass="table table-hover" OnSelectedIndexChanged="GVAsignarRedSocial_SelectedIndexChanged"
            DataKeyNames="idasignarredsocial" OnRowDeleting="GVAsignarRedSocial_RowDeleting">
            <Columns>
                <asp:BoundField DataField="empresa_idempresa" HeaderText="Empresa" />
                <asp:BoundField DataField="redsocial_idredsocial" HeaderText="Red social" />
                <asp:BoundField DataField="url" HeaderText="Url" />

                <asp:CommandField ShowSelectButton="true" />
                <asp:CommandField ShowDeleteButton="false" />
            </Columns>

        </asp:GridView>
    </div>
</asp:Content>
