<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPersona.aspx.cs" Inherits="Presentation.WFPersona" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WFPersona.aspx.cs" Inherits="Presentation.WFPersona" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h2>Gestión de Personas</h2>
        <hr />
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <input type="hidden" id="TBId" runat="server" />
                    
                    <label for="TBNombres">Nombres:</label>
                    <asp:TextBox ID="TBNombres" runat="server" CssClass="form-control"></asp:TextBox>
                    
                    <label for="TBApellidos">Apellidos:</label>
                    <asp:TextBox ID="TBApellidos" runat="server" CssClass="form-control"></asp:TextBox>
                    
                    <label for="TBTipoDocumento">Tipo de Documento:</label>
                    <asp:TextBox ID="TBTipoDocumento" runat="server" CssClass="form-control"></asp:TextBox>
                    
                    <label for="TBDocumento">Número de Documento:</label>
                    <asp:TextBox ID="TBDocumento" runat="server" CssClass="form-control"></asp:TextBox>
                    
                    <label for="TBGenero">Género:</label>
                    <asp:TextBox ID="TBGenero" runat="server" CssClass="form-control"></asp:TextBox>
                    
                    <label for="TBEstadoCivil">Estado Civil:</label>
                    <asp:TextBox ID="TBEstadoCivil" runat="server" CssClass="form-control"></asp:TextBox>
                    
                    <label for="TBLugarNacimiento">Lugar de Nacimiento:</label>
                    <asp:TextBox ID="TBLugarNacimiento" runat="server" CssClass="form-control"></asp:TextBox>
                    
                    <label for="TBFechaNacimiento">Fecha de Nacimiento:</label>
                    <asp:TextBox ID="TBFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    
                    <label for="TBTelefono">Teléfono:</label>
                    <asp:TextBox ID="TBTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                    
                    <label for="TBCelular">Celular:</label>
                    <asp:TextBox ID="TBCelular" runat="server" CssClass="form-control"></asp:TextBox>
                    
                    <label for="TBCorreo">Correo Electrónico:</label>
                    <asp:TextBox ID="TBCorreo" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                    
                    <label for="TBDireccion">Dirección:</label>
                    <asp:TextBox ID="TBDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                    
                    <label for="TBEstado">Estado:</label>
                    <asp:TextBox ID="TBEstado" runat="server" CssClass="form-control"></asp:TextBox>
                    
                    <label for="TBOcupacion">Ocupación:</label>
                    <asp:TextBox ID="TBOcupacion" runat="server" CssClass="form-control"></asp:TextBox>
                    
                    <label for="TBNivelEducacion">Nivel de Educación:</label>
                    <asp:TextBox ID="TBNivelEducacion" runat="server" CssClass="form-control"></asp:TextBox>
                    
                    <br />
                    <asp:Button ID="BtnSave" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="BtnSave_Click" />
                    <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CssClass="btn btn-success" OnClick="BtnUpdate_Click" />
                    <br /><br />
                    <asp:Label ID="LblMsg" runat="server" CssClass="text-info"></asp:Label>
                </div>
            </div>
            
            <div class="col-md-8">
                <table id="tblPersonas" class="table table-striped table-bordered">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Nombres</th>
                            <th>Apellidos</th>
                            <th>Documento</th>
                            <th>Teléfono</th>
                            <th>Correo</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>
                </table>
            </div>
        </div>
    </div>

    <!-- Script para DataTable -->
    <script type="text/javascript">
        $(document).ready(function () {
            var table = $("#tblPersonas").DataTable({
                "ajax": {
                    "url": "WFPersona.aspx/ListPersonas",
                    "type": "POST",
                    "datatype": "json",
                    "contentType": "application/json; charset=utf-8",
                    "dataSrc": function (json) {
                        return json.d.data;
                    }
                },
                "columns": [
                    { "data": "idpersona" },
                    { "data": "nombres" },
                    { "data": "apellidos" },
                    { "data": "documento" },
                    { "data": "telefono" },
                    { "data": "correo" },
                    {
                        "data": "idpersona",
                        "render": function (data) {
                            return '<button type="button" class="btn btn-primary btn-sm" onclick="EditPersona(' + data + ')"><i class="fas fa-edit"></i></button> ' +
                                   '<button type="button" class="btn btn-danger btn-sm" onclick="DeletePersona(' + data + ')"><i class="fas fa-trash"></i></button>';
                        }
                    }
                ]
            });
        });

        function EditPersona(id) {
            $.ajax({
                type: "POST",
                url: "WFPersona.aspx/GetPersona",
                data: JSON.stringify({ id: id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var persona = response.d;
                    $("#MainContent_TBId").val(persona.idpersona);
                    $("#MainContent_TBNombres").val(persona.nombres);
                    $("#MainContent_TBApellidos").val(persona.apellidos);
                    $("#MainContent_TBTipoDocumento").val(persona.tipodocumento);
                    $("#MainContent_TBDocumento").val(persona.documento);
                    $("#MainContent_TBGenero").val(persona.genero);
                    $("#MainContent_TBEstadoCivil").val(persona.estadocivil);
                    $("#MainContent_TBLugarNacimiento").val(persona.lugarNacimiento);
                    $("#MainContent_TBFechaNacimiento").val(persona.fechaNacimiento.split('T')[0]);
                    $("#MainContent_TBTelefono").val(persona.telefono);
                    $("#MainContent_TBCelular").val(persona.celular);
                    $("#MainContent_TBCorreo").val(persona.correo);
                    $("#MainContent_TBDireccion").val(persona.direccion);
                    $("#MainContent_TBEstado").val(persona.estado);
                    $("#MainContent_TBOcupacion").val(persona.ocupacion);
                    $("#MainContent_TBNivelEducacion").val(persona.nivelEducacion);
                }
            });
        }

        function DeletePersona(id) {
            if (confirm("¿Está seguro que desea eliminar esta persona?")) {
                $.ajax({
                    type: "POST",
                    url: "WFPersona.aspx/DeletePersona",
                    data: JSON.stringify({ id: id }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d) {
                            alert("Persona eliminada exitosamente");
                            $("#tblPersonas").DataTable().ajax.reload();
                        } else {
                            alert("Error al eliminar la persona");
                        }
                    }
                });
            }
        }
    </script>
</asp:Content>