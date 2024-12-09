<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAsignarExpediente.aspx.cs" Inherits="Presentation.WFAsignarExpediente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
    <div class="card-header">
        Gestión de Expediente caso: <asp:Label ID="LBNombrecaso" runat="server" Text=""></asp:Label>
    </div>
    <div class="card-body">
        <form id="FrmAsignarExpediente" runat="server">
            <%--ID del Expediente--%>
            <asp:HiddenField ID="ExpedienteID" runat="server" />
            <div class="row m-1">
                <div class="col-6">
                    <%--Caso--%>
                    <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Caso selecionado" ></asp:Label>
                    <asp:DropDownList ID="DDCaso_idcaso" runat="server" CssClass="form-select" style="pointer-events: none;"></asp:DropDownList>
                    
                </div>
                <div class="col-6">
                    <%--Código--%>
                    <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Ingrese el código"></asp:Label>
                    <asp:TextBox ID="TBCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TBCodigo" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>

                </div>
            </div>
            <div class="row m-1">
                <div class="col-6">
                    <%--Acción realizada--%>
                    <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Ingrese la acción realizada"></asp:Label>
                    <asp:TextBox ID="TBAccionrealizada" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TBAccionrealizada" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>

                </div>
                <div class="col-6">
                    <%--Razón--%>
                    <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Ingrese la razón"></asp:Label>
                    <asp:TextBox ID="TBRazon" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TBRazon" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>

                </div>
            </div>
            <div class="row m-1">
                <div class="col-6">
                    <%--Relevancia--%>
                    <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Seleccione la relevancia"></asp:Label>
                    <asp:DropDownList ID="DDLRelevancia" CssClass="form-select" runat="server">
                        <asp:ListItem Value="0">Seleccione</asp:ListItem>
                        <asp:ListItem Text="Alta" Value="alta"></asp:ListItem>
                        <asp:ListItem Text="Media" Value="media"></asp:ListItem>
                        <asp:ListItem Text="Baja" Value="baja"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="DDLRelevancia" InitialValue="0" ForeColor="Red" ErrorMessage="Debes seleccionar la complejidad."></asp:RequiredFieldValidator>

                </div>
                <div class="col-6">
                    <%--Evidencia--%>
                    <asp:Label ID="Label7" CssClass="form-label" runat="server" Text="Ingrese la evidencia"></asp:Label>
                    <asp:TextBox ID="TBEvidencia" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TBEvidencia" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>

                </div>
            </div>
            <div class="row m-1">
                <div class="col-6">
                    <%--Comentario--%>
                    <asp:Label ID="Label8" CssClass="form-label" runat="server" Text="Ingrese un comentario"></asp:Label>
                    <asp:TextBox ID="TBComentario" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TBComentario" ForeColor="Red" Display="Dynamic" ErrorMessage="Este campo es obligatorio."></asp:RequiredFieldValidator>

                </div>
                <div class="col-6">
                    <%--Estado--%>
                    <asp:Label ID="Label9" CssClass="form-label" runat="server" Text="Seleccione el estado"></asp:Label>
                    <asp:DropDownList ID="DDLEstado" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Seleccione el estado" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Pendiente" Value="pendiente"></asp:ListItem>
                        <asp:ListItem Text="En progreso" Value="en progreso"></asp:ListItem>
                        <asp:ListItem Text="Finalizada" Value="finalizada"></asp:ListItem>
                        <asp:ListItem Text="Cancelada" Value="cancelada"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DDLEstado" InitialValue="0" ForeColor="Red" ErrorMessage="Debes seleccionar la complejidad."></asp:RequiredFieldValidator>

                </div>
            </div>
            <div class="row m-1">
                <div class="col">
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
            Lista de Expedientes
        </div>
        <div class="table-responsive">
            <table id="expedienteTable" class="table table-hover display" style="width: 100%">
                <thead>
                    <tr>
                        <th>casoID</th>
                        <th>FKCaso</th>
                        <th>Caso</th>
                        <th>Código</th>
                        <th>Fecha creación</th>
                        <th>Acción realizada</th>
                        <th>Razón</th>
                        <th>Relevancia</th>
                        <th>Evidencia</th>
                        <th>Comentario</th>
                        <th>Estado</th>
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
    document.getElementById('<%= DDCaso_idcaso.ClientID %>').setAttribute('readonly', true);
    document.getElementById('<%= DDCaso_idcaso.ClientID %>').style.backgroundColor = '#e9ecef';

    $(document).ready(function () {
        $('#expedienteTable').DataTable({
            "processing": true,
            "serverSide": false,
            "ajax": {
                "url": "WFAsignarExpediente.aspx/ListExpedientes",// Se invoca el WebMethod Listar Expediente
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
                { "data": "ExpedienteID" },
                { "data": "FKCaso", "visible": false },
                { "data": "Caso" },
                { "data": "Codigo" },
                { "data": "Fechacreacion" },
                { "data": "Accionrealizada" },
                { "data": "Razon" },
                { "data": "Relevancia" },
                { "data": "Evidencia" },
                { "data": "Comentario" },
                { "data": "Estado" },
                {
                    "data": null,
                    "render": function (data, type, row) {
                        return `<button class="btn btn-link btn-lg px-0 edit-btn" data-id="${row.ExpedienteID}" style="color:#fd7e14" title="Editar" ><i class="lni lni-pencil-1"></i></button>
                          <button class="btn btn-link btn-lg text-danger px-0 delete-btn" data-id="${row.ExpedienteID}" title="Eliminar"><i class="lni lni-trash-3"></i></button>`;
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

        // Editar un expediente
        $('#expedienteTable').on('click', '.edit-btn', function () {
            //const id = $(this).data('id');
            const rowData = $('#expedienteTable').DataTable().row($(this).parents('tr')).data();
            //alert(JSON.stringify(rowData, null, 2));
            loadExpedienteData(rowData);
        });

        // Eliminar un expediente
        $('#expedienteTable').on('click', '.delete-btn', function () {
            const id = $(this).data('id');// Obtener el ID del expediente
            if (confirm("¿Estás seguro de que deseas eliminar este expediente?")) {
                deleteExpediente(id);// Invoca a la función para eliminar el expediente
            }
        });
    });

    // Cargar los datos en los TextBox y DDL para actualizar
    function loadExpedienteData(rowData) {
        $('#<%= ExpedienteID.ClientID %>').val(rowData.ExpedienteID);
        $('#<%= DDCaso_idcaso.ClientID %>').val(rowData.FKCaso);
        $('#<%= TBCodigo.ClientID %>').val(rowData.Codigo);
        $('#<%= TBAccionrealizada.ClientID %>').val(rowData.Accionrealizada);
        $('#<%= TBRazon.ClientID %>').val(rowData.Razon);
        $('#<%= DDLRelevancia.ClientID %>').val(rowData.Relevancia);
        $('#<%= TBEvidencia.ClientID %>').val(rowData.Evidencia);
        $('#<%= TBComentario.ClientID %>').val(rowData.Comentario);
        $('#<%= DDLEstado.ClientID %>').val(rowData.Estado);
    }

    // Función para eliminar un Expediente
    function deleteExpediente(id) {
        $.ajax({
            type: "POST",
            url: "WFAsignarExpediente.aspx/DeleteExpediente",// Se invoca el WebMethod Eliminar un Expediente
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ id: id }),
            success: function (response) {
                $('#expedienteTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                alert("Expediente eliminado exitosamente.");
            },
            error: function () {
                alert("Error al eliminar el expediente.");
            }
        });
    }
</script>
</asp:Content>
