$(document).ready(function () {
});

function loadGlass(id) {
    $.ajax({
        contentType: 'application/json',
        type: "POST",
        url: "/api/values/postsetstakan",
        data: JSON.stringify({
            Id: id
        }),
        success: function (stakan) {
            if (stakan === null) {
                $('#stakan-info').html('');
                return;
            }

            var table_body = '<table border="1"><thead><tr align="center"><th width="80">Покупка</th><th width="100">Цена</th><th width="80">Продажа</th></tr></thead><tbody>';

            for (i = 0; i < stakan.length; i++) {
                table_body += '<tr align="center">';

                if (stakan[i].Dir == 1)
                    table_body += '<td><p>' + stakan[i].Volum + '</p></td>';
                else
                    table_body += '<td><p></p></td>';

                table_body += '<td><p>' + stakan[i].Price + '</p></td>';

                if (stakan[i].Dir == 2)
                    table_body += '<td><p>' + stakan[i].Volum + '</p></td>';
                else
                    table_body += '<td><p></p></td>';

                table_body += '</tr>';
            }

            table_body += '</tbody></table>';
            $('#stakan-info').html(table_body);
        }
    });
}

function loadCandles(id) {
    $("#chart_div").html('');
    var trace1;
    $.ajax({
        url: "/api/values/postdatacandles",
        type: "POST",
        async: false,
        contentType: 'application/json',
        data: JSON.stringify({
            Id: id
        }),
        success: function (datas) {
            var a1 = [], a2 = [], a3 = [], a4 = [], a5 = [];

            a1 = datas.ArrayTime;
            for (var i = 0; i < datas.ArrayCandles.length - 1; i++) {
                a5.push(datas.ArrayCandles[i]);
            }
            for (var i = 1; i < datas.ArrayCandles.length; i++) {
                a2.push(datas.ArrayCandles[i]);
            }
            for (var i = 0; i < datas.ArrayCandles.length - 1; i++) {

                if (datas.ArrayCandles[i] > datas.ArrayCandles[i + 1]) {
                    a3.push(datas.ArrayCandles[i]);
                    a4.push(datas.ArrayCandles[i + 1]);
                }
                else {
                    a3.push(datas.ArrayCandles[i + 1]);
                    a4.push(datas.ArrayCandles[i]);
                }
            }

            trace1 = {
                x: datas.a1,
                close: a2,
                decreasing: { line: { color: 'red' } },
                high: a3,
                increasing: { line: { color: 'green' } },
                line: { color: 'rgba(31,119,180,1)' },
                low: a4,
                open: a5,
                type: 'candlestick',
                xaxis: 'x',
                yaxis: 'y'
            };
        },
        error: function () {
            connectionError();
        }
    });


    var data = [trace1];


    var layout = {
        dragmode: 'zoom',
        //margin: {
        //    r: 10,
        //    t: 25,
        //    b: 40,
        //    l: 60
        //},
        showlegend: false
        //xaxis: {
        //    autorange: true,
        //    domain: [0, 1],
        //    range: ['2017-01-03 12:00', '2017-02-15 12:00'],
        //    rangeslider: { range: ['2017-01-03 12:00', '2017-02-15 12:00'] },
        //    title: 'Date',
        //    type: 'date'
        //},
        //yaxis: {
        //    autorange: true,
        //    domain: [0, 1],
        //    range: [114.609999778, 137.410004222],
        //    type: 'linear'
        //}
    };

    Plotly.plot("chart_div", data, layout);
}

function t2(id) {
    var rez;
    $.ajax({
        url: "/api/values/postdatacandles",
        type: "POST",
        async: false,
        contentType: 'application/json',
        data: JSON.stringify({
            Id: id
        }),
        success: function (datas) {
            rez = datas;
        }
    });


    var options = {
        chart: {
            // height: 350,
            type: 'candlestick',
        },
        series: [{

            data: [{
                x: new Date(rez[0].Time),
                y: [rez[0].Arr[0], rez[0].Arr[1], rez[0].Arr[2], rez[0].Arr[3]]
            },
                {
                    x: new Date(rez[1].Time),
                    y: [rez[1].Arr[0], rez[1].Arr[1], rez[1].Arr[2], rez[1].Arr[3]]
                }, {
                    x: new Date(rez[2].Time),
                    y: [rez[2].Arr[0], rez[2].Arr[1], rez[2].Arr[2], rez[2].Arr[3]]
                }, {
                    x: new Date(rez[3].Time),
                    y: [rez[3].Arr[0], rez[3].Arr[1], rez[3].Arr[2], rez[3].Arr[3]]
                }]

            //data: [{
            //    x: new Date(1538778600000),
            //    y: [6629.81, 6650.5, 6623.04, 6633.33]
            //},
            //{
            //    x: new Date(1538780400000),
            //    y: [6632.01, 6643.59, 6620, 6630.11]
            //},
            //{
            //    x: new Date(1538782200000),
            //    y: [6630.71, 6648.95, 6623.34, 6635.65]
            //},
            //{
            //    x: new Date(1538784000000),
            //    y: [6635.65, 6651, 6629.67, 6638.24]
            //},
            //{
            //    x: new Date(1538785800000),
            //    y: [6638.24, 6640, 6620, 6624.47]
            //},
            //{
            //    x: new Date(1538787600000),
            //    y: [6624.53, 6636.03, 6621.68, 6624.31]
            //},
            //{
            //    x: new Date(1538789400000),
            //    y: [6624.61, 6632.2, 6617, 6626.02]
            //},
            //{
            //    x: new Date(1538791200000),
            //    y: [6627, 6627.62, 6584.22, 6603.02]
            //},
            //{
            //    x: new Date(1538793000000),
            //    y: [6605, 6608.03, 6598.95, 6604.01]
            //},
            //{
            //    x: new Date(1538794800000),
            //    y: [6604.5, 6614.4, 6602.26, 6608.02]
            //},
            //{
            //    x: new Date(1538796600000),
            //    y: [6608.02, 6610.68, 6601.99, 6608.91]
            //},
            //{
            //    x: new Date(1538798400000),
            //    y: [6608.91, 6618.99, 6608.01, 6612]
            //},
            //{
            //    x: new Date(1538800200000),
            //    y: [6612, 6615.13, 6605.09, 6612]
            //},
            //{
            //    x: new Date(1538802000000),
            //    y: [6612, 6624.12, 6608.43, 6622.95]
            //},
            //{
            //    x: new Date(1538803800000),
            //    y: [6623.91, 6623.91, 6615, 6615.67]
            //},
            //{
            //    x: new Date(1538805600000),
            //    y: [6618.69, 6618.74, 6610, 6610.4]
            //},
            //{
            //    x: new Date(1538807400000),
            //    y: [6611, 6622.78, 6610.4, 6614.9]
            //},
            //{
            //    x: new Date(1538809200000),
            //    y: [6614.9, 6626.2, 6613.33, 6623.45]
            //},
            //{
            //    x: new Date(1538811000000),
            //    y: [6623.48, 6627, 6618.38, 6620.35]
            //},
            //{
            //    x: new Date(1538812800000),
            //    y: [6619.43, 6620.35, 6610.05, 6615.53]
            //},
            //{
            //    x: new Date(1538814600000),
            //    y: [6615.53, 6617.93, 6610, 6615.19]
            //},
            //{
            //    x: new Date(1538816400000),
            //    y: [6615.19, 6621.6, 6608.2, 6620]
            //},
            //{
            //    x: new Date(1538818200000),
            //    y: [6619.54, 6625.17, 6614.15, 6620]
            //},
            //{
            //    x: new Date(1538820000000),
            //    y: [6620.33, 6634.15, 6617.24, 6624.61]
            //},
            //{
            //    x: new Date(1538821800000),
            //    y: [6625.95, 6626, 6611.66, 6617.58]
            //},
            //{
            //    x: new Date(1538823600000),
            //    y: [6619, 6625.97, 6595.27, 6598.86]
            //},
            //{
            //    x: new Date(1538825400000),
            //    y: [6598.86, 6598.88, 6570, 6587.16]
            //},
            //{
            //    x: new Date(1538827200000),
            //    y: [6588.86, 6600, 6580, 6593.4]
            //},
            //{
            //    x: new Date(1538829000000),
            //    y: [6593.99, 6598.89, 6585, 6587.81]
            //},
            //{
            //    x: new Date(1538830800000),
            //    y: [6587.81, 6592.73, 6567.14, 6578]
            //},
            //{
            //    x: new Date(1538832600000),
            //    y: [6578.35, 6581.72, 6567.39, 6579]
            //},
            //{
            //    x: new Date(1538834400000),
            //    y: [6579.38, 6580.92, 6566.77, 6575.96]
            //},
            //{
            //    x: new Date(1538836200000),
            //    y: [6575.96, 6589, 6571.77, 6588.92]
            //},
            //{
            //    x: new Date(1538838000000),
            //    y: [6588.92, 6594, 6577.55, 6589.22]
            //},
            //{
            //    x: new Date(1538839800000),
            //    y: [6589.3, 6598.89, 6589.1, 6596.08]
            //},
            //{
            //    x: new Date(1538841600000),
            //    y: [6597.5, 6600, 6588.39, 6596.25]
            //},
            //{
            //    x: new Date(1538843400000),
            //    y: [6598.03, 6600, 6588.73, 6595.97]
            //},
            //{
            //    x: new Date(1538845200000),
            //    y: [6595.97, 6602.01, 6588.17, 6602]
            //},
            //{
            //    x: new Date(1538847000000),
            //    y: [6602, 6607, 6596.51, 6599.95]
            //},
            //{
            //    x: new Date(1538848800000),
            //    y: [6600.63, 6601.21, 6590.39, 6591.02]
            //},
            //{
            //    x: new Date(1538850600000),
            //    y: [6591.02, 6603.08, 6591, 6591]
            //},
            //{
            //    x: new Date(1538852400000),
            //    y: [6591, 6601.32, 6585, 6592]
            //},
            //{
            //    x: new Date(1538854200000),
            //    y: [6593.13, 6596.01, 6590, 6593.34]
            //},
            //{
            //    x: new Date(1538856000000),
            //    y: [6593.34, 6604.76, 6582.63, 6593.86]
            //},
            //{
            //    x: new Date(1538857800000),
            //    y: [6593.86, 6604.28, 6586.57, 6600.01]
            //},
            //{
            //    x: new Date(1538859600000),
            //    y: [6601.81, 6603.21, 6592.78, 6596.25]
            //},
            //{
            //    x: new Date(1538861400000),
            //    y: [6596.25, 6604.2, 6590, 6602.99]
            //},
            //{
            //    x: new Date(1538863200000),
            //    y: [6602.99, 6606, 6584.99, 6587.81]
            //},
            //{
            //    x: new Date(1538865000000),
            //    y: [6587.81, 6595, 6583.27, 6591.96]
            //},
            //{
            //    x: new Date(1538866800000),
            //    y: [6591.97, 6596.07, 6585, 6588.39]
            //},
            //{
            //    x: new Date(1538868600000),
            //    y: [6587.6, 6598.21, 6587.6, 6594.27]
            //},
            //{
            //    x: new Date(1538870400000),
            //    y: [6596.44, 6601, 6590, 6596.55]
            //},
            //{
            //    x: new Date(1538872200000),
            //    y: [6598.91, 6605, 6596.61, 6600.02]
            //},
            //{
            //    x: new Date(1538874000000),
            //    y: [6600.55, 6605, 6589.14, 6593.01]
            //},
            //{
            //    x: new Date(1538875800000),
            //    y: [6593.15, 6605, 6592, 6603.06]
            //},
            //{
            //    x: new Date(1538877600000),
            //    y: [6603.07, 6604.5, 6599.09, 6603.89]
            //},
            //{
            //    x: new Date(1538879400000),
            //    y: [6604.44, 6604.44, 6600, 6603.5]
            //},
            //{
            //    x: new Date(1538881200000),
            //    y: [6603.5, 6603.99, 6597.5, 6603.86]
            //},
            //{
            //    x: new Date(1538883000000),
            //    y: [6603.85, 6605, 6600, 6604.07]
            //},
            //{
            //    x: new Date(1538884800000),
            //    y: [6604.98, 6606, 6604.07, 6606]
            //},
            //]
        }],
        title: {
            text: 'CandleStick Chart',
            align: 'left'
        },
        xaxis: {
            type: 'datetime'
        },
        yaxis: {
            tooltip: {
                enabled: true
            }
        }
    }

    var chart = new ApexCharts(
        document.querySelector("#chart_div"),
        options
    );

    chart.render();
}

function test(id) {
    var dataPoints = [];

    var chart = new CanvasJS.Chart("chart_div", {
        animationEnabled: true,
        theme: "light1", // "light1", "light2", "dark1", "dark2"
        exportEnabled: true,
        axisX: {
            interval: 1,
            valueFormatString: "MMM"
        },
        axisY: {
            includeZero: false,
            prefix: "$",
            title: "Price"
        },
        toolTip: {
            content: "Date: {x}<br /><strong>Price:</strong><br />Open: {y[0]}, Close: {y[3]}<br />High: {y[1]}, Low: {y[2]}"
        },
        data: [{
            type: "candlestick",
            yValueFormatString: "$##0.00",
            dataPoints: dataPoints
        }]
    });

    $.ajax({
        url: "/api/values/postdatacandles",
        type: "POST",
        async: false,
        contentType: 'application/json',
        data: JSON.stringify({
            Id: id
        }),
        success: function (datas) {

            for (var i = 0; i < datas.ArrayCandles.length; i++) {

                var min, max;
                if (datas.ArrayCandles[i] > datas.ArrayCandles[i + 1]) {
                    max = datas.ArrayCandles[i];
                    min = datas.ArrayCandles[i + 1];
                }
                else {
                    max = datas.ArrayCandles[i + 1];
                    min = datas.ArrayCandles[i];
                }

                dataPoints.push({
                    x: new Date(
                        2018,
                        11,
                        i
                    ),
                    y: [
                        datas.ArrayCandles[i],
                        max,
                        min,
                        datas.ArrayCandles[i + 1]
                    ]
                });
            }




            chart.render();
        }
    });




}

function myFunction() {
    var id_tool = $('#test > option:selected').attr("value");
    loadGlass(id_tool);
    //  test(id_tool);
    t2(id_tool);
}

window.onload = function () {
    $.get("/api/values/getdatakotirovka", function (cotir) {
        var table_body = '<table border="1"><thead><tr align="center"><th width="90">Инструменты</th><th width="70">Цена</th><th width="150">Изминение</th></tr></thead><tbody>';

        for (i = 0; i < cotir.length; i++) {
            var color = 'black';
            if (cotir[i].Diference < 0)
                color = 'red';
            else if (cotir[i].Diference > 0)
                color = 'green';

            table_body += '<tr align="center">';
            table_body += '<td><p>' + cotir[i].Name + '</p></td>';
            table_body += '<td><p>' + cotir[i].Kotir + '</p></td>';
            table_body += '<td><p style="color:' + color + ';">' + cotir[i].Diference.toFixed(2) + ' (' + cotir[i].Percent.toFixed(2) + '%)</p></td>';
            table_body += '</tr>';
        }

        table_body += '</tbody></table>';
        $('#cotirovka-info').html(table_body);
    });

    //$.get("/api/values/getdatastakan", function (stakan) {
    //    var table_body = '<table border="1" class="container"><thead><tr align="center"><th width="80">Покупка</th><th width="100">Цена</th><th width="80">Продажа</th></tr></thead><tbody>';

    //    for (i = 0; i < stakan.length; i++) {
    //        table_body += '<tr align="center">';

    //        if (stakan[i].Dir == 1)
    //            table_body += '<td><p>' + stakan[i].Volum + '</p></td>';
    //        else
    //            table_body += '<td><p></p></td>';

    //        table_body += '<td><p>' + stakan[i].Price + '</p></td>';

    //        if (stakan[i].Dir == 2)
    //            table_body += '<td><p>' + stakan[i].Volum + '</p></td>';
    //        else
    //            table_body += '<td><p></p></td>';

    //        table_body += '</tr>';
    //    }

    //    table_body += '</tbody></table>';
    //    $('#stakan-info').html(table_body);
    //});
}
