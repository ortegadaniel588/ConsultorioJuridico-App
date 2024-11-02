<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPersona.aspx.cs" Inherits="Presentation.WFPersona" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="TBId" runat="server" Visible="false"></asp:TextBox>
    <%--Nombres--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese los Nombres"></asp:Label>
    <asp:TextBox ID="TBNombres" runat="server"></asp:TextBox>
    <br />
    <%--Apellidos--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese los Apellidos"></asp:Label>
    <asp:TextBox ID="TBApellidos" runat="server"></asp:TextBox>
    <br />
    <%--Tipo de Documento--%>
    <asp:Label ID="Label3" runat="server" Text="Seleccione el Tipo de Documento"></asp:Label>
    <asp:DropDownList ID="DDLTBTipodocumento" runat="server">
        <asp:ListItem Value="cedula">Cédula</asp:ListItem>
        <asp:ListItem Value="pasaporte">Pasaporte</asp:ListItem>
        <asp:ListItem Value="tarjeta de identidad">Tarjeta de Identidad</asp:ListItem>
    </asp:DropDownList>
    <br />
    <%--Documento--%>
    <asp:Label ID="Label4" runat="server" Text="Ingrese el Documento"></asp:Label>
    <asp:TextBox ID="TBDocumento" runat="server"></asp:TextBox>
    <br />
    <%--Género--%>
    <asp:Label ID="Label5" runat="server" Text="Seleccione el Género"></asp:Label>
    <asp:DropDownList ID="DDLTBGénero" runat="server">
        <asp:ListItem Value="masculino">Masculino</asp:ListItem>
        <asp:ListItem Value="femenino">Femenino</asp:ListItem>
        <asp:ListItem Value="prefiero no decir">Prefiero no decir</asp:ListItem>
    </asp:DropDownList>
    <br />
    <%--Estado Civil--%>
    <asp:Label ID="Label6" runat="server" Text="Seleccione el Estado Civil"></asp:Label>
    <asp:DropDownList ID="DDLTBEstadoCivil" runat="server">
        <asp:ListItem Value="soltero">Soltero</asp:ListItem>
        <asp:ListItem Value="casado">Casado</asp:ListItem>
        <asp:ListItem Value="viudo">Viudo</asp:ListItem>
        <asp:ListItem Value="union libre">Unión Libre</asp:ListItem>
    </asp:DropDownList>
    <br />
    <%--Lugar de Nacimiento--%>
    <asp:Label ID="Label7" runat="server" Text="Ingrese el Lugar de Nacimiento"></asp:Label>
    <asp:TextBox ID="TBLugarNacimiento" runat="server"></asp:TextBox>
    <br />
    <%--Fecha de Nacimiento--%>
    <asp:Label ID="Label8" runat="server" Text="Seleccione la Fecha de Nacimiento"></asp:Label>
    <asp:TextBox ID="TBFechaNacimiento" runat="server" TextMode="Date"></asp:TextBox>
    <br />
    <%--Teléfono--%>
    <asp:Label ID="Label9" runat="server" Text="Ingrese el Teléfono"></asp:Label>
    <asp:TextBox ID="TBTeléfono" runat="server"></asp:TextBox>
    <br />
    <%--Celular--%>
    <asp:Label ID="Label10" runat="server" Text="Ingrese el Celular"></asp:Label>
    <asp:TextBox ID="TBCelular" runat="server"></asp:TextBox>
    <br />
    <%--Correo--%>
    <asp:Label ID="Label11" runat="server" Text="Ingrese el Correo"></asp:Label>
    <asp:TextBox ID="TBCorreo" runat="server"></asp:TextBox>
    <br />
    <%--Dirección--%>
    <asp:Label ID="Label12" runat="server" Text="Ingrese la Dirección"></asp:Label>
    <asp:TextBox ID="TBDirección" runat="server"></asp:TextBox>
    <br />
    <%--Estado--%>
    <asp:Label ID="Label13" runat="server" Text="Seleccione el Estado"></asp:Label>
    <asp:DropDownList ID="DDLTBEstado" runat="server">
        <asp:ListItem Value="1">1</asp:ListItem>
        <asp:ListItem Value="2">2</asp:ListItem>
        <asp:ListItem Value="3">3</asp:ListItem>
        <asp:ListItem Value="4">4</asp:ListItem>
        <asp:ListItem Value="5">5</asp:ListItem>
        <asp:ListItem Value="6">6</asp:ListItem>
    </asp:DropDownList>
    <br />
    <%--Ocupación--%>
    <asp:Label ID="Label14" runat="server" Text="Ingrese la Ocupación"></asp:Label>
    <asp:TextBox ID="TBOcupación" runat="server"></asp:TextBox>
    <br />
    <%--Nivel de Educación--%>
    <asp:Label ID="Label15" runat="server" Text="Seleccione el Nivel de Educación"></asp:Label>
    <asp:DropDownList ID="DDLTBNivelEducación" runat="server">
        <asp:ListItem Value="ninguno">Ninguno</asp:ListItem>
        <asp:ListItem Value="primaria">Primaria</asp:ListItem>
        <asp:ListItem Value="secundaria">Secundaria</asp:ListItem>
        <asp:ListItem Value="técnica">Técnica</asp:ListItem>
        <asp:ListItem Value="técnológica">Técnológica</asp:ListItem>
        <asp:ListItem Value="pregrado">Pregrado</asp:ListItem>
        <asp:ListItem Value="posgrado">Posgrado</asp:ListItem>
    </asp:DropDownList>
    <br />
    <%--Botones Guardar y Actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%--Lista de Personas--%>
    <div>
        <asp:GridView ID="GVPersonas" runat="server" AutoGenerateColumns="False" OnRowCommand="GVPersonas_RowCommand">
            <Columns>
                <asp:BoundField DataField="idpersona" HeaderText="ID" />
                <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                <asp:BoundField DataField="apellidos" HeaderText="Apellidos" />
                <asp:BoundField DataField="tipodocumento" HeaderText="Tipo de Documento" />
                <asp:BoundField DataField="documento" HeaderText="Documento" />
                <asp:BoundField DataField="genero" HeaderText="Género" />
                <asp:BoundField DataField="estadocivil" HeaderText="Estado Civil" />
                <asp:BoundField DataField="lugarNacimiento" HeaderText="Lugar de Nacimiento" />
                <asp:BoundField DataField="fechaNacimiento" HeaderText="Fecha de Nacimiento" />
                <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                <asp:BoundField DataField="celular" HeaderText="Celular" />
                <asp:BoundField DataField="correo" HeaderText="Correo" />
                <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                <asp:BoundField DataField="estado" HeaderText="Estado" />
                <asp:BoundField DataField="ocupacion" HeaderText="Ocupación" />
                <asp:BoundField DataField="nivelEducacion" HeaderText="Nivel de Educación" />
                <asp:ButtonField ButtonType="Button" CommandName="Edit" Text="Editar" />
                <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Eliminar" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>