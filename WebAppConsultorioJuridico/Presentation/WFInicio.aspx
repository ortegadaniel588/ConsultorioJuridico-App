﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFInicio.aspx.cs" Inherits="Presentation.WFInicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    <div class="container-fluid">
        <div class="row">
            <div class="col">
                <div class="card border-success mb-3" style="max-width: 18rem;">
                    <div class="card-body text-success">
                        <asp:Label ID="LblCantUsu" runat="server" Text="" CssClass="fs-4 fw-bold"></asp:Label>
                        <h5 class="card-title">Total Usuarios</h5>
                    </div>
                    <div class="card-footer bg-transparent border-success text-center">
                        <a class="small-box-footer" href="WFUsers.aspx">Mas info
                            <i class="lni lni-chevron-right-circle"></i>
                        </a>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card border-success mb-3" style="max-width: 18rem;">
                    <div class="card-body text-success">
                        <asp:Label ID="LblCantEmp" runat="server" Text="" CssClass="fs-4 fw-bold"></asp:Label>
                        <h5 class="card-title">Total Empleados</h5>
                    </div>
                    <div class="card-footer bg-transparent border-success text-center">
                        <a class="small-box-footer" href="#">Mas info
                             <i class="lni lni-chevron-right-circle"></i>
                        </a>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card border-success mb-3" style="max-width: 18rem;">
                    <div class="card-body text-success">
                        <asp:Label ID="LblCantProd" runat="server" Text="" CssClass="fs-4 fw-bold"></asp:Label>
                        <h5 class="card-title">Total Productos</h5>
                    </div>
                    <div class="card-footer bg-transparent border-success text-center">
                        <a class="small-box-footer" href="WFProducts.aspx">Mas info
                            <i class="lni lni-chevron-right-circle"></i>
                        </a>
                    </div>
                </div>
            </div>
            <div class="col">
                <div class="card border-success mb-3" style="max-width: 18rem;">
                    <div class="card-body text-success">
                        <asp:Label ID="LblCantProv" runat="server" Text="" CssClass="fs-4 fw-bold"></asp:Label>
                        <h5 class="card-title">Total Proveedores</h5>
                    </div>
                    <div class="card-footer bg-transparent border-success text-center">
                        <a class="small-box-footer" href="WFProviders.aspx">Mas info
                            <i class="lni lni-chevron-right-circle"></i>
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-6">
                <div class="card border-info mb-3">
                    <div class="card-header">
                        <i class="lni lni-bar-chart-4"></i>
                        Cantidad de productos por categoria
                    </div>
                    <div class="card-body">
                        <div id="piechart" style="width: 100%; height: 100%; min-height: 400px;"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--JQuery--%>
    <script src="https://code.jquery.com/jquery-3.7.1.js" integrity="sha256-eKhayi8LEQwp4NKxN+CfCh+3qOVUtJn3QNZ0TciWLP4=" crossorigin="anonymous"></script>
    <!--Load the AJAX API-->
    <script src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        // Carga la API de Google Charts
        google.charts.load('current', { 'packages': ['corechart'] });

        // Llama al WebMethod y dibuja el gráfico al cargar la API
        google.charts.setOnLoadCallback(fetchDataAndDrawChart);

        // Función para obtener datos desde el WebMethod
        function fetchDataAndDrawChart() {
            $.ajax({
                url: 'WFInicio.aspx/ListCountProductsCategories', // Ajustar con el nombre de tu archivo ASPX
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    // Procesar los datos devueltos por el WebMethod
                    var rawData = response.d.data;

                    // Crear la tabla de datos para Google Charts
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Categoría');
                    data.addColumn('number', 'TotalProductos');

                    // Llenar la tabla con los datos del WebMethod
                    rawData.forEach(function (item) {
                        data.addRow([item.CategoryName, parseInt(item.TotalProducts)]);
                    });

                    // Configuración del gráfico
                    var options = {
                        title: '',
                        width: '100%',
                        height: '100%',
                        chartArea: { width: '90%', height: '80%' }
                    };

                    // Dibuja la gráfica
                    var chart = new google.visualization.PieChart(document.getElementById('piechart'));
                    chart.draw(data, options);
                },
                error: function (error) {
                    console.error('Error al obtener los datos: ', error);
                }
            });
        }
        // Redibuja la gráfica al redimensionar la ventana
        window.addEventListener('resize', fetchDataAndDrawChart);
    </script>
</asp:Content>
