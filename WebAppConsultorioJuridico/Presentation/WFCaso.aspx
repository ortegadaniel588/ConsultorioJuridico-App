<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFCaso.aspx.cs" Inherits="Presentation.WFCaso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestión de Casos
        </div>
        <div class="card-body">
            <form id="FrmCaso" runat="server">
                <%-- ID del Caso --%>
                <asp:HiddenField ID="CasoID" runat="server" />

                <div class="row m-1">
                    <div class="col-4">
                        <%-- Código --%>
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Ingrese el Código"></asp:Label>
                        <asp:TextBox ID="TBCodigo" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVCode" runat="server" ControlToValidate="TBCodigo" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <%-- Nombre --%>
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Ingrese el Nombre"></asp:Label>
                        <asp:TextBox ID="TBNombre" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TBNombre" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <%-- Consultorio --%>
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Seleccione el Consultorio"></asp:Label>
                        <asp:DropDownList ID="DDLEpresa" CssClass="form-select" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFEmpresa" runat="server" ControlToValidate="DDLEpresa" InitialValue="0" ForeColor="Red" ErrorMessage="Debes seleccionar el consultorio."></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row m-1">
                    <div class="col-4">
                        <%-- Fecha de Cierre --%>
                        <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Fecha de Cierre"></asp:Label>
                        <asp:TextBox ID="TBFechacierre" CssClass="form-control" TextMode="Date" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-4">
                        <%-- Asunto --%>
                        <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Ingrese el Asunto"></asp:Label>
                        <asp:TextBox ID="TBAsunto" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TBAsunto" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <%-- Tipo --%>
                        <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="Seleccione el Tipo"></asp:Label>
                        <asp:DropDownList ID="DDLTipo" CssClass="form-select" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DDLTipo" InitialValue="0" ErrorMessage="Debes seleccionar el tipo." ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row m-1">
                    <div class="col-4">
                        <%-- Estado --%>
                        <asp:Label ID="Label7" CssClass="form-label" runat="server" Text="Seleccione el Estado"></asp:Label>
                        <asp:DropDownList ID="DDLEstado" CssClass="form-select" runat="server">
                            <asp:ListItem Text="Seleccione" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DDLEstado" InitialValue="0" ForeColor="Red" ErrorMessage="Debes seleccionar la complejidad."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <%-- Complejidad --%>
                        <asp:Label ID="Label8" CssClass="form-label" runat="server" Text="Seleccione la Complejidad"></asp:Label>
                        <asp:DropDownList ID="DDLComplejidad" CssClass="form-select" runat="server">
                            <asp:ListItem Text="Seleccione" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Alta" Value="alta"></asp:ListItem>
                            <asp:ListItem Text="Media" Value="media"></asp:ListItem>
                            <asp:ListItem Text="Baja" Value="baja"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DDLComplejidad" InitialValue="0" ForeColor="Red" ErrorMessage="Debes seleccionar la complejidad."></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-4">
                        <%-- Abogado --%>
                        <asp:Label ID="Label9" CssClass="form-label" runat="server" Text="Seleccione un Abogado"></asp:Label>
                        <asp:DropDownList ID="DDLEmpleado" CssClass="form-select" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DDLEmpleado" InitialValue="" ForeColor="Red" ErrorMessage="Debes seleccionar un abogado."></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row m-1">
                    <div class="col">
                        <%-- Botones Guardar y Actualizar --%>
                        <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnUpdate" CssClass="btn btn-primary" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
                        <asp:Label ID="LblMsj" CssClass="form-label" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <div class="card m-1">
        <asp:Panel ID="PanelAdmin" runat="server">
            <div class="card-header">
                Lista de Casos
            </div>
            <div class="table-responsive">
                <table id="casosTable" class="table table-hover display" style="width: 100%">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Código</th>
                            <th>Nombre</th>
                            <th>FKEmpresa</th>
                            <th>Empresa</th>
                            <th>Fecha Apertura</th>
                            <th>Fecha Cierre</th>
                            <th>Asunto</th>
                            <th>FkTipo</th>
                            <th>Tipo</th>
                            <th>FkEstado</th>
                            <th>Estado</th>
                            <th>Complejidad</th>
                            <th>FkEmpleado</th>
                            <th>Abogado</th>
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
        $(document).ready(function () {
            $('#casosTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFCaso.aspx/ListCasos",// Se invoca el WebMethod Listar casos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de caso del resultado
                    }
                },
                "columns": [
                    { "data": "CasoID" },
                    { "data": "Codigo" },
                    { "data": "Nombre" },
                    { "data": "FKEmpresa", "visible": false },
                    { "data": "Empresa" },
                    { "data": "Fechaapertura" },
                    { "data": "Fechacierra" },
                    { "data": "Asunto" },
                    { "data": "FKTipo", "visible": false },
                    { "data": "Tipo" },
                    { "data": "FKEstado", "visible": false },
                    { "data": "Estado" },
                    { "data": "Complejidad" },
                    { "data": "FKEmpleado", "visible": false },
                    { "data": "Empleado" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.CasoID}">Editar</button>
                              <button class="delete-btn" data-id="${row.CasoID}">Eliminar</button>
                              <button class="expediente-btn" data-id="${row.CasoID}">Expedientes</button>`;
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
            $('#casosTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#casosTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadCasoData(rowData);
            });

            // Eliminar un caso
            $('#casosTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del caso
                if (confirm("¿Estás seguro de que deseas eliminar este caso?")) {
                    deleteCaso(id);// Invoca a la función para eliminar el caso
                }
            });
            // Extraer id caso para asignar un expediente
            $('#casosTable').on('click', '.expediente-btn', function () {
                const id = $(this).data('id');// Obtener el ID del caso
                asignarExpediente(id);// Invoca a la función para eliminar el caso

            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadCasoData(rowData) {
            $('#<%= CasoID.ClientID %>').val(rowData.CasoID);
            $('#<%= TBCodigo.ClientID %>').val(rowData.Codigo);
            $('#<%= TBNombre.ClientID %>').val(rowData.Nombre);
            $('#<%= DDLEpresa.ClientID %>').val(rowData.FKEmpresa);
            $('#<%= TBFechacierre.ClientID %>').val(rowData.Fechacierra);
            $('#<%= TBAsunto.ClientID %>').val(rowData.Asunto);
            $('#<%= DDLTipo.ClientID %>').val(rowData.FKTipo);
            $('#<%= DDLEstado.ClientID %>').val(rowData.FKEstado);
            $('#<%= DDLComplejidad.ClientID %>').val(rowData.Complejidad);
            $('#<%= DDLEmpleado.ClientID %>').val(rowData.FKEmpleado);
        }

        // Función para eliminar un caso
        function deleteCaso(id) {
            $.ajax({
                type: "POST",
                url: "WFCaso.aspx/DeleteCaso",// Se invoca el WebMethod Eliminar un caso
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#casosTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Caso eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el caso.");
                }
            });

        }

        // Función para asignar un expediente
        function asignarExpediente(id) {
            $.ajax({
                type: "POST",
                url: "WFAsignarExpediente.aspx/extraerIdCaso", // Se invoca el WebMethod extraer id caso
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#casosTable').DataTable().ajax.reload(); // Recargar la tabla después de extraer id
                    // Redirigir usando la URL que recibimos del servidor
                    // Asegúrate de que la URL esté correctamente especificada
                    window.location.href = "WFAsignarExpediente.aspx"; // Redirige a la página deseada
                },
                error: function () {
                    alert("Error al asignar el expediente.");
                }
            });
        }


            
    </script>
</asp:Content>
