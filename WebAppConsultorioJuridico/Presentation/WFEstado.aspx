<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEstado.aspx.cs" Inherits="Presentation.WFEstado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <%--Id--%>
        <asp:HiddenField ID="HFEstadoID" runat="server" />

        <%--Nombre--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el Nombre"></asp:Label>
        <asp:TextBox ID="TBNombre" runat="server"></asp:TextBox>
        <br />
        <%--Descripcion--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la Descripcion"></asp:Label>
        <asp:TextBox ID="TBDescripcion" runat="server"></asp:TextBox>
        <br />
        
        <%--Botones Guardar y Actualizar--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>

     // Cargar los datos en los TextBox y DDL para actualizar
        function loadProductData(rowData) {
          $('#<%= HFEstadoID.ClientID %>').val(rowData.EstadoID);
          $('#<%= TBNombre.ClientID %>').val(rowData.Nombre);
          $('#<%= TBDescripcion.ClientID %>').val(rowData.Descripcion);
        }

    </script>
</asp:Content>
