<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFSeguimiento.aspx.cs" Inherits="Presentation.WFSeguimiento" %>

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
        <%--Fechaactu de alizacion--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la fecha de actualización"></asp:Label>
        <asp:TextBox ID="TBFechaactualizacion" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Proceso--%>
        <asp:Label ID="Label3" runat="server" Text="Ingrese el proceso"></asp:Label>
        <asp:TextBox ID="TBProceso" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Descripción--%>
        <asp:Label ID="Label4" runat="server" Text="Ingrese la acción Descripción"></asp:Label>
        <asp:TextBox ID="TBDescripcion" runat="server" Visible="false"></asp:TextBox>
        <br />
        <%--Estado--%>
        <asp:Label ID="Label5" runat="server" Text="Ingrese el estado"></asp:Label>
        <asp:TextBox ID="TBEstado" runat="server" Visible="false"></asp:TextBox>
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
        <asp:GridView ID="GVSeguimiento" runat="server" CssClass="table table-hover" OnSelectedIndexChanged="GVSeguimiento_SelectedIndexChanged"
            DataKeyNames="idseguimiento" OnRowDeleting="GVSeguimiento_RowDeleting">
            <Columns>
                <asp:BoundField DataField="caso_idcaso" HeaderText="Caso" />
                <asp:BoundField DataField="fechaactualizacion" HeaderText="Fecha de actualización" />
                <asp:BoundField DataField="proceso" HeaderText="Proceso" />
                <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                <asp:BoundField DataField="estado" HeaderText="Estado" />

                <asp:CommandField ShowSelectButton="true" />
                <asp:CommandField ShowDeleteButton="false" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
