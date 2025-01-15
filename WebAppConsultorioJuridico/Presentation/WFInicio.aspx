<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFInicio.aspx.cs" Inherits="Presentation.WFInicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/StyleDashboard.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="FrmInicio" runat="server">
        <asp:Label ID="LblMsg" runat="server" Text="" CssClass="welcome-message d-block"></asp:Label>
        <div class="container-fluid py-4">
            <!-- Tarjetas de información -->
            <div class="row g-4 mb-4">
                <!-- Usuarios -->
                <div class="col-12 col-sm-6 col-xl-3">
                    <div class="dashboard-card">
                        <div class="card-body">
                            <div class="card-title-container">
                                <i class="fas fa-users card-icon"></i>
                                <h5 class="card-title">Total Usuarios</h5>
                            </div>
                            <asp:Label ID="LblCantUsu" runat="server" Text="" CssClass="stats-number"></asp:Label>
                        </div>
                        <div class="card-footer text-center">
                            <a class="small-box-footer" href="WFUsuario.aspx">Más información <i class="lni lni-chevron-right-circle ms-1"></i>
                            </a>
                        </div>
                    </div>
                </div>

                <!-- Abogados -->
                <div class="col-12 col-sm-6 col-xl-3">
                    <div class="dashboard-card">
                        <div class="card-body">
                            <div class="card-title-container">
                                <i class="fas fa-user-tie card-icon"></i>
                                <h5 class="card-title">Total Abogados</h5>
                            </div>
                            <asp:Label ID="LblCantEmp" runat="server" Text="" CssClass="stats-number"></asp:Label>
                        </div>
                        <div class="card-footer text-center">
                            <a class="small-box-footer" href="WFEmpleado.aspx">Más información <i class="lni lni-chevron-right-circle ms-1"></i>
                            </a>
                        </div>
                    </div>
                </div>

                <!-- Clientes -->
                <div class="col-12 col-sm-6 col-xl-3">
                    <div class="dashboard-card">
                        <div class="card-body">
                            <div class="card-title-container">
                                <i class="fas fa-user-friends card-icon"></i>
                                <h5 class="card-title">Total Clientes</h5>
                            </div>
                            <asp:Label ID="LblCantClient" runat="server" Text="" CssClass="stats-number"></asp:Label>
                        </div>
                        <div class="card-footer text-center">
                            <a class="small-box-footer" href="WFCasoHasPersona.aspx">Más información <i class="lni lni-chevron-right-circle ms-1"></i>
                            </a>
                        </div>
                    </div>
                </div>

                <!-- Casos -->
                <div class="col-12 col-sm-6 col-xl-3">
                    <div class="dashboard-card">
                        <div class="card-body">
                            <div class="card-title-container">
                                <i class="fas fa-briefcase card-icon"></i>
                                <h5 class="card-title">Total Casos</h5>
                            </div>
                            <asp:Label ID="LblCantCasos" runat="server" Text="" CssClass="stats-number"></asp:Label>
                        </div>
                        <div class="card-footer text-center">
                            <a class="small-box-footer" href="WFCaso.aspx">Más información <i class="lni lni-chevron-right-circle ms-1"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Gráficas -->
        <div class="row">
            <div class="col-12 col-xl-6">
                <div class="chart-card">
                    <div class="card-header">
                        <i class="lni lni-bar-chart-4 me-2"></i>
                        Cantidad de casos por estado
                       
                    </div>
                    <div class="card-body">
                        <div id="piechart" style="width: 100%; height: 400px;"></div>
                    </div>
                </div>
            </div>
            <div class="col-12 col-xl-6">
                <div class="chart-card">
                    <div class="card-header">
                        <i class="lni lni-bar-chart-4 me-2"></i>
                        Asignacion de Citas por mes
                    </div>
                    <div class="card-body">
                        <div id="columnchart" style="width: 100%; height: 400px;"></div>
                    </div>
                </div>
            </div>
            <div class="col-12">
                <div class="chart-card">
                    <div class="card-header">
                        <i class="fas fa-chart-line me-2"></i>
                        Tendencia de Casos Cerrados
                       
                    </div>
                    <div class="card-body">
                        <div id="tendenciaChart" style="width: 100%; height: 400px;"></div>
                    </div>
                </div>
            </div>
        </div>   
    </form>

    <!-- Scripts -->
    <script src="https://code.jquery.com/jquery-3.7.1.js"></script>
    <script src="https://www.gstatic.com/charts/loader.js"></script>

    <script type="text/javascript">
        // Solo una carga inicial
        google.charts.load('current', { 'packages': ['corechart'] });

        // Callback principal después de la carga
        google.charts.setOnLoadCallback(function () {
            // Llamar a las funciones de dibujo aquí, sin setOnLoadCallback individuales
            fetchDataAndDrawPieChart();
            fetchDataAndDrawAsignacionCitasChart();
            drawTendenciaChart();
        });
    </script>

    <!-- Script para la gráfica circular -->
    <script type="text/javascript">
        function fetchDataAndDrawPieChart() {
            $.ajax({
                url: 'WFInicio.aspx/ListCountCasosEstados',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var rawData = response.d.data;
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Nombre');
                    data.addColumn('number', 'TotalCasos');

                    rawData.forEach(function (item) {
                        data.addRow([item.EstadoName, parseInt(item.TotalCasos)]);
                    });

                    var options = {
                        pieHole: 0.4,
                        colors: ['#2C3E50', '#34495E', '#C0392B', '#E74C3C'],
                        chartArea: { width: '90%', height: '80%' },
                        legend: { position: 'bottom' }
                    };

                    var chart = new google.visualization.PieChart(document.getElementById('piechart'));
                    chart.draw(data, options);
                },
                error: function (error) {
                    console.error('Error al obtener los datos: ', error);
                }
            });
        }
    </script>
    <!-- Script para la gráfica de columnas -->
    <script type="text/javascript">
        function fetchDataAndDrawAsignacionCitasChart() {
            $.ajax({
                url: 'WFInicio.aspx/GetAsignacionCitasPorMes',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Mes');
                    data.addColumn('number', 'Total Citas');

                    response.d.data.forEach(function (item) {
                        var fecha = new Date(item.MesActual + "-01");
                        var nombreMes = fecha.toLocaleDateString('es-ES', { month: 'long' });
                        var anio = fecha.getFullYear();
                        data.addRow([nombreMes + ' ' + anio, item.TotalCitasAsignadas]);
                    });

                    var options = {
                        title: 'Asignación de Citas por Mes',
                        colors: ['#3498DB'],
                        animation: { duration: 1000, easing: 'out', startup: true },
                        hAxis: {
                            title: 'Mes',
                            slantedText: true,
                            slantedTextAngle: 45
                        },
                        vAxis: {
                            title: 'Total Citas',
                            minValue: 0
                        },
                        bar: { groupWidth: '75%' }
                    };

                    var chart = new google.visualization.ColumnChart(document.getElementById('columnchart'));
                    chart.draw(data, options);
                },
                error: function (error) {
                    console.error('Error:', error);
                }
            });
        }
    </script>
    <!-- Script para la gráfica de tendencia -->
    <script type="text/javascript">
        function drawTendenciaChart() {
            $.ajax({
                url: 'WFInicio.aspx/GetTendenciaCasosCerrados',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var rawData = response.d.data;
                    if (!rawData || rawData.length === 0) {
                        console.error("No hay datos disponibles para la tendencia.");
                        return;
                    }

                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Período');
                    data.addColumn('number', 'Total Casos');

                    rawData.forEach(function (item) {
                        data.addRow([item.Mes + '/' + item.Anio, parseInt(item.TotalCasos)]);
                    });

                    var options = {
                        curveType: 'function',
                        colors: ['#2C3E50'],
                        lineWidth: 3,
                        pointSize: 6,
                        chartArea: { width: '80%', height: '70%' },
                        hAxis: {
                            title: 'Período',
                            slantedText: true,
                            slantedTextAngle: 45
                        },
                        vAxis: {
                            title: 'Total Casos',
                            minValue: 0
                        },
                        animation: {
                            duration: 1000,
                            easing: 'out',
                            startup: true
                        }
                    };

                    var chart = new google.visualization.LineChart(document.getElementById('tendenciaChart'));
                    chart.draw(data, options);
                },
                error: function (error) {
                    console.error('Error al obtener los datos de tendencia: ', error);
                }
            });
        }
    </script>

    <script type="text/javascript">
        window.addEventListener('resize', function () {
            fetchDataAndDrawPieChart();
            fetchDataAndDrawAsignacionCitasChart();
            drawTendenciaChart();
        });
    </script>

</asp:Content>
