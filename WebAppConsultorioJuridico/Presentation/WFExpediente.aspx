<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFExpediente.aspx.cs" Inherits="Presentation.WFExpediente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:TextBox ID="TBid" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Caso--%>
        <asp:Label ID="Label1" runat="server" Text="Seleccione el caso"></asp:Label>
        <asp:DropDownList ID="DDCaso_idcaso" runat="server" CssClass="form-select"></asp:DropDownList>
        <br />
        <%--Código--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese el código"></asp:Label>
        <asp:TextBox ID="TBCodigo" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Fecha de creación--%>
        <asp:Label ID="Label3" runat="server" Text="Seleccione la fecha"></asp:Label>
        <asp:TextBox ID="TBCracionfecha" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Acción realizada--%>
        <asp:Label ID="Label4" runat="server" Text="Ingrese la acción realizada"></asp:Label>
        <asp:TextBox ID="TBAccionrealizada" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Razón--%>
        <asp:Label ID="Label5" runat="server" Text="Ingrese la razón"></asp:Label>
        <asp:TextBox ID="TBRazon" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Relevancia--%>
        <asp:Label ID="Label6" runat="server" Text="Ingrese la relevancia"></asp:Label>
        <asp:TextBox ID="TBRelevancia" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Evidencia--%>
        <asp:Label ID="Label7" runat="server" Text="Ingrese la evidencia"></asp:Label>
        <asp:TextBox ID="TBEvidencia" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Comentario--%>
        <asp:Label ID="Label8" runat="server" Text="Ingrese un comentario"></asp:Label>
        <asp:TextBox ID="TBComentario" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Estado--%>
        <asp:DropDownList ID="DDLEstado" runat="server">
            <asp:ListItem Text="pendiente" Value="1"></asp:ListItem>
            <asp:ListItem Text="'en progreso" Value="2"></asp:ListItem>
            <asp:ListItem Text="finalizada" Value="3"></asp:ListItem>
            <asp:ListItem Text="cancelada" Value="3"></asp:ListItem>
        </asp:DropDownList>
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
        <asp:GridView ID="GVExpediente" runat="server" CssClass="table table-hover" OnSelectedIndexChanged="GVExpediente_SelectedIndexChanged"
            DataKeyNames="idasignarredsocial" OnRowDeleting="GVExpediente_RowDeleting">
            <Columns>
                <asp:BoundField DataField="caso_idcaso" HeaderText="Caso" />
                <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                <asp:BoundField DataField="cracionfecha" HeaderText="Cracionfecha" />
                <asp:BoundField DataField="accionrealizada" HeaderText="Accionrealizada" />
                <asp:BoundField DataField="razon" HeaderText="Razon" />
                <asp:BoundField DataField="relevancia" HeaderText="Relevancia" />
                <asp:BoundField DataField="evidencia" HeaderText="Evidencia" />
                <asp:BoundField DataField="comentario" HeaderText="Comentario" />
                <asp:BoundField DataField="estado" HeaderText="Estado" />

                <asp:CommandField ShowSelectButton="true" />
                <asp:CommandField ShowDeleteButton="false" />
            </Columns>

        </asp:GridView>
    </div>
</asp:Content>
