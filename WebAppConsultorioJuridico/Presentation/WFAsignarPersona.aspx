<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAsignarPersona.aspx.cs" Inherits="Presentation.WFAsignarPersona" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Asignación de implicados caso:
            <asp:Label ID="LBNombrecaso" runat="server" Text=""></asp:Label>
        </div>
        <div class="card-body">
            <form id="FrmAsignarPersona" runat="server">
                <%--Asignar implicados ID--%>
                <asp:HiddenField ID="CasoHasPersonaID" runat="server" />
                <div class="row m-1">
                    <div class="col-6">
                        <%--Caso--%>
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Caso seleccionado"></asp:Label>
                        <asp:DropDownList ID="DDLCaso_idcaso" CssClass="form-select" runat="server" Style="pointer-events: none;"></asp:DropDownList>
                        <%--Valida que se seleccione una empresa--%>
                        <asp:RequiredFieldValidator ID="RFVCaso_idcaso" runat="server" ControlToValidate="DDLCaso_idcaso" InitialValue="" ErrorMessage="Debes seleccionar un implicado." ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-6">
                        <%--Persona--%>
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Seleccione el implicado"></asp:Label>
                        <asp:DropDownList ID="DDLPersona_idpersona" CssClass="form-select" runat="server"></asp:DropDownList>
                        <%--Valida que se seleccione una red social--%>
                        <asp:RequiredFieldValidator ID="RFVPersona_idpersona" runat="server" ControlToValidate="DDLPersona_idpersona" InitialValue="" ErrorMessage="Debes seleccionar una Red Social." ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row m-1">
                    <div class="col">
                        <%--Botones Guardar y Actualizar--%>
                        <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnUpdate" runat="server" CssClass="btn btn-primary" Text="Actualizar" OnClick="BtnUpdate_Click" />
                        <asp:Label ID="LblMsj" CssClass="form-label" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <div class="card m-1">
        <asp:Panel ID="PanelAdmin" runat="server">

            <div class="card-header">
                Lista de Asignaciones de Redes Sociales
            </div>
            <div class="table-responsive">
                <%--Lista de Asignaciones de Redes Sociales--%>
                <table id="casoHasPersonaList" class="table table-hover display" style="width: 100%">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>FkCaso</th>
                            <th>Caso</th>
                            <th>FKPersona</th>
                            <th>Nombres</th>
                            <th>Apellidos</th>
                            <th>Tipo Doc</th>
                            <th>Documento</th>
                            <th>Género</th>
                            <th>Estado Civil</th>
                            <th>Lugar Nacimiento</th>
                            <th>Fecha Nacimiento</th>
                            <th>Teléfono</th>
                            <th>Teléfono Alternativo</th>
                            <th>Correo</th>
                            <th>Dirección</th>
                            <th>Estrato</th>
                            <th>Ocupación</th>
                            <th>Nivel Escolaridad</th>
                            <th>Opciones</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </asp:Panel>
    </div>


    <script src="resources/js/datatables.min.js"></script>

    <script type="text/javascript">
        document.getElementById('<%= DDLCaso_idcaso.ClientID %>').setAttribute('readonly', true);
        document.getElementById('<%= DDLCaso_idcaso.ClientID %>').style.backgroundColor = '#e9ecef';

        $(document).ready(function () {
            $('#casoHasPersonaList').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFAsignarPersona.aspx/listCasoHasPersona",// Se invoca el WebMethod Listar Productos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de asignarredessociales del resultado
                    }
                },
                "columns": [
                    { "data": "CasoHasPersonaID" },
                    { "data": "FkCaso", "visible": false },
                    { "data": "Caso", "visible": false }, // Columna para mostrar el nombre del caso
                    { "data": "FKPersona", "visible": false },
                    { "data": "Nombres" },
                    { "data": "Apellidos" },
                    { "data": "TipoDocumento", "visible": true },
                    { "data": "Documento", "visible": true },
                    { "data": "Genero", "visible": true },
                    { "data": "EstadoCivil", "visible": true },
                    { "data": "LugarNacimiento", "visible": true },
                    { "data": "FechaNacimiento" },
                    { "data": "Telefono", "visible": true },
                    { "data": "TelefonoAlternativo", "visible": true },
                    { "data": "Correo", "visible": true },
                    { "data": "Direccion", "visible": true },
                    { "data": "Estrato", "visible": true },
                    { "data": "Ocupacion", "visible": true },
                    { "data": "NivelEscolaridad", "visible": true },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.CasoHasPersonaID}">Editar</button>
                              <button class="btn btn-link btn-lg text-danger px-0 delete-btn" data-id="${row.CasoHasPersonaID}">Eliminar</button>`;
                        }
                    }
                ],
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página",
                    "zeroRecords": "No se encontraron resultados",
                    "info": "Mostrando página _PAGE_ de _PAGES_",
                    "infoEmpty": "No hay registros disponibles",
                    "infoFiltered": "(filtrado de _MAX_ registros totales)",
                    "search": "Buscar:",
                    "paginate": {
                        "first": "Primero",
                        "last": "Último",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                }

            });

            // Editar un caso
            $('#casoHasPersonaList').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#casoHasPersonaList').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadCasoHasPersonaData(rowData);
            });

            // Eliminar un caso
            $('#casoHasPersonaList').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del caso
                if (confirm("¿Estás seguro de que deseas eliminar este implicado?")) {
                    deleteCasoHasPersona(id);// Invoca a la función para eliminar el asignarredsocial
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadCasoHasPersonaData(rowData) {
            $('#<%= CasoHasPersonaID.ClientID %>').val(rowData.CasoHasPersonaID);
            $('#<%= DDLCaso_idcaso.ClientID %>').val(rowData.FkCaso);
            $('#<%= DDLPersona_idpersona.ClientID %>').val(rowData.FKPersona);
        }

        // Función para eliminar un producto
        function deleteCasoHasPersona(id) {
            $.ajax({
                type: "POST",
                url: "WFCasoHasPersona.aspx/DeleteCasoHasPersona",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#casoHasPersonaList').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Implicado eliminada exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el implicado.");
                }
            });
        }
    </script>
</asp:Content>
