<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFSeguimiento.aspx.cs" Inherits="Presentation.WFSeguimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="card m-1">
    <div class="card-header">
        Gestión de Seguimientos
    </div>
    <div class="card-body">
        <form runat="server">
            <%--ID--%>
            <asp:HiddenField ID="SeguimientoID" runat="server" />
            
            <div class="row m-1">
                <div class="col-12">
                    <%--Caso--%>
                    <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Seleccione el caso"></asp:Label>
                    <asp:DropDownList ID="DDCaso_idcaso" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
            </div>

            <div class="row m-1">
                <div class="col-6">
                    <%--Fecha de actualización--%>
                    <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Ingrese la fecha de actualización"></asp:Label>
                    <asp:TextBox ID="TBFechaactualizacion" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    <%--Valida que el TextBox esté lleno--%>
                    <asp:RequiredFieldValidator ID="RFVFecha" runat="server" ControlToValidate="TBFechaactualizacion" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                </div>

                <div class="col-6">
                    <%--Proceso--%>
                    <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Ingrese el proceso"></asp:Label>
                    <asp:TextBox ID="TBProceso" runat="server" CssClass="form-control"></asp:TextBox>
                    <%--Valida que el TextBox esté lleno--%>
                    <asp:RequiredFieldValidator ID="RFVProceso" runat="server" ControlToValidate="TBProceso" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row m-1">
                <div class="col-12">
                    <%--Descripción--%>
                    <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Ingrese la descripción"></asp:Label>
                    <asp:TextBox ID="TBDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                    <%--Valida que el TextBox esté lleno--%>
                    <asp:RequiredFieldValidator ID="RFVDescripcion" runat="server" ControlToValidate="TBDescripcion" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row m-1">
                <div class="col-12">
                    <%--Estado--%>
                    <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Seleccione el estado"></asp:Label>
                    <asp:DropDownList ID="TBEstado" runat="server" CssClass="form-select">
                        <asp:ListItem Value="0">Seleccione</asp:ListItem>
                        <asp:ListItem Text="Iniciado" Value="iniciado"></asp:ListItem>
                        <asp:ListItem Text="En trámite" Value="en trámite"></asp:ListItem>
                        <asp:ListItem Text="Suspendido" Value="suspendido"></asp:ListItem>
                        <asp:ListItem Text="Archivado" Value="archivado"></asp:ListItem>
                        <asp:ListItem Text="Resuelto" Value="resuelto"></asp:ListItem>
                        <asp:ListItem Text="Apelado" Value="apelado"></asp:ListItem>
                        <asp:ListItem Text="Ejecutado" Value="ejecutado"></asp:ListItem>
                    </asp:DropDownList>
                    <%--Valida que el DropDownList esté seleccionado--%>
                    <asp:RequiredFieldValidator ID="RFVEstado" runat="server" ControlToValidate="TBEstado" InitialValue="" ErrorMessage="Debes seleccionar un estado." ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="row m-1">
                <div class="col-12">
                    <%--Botones Guardar y Actualizar--%>
                    <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-success" Text="Guardar" OnClick="BtnSave_Click" />
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
        Lista de Seguimientos
    </div>
    <div class="table-responsive">
        <%--Lista de Seguimientos--%>
        <table id="seguimientosTable" class="table table-hover display" style="width: 100%">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>FK Caso</th>
                    <th>Código Caso</th>
                    <th>Fecha Actualización</th>
                    <th>Proceso</th>
                    <th>Descripción</th>
                    <th>Estado</th>
                    <th>Asunto</th>
                    <th>Fecha Apertura</th>
                    <th>Fecha Cierre</th>
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
            $('#seguimientosTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFSeguimiento.aspx/ListSeguimientos",// Se invoca el WebMethod Listar Productos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de productos del resultado
                    }
                },
                "columns": [
                    { "data": "SeguimientoID" },
                    { "data": "FKCaso", "visible": false },
                    { "data": "Casocode" },
                    { "data": "Fechaactualizacion" },
                    { "data": "Proceso" },
                    { "data": "Descripcion" },
                    { "data": "Estado" },
                    { "data": "Asunto" },
                    { "data": "Fechaapertura" },
                    { "data": "Fechacierre" },

                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.SeguimientoID}">Editar</button>
                                  <button class="delete-btn" data-id="${row.SeguimientoID}">Eliminar</button>`;
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

            // Editar un Seguimiento
            $('#seguimientosTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#seguimientosTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadSeguimientoData(rowData);
            });

            // Eliminar un Seguimiento
            $('#seguimientosTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del seguimiento
                if (confirm("¿Estás seguro de que deseas eliminar este seguimiento?")) {
                    deleteSeguimineto(id);// Invoca a la función para eliminar el Seguimiento
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadSeguimientoData(rowData) {
            $('#<%= SeguimientoID.ClientID %>').val(rowData.SeguimientoID);
            $('#<%= DDCaso_idcaso.ClientID %>').val(rowData.FKCaso);
            $('#<%= TBFechaactualizacion.ClientID %>').val(rowData.Fechaactualizacion);
            $('#<%= TBProceso.ClientID %>').val(rowData.Proceso);
            $('#<%= TBDescripcion.ClientID %>').val(rowData.Descripcion);
            $('#<%= TBEstado.ClientID %>').val(rowData.Estado);
        }

        // Función para eliminar un Seguimineto
        function deleteSeguimineto(id) {
            $.ajax({
                type: "POST",
                url: "WFSeguimiento.aspx/DeleteSeguimiento",// Se invoca el WebMethod Eliminar un seguimiento
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#seguimientosTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Seguimiento eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el seguimientos.");
                }
            });
        }
    </script>
</asp:Content>
