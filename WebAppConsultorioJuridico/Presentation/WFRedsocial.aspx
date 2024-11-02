<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRedsocial.aspx.cs" Inherits="Presentation.WFRedsocial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <asp:TextBox ID="TBId" runat="server" Visible="false"></asp:TextBox><br />
        <asp:Label ID="Label1" runat="server" Text="Ingrese el nombre"></asp:Label>
        <asp:TextBox ID="TBNombre" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label2" runat="server" Text="Ingrese la descripción"></asp:Label>
        <asp:TextBox ID="TBDescripcion" runat="server"></asp:TextBox><br />
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" /><br />
        <asp:Label ID="LblMsj" runat="server" Text=""></asp:Label><br />
    </div>
    <div>
        <asp:GridView ID="GVRedsocial" runat="server" CssClass="table table-hover" OnSelectedIndexChanged="GVRedsocial_SelectedIndexChanged"
            DataKeyNames="reds_id" OnRowDeleting="GVRedsocial_RowDeleting">
            <Columns>
                <asp:CommandField ShowSelectButton="true" />
                <asp:CommandField ShowDeleteButton="true" />
            </Columns>

        </asp:GridView>
    </div>
</asp:Content>
