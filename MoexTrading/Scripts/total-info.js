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

function test(id) {
    var dataPoints = [];

    var chart = new CanvasJS.Chart("chart_div", {
        animationEnabled: true,
        theme: "light1", // "light1", "light2", "dark1", "dark2"
        exportEnabled: true,
        subtitles: [{
            text: "Weekly Averages"
        }],
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



            //for (var i = 0; i < csvLines.length; i++) {
            //    if (csvLines[i].length > 0) {
            //        points = csvLines[i].split(",");
            //        dataPoints.push({
            //            x: new Date(
            //                parseInt(points[0].split("-")[0]),
            //                parseInt(points[0].split("-")[1]),
            //                parseInt(points[0].split("-")[2])
            //            ),
            //            y: [
            //                parseFloat(points[1]),
            //                parseFloat(points[2]),
            //                parseFloat(points[3]),
            //                parseFloat(points[4])
            //            ]
            //        });
            //    }
            //}
            chart.render();
        }
    });

    


}

function myFunction() {
    var id_tool = $('#test > option:selected').attr("value");
    loadGlass(id_tool);
    test(id_tool);
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



//window.onload = function () {
//    google.charts.load('current', { 'packages': ['corechart'] });
//    google.charts.setOnLoadCallback(drawChart);

//    function drawChart() {
//        //var data = google.visualization.arrayToDataTable([
//        //    ['', 20, 28, 38, 45],
//        //    ['', 31, 38, 55, 66],
//        //    ['', 50, 55, 77, 80],
//        //    ['', 77, 77, 66, 50],
//        //    ['', 68, 66, 22, 15],
//        //    ['', 20, 28, 38, 45],
//        //    ['', 31, 38, 55, 66],
//        //    ['', 50, 55, 77, 80],
//        //    ['', 77, 77, 66, 50],
//        //    ['', 68, 66, 22, 15],
//        //    ['', 20, 28, 38, 45],
//        //    ['', 31, 38, 55, 66],
//        //    ['', 50, 55, 77, 80],
//        //    ['', 77, 77, 66, 50],
//        //    ['', 68, 66, 22, 15]
//        //    // Treat first row as data as well.
//        //], true);
//        var data;// = google.visualization.arrayToDataTable($.get("/api/values/getdatacommon"), true);

//        $.ajax({
//            url: "/api/values/getdatacommon",
//            type: "get",
//            async: false,
//            success: function (userStatus) {
//                data = google.visualization.arrayToDataTable(userStatus, true);
//            },
//            error: function () {
//                connectionError();
//            }
//        });



//        //$.get("/api/values/getdatacommon", function (d) {
//        //    data = d;
//        //});

//        var options = {
//            legend: 'none',
//            candlestick: {
//                fallingColor: { strokeWidth: 0, fill: '#a52714' }, // red
//                risingColor: { strokeWidth: 0, fill: '#0f9d58' }   // green
//            }
//        };

//        var chart = new google.visualization.CandlestickChart(document.getElementById('chart_div'));

//        chart.draw(data, options);
//    }
//}



////window.onload = function () {

////    var dataPoints = [];

////    var chart = new CanvasJS.Chart("chart_div", {
////        animationEnabled: true,
////        theme: "light2", // "light1", "light2", "dark1", "dark2"
////        exportEnabled: true,
////        title: {
////            text: "Netflix Stock Price in 2016"
////        },
////        axisX: {
////            interval: 1,
////            valueFormatString: "MMM"
////        },
////        toolTip: {
////            content: "Date: {x}<br /><strong>Price:</strong><br />Open: {y[0]}, Close: {y[3]}<br />High: {y[1]}, Low: {y[2]}"
////        },
////        data: [{
////            type: "candlestick",
////            yValueFormatString: "$##0.00",
////            dataPoints: dataPoints
////        }]
////    });

////    $.get("/api/values/getdatacommon", function (csvLines) {
////        for (var i = 0; i < csvLines.length; i++) {
////            if (csvLines[i].length > 0) {
////                var points = csvLines[i].split(",");
////                dataPoints.push({
////                    x: new Date(
////                        parseInt(points[0].split("-")[0]),
////                        parseInt(points[0].split("-")[1]),
////                        parseInt(points[0].split("-")[2])
////                    ),
////                    y: [
////                        parseFloat(points[1]),
////                        parseFloat(points[2]),
////                        parseFloat(points[3]),
////                        parseFloat(points[4])
////                    ]
////                });
////            }
////        }
////        chart.render();
////    });

////    //function getDataPointsFromCSV(csvLines) {
////    //  //  var csvLines = points = [];
////    //  //  csvLines = csv.split(/[\r?\n|\r|\n]+/);
////    //    for (var i = 0; i < csvLines.length; i++) {
////    //        if (csvLines[i].length > 0) {
////    //            points = csvLines[i].split(",");
////    //            dataPoints.push({
////    //                x: new Date(
////    //                    parseInt(points[0].split("-")[0]),
////    //                    parseInt(points[0].split("-")[1]),
////    //                    parseInt(points[0].split("-")[2])
////    //                ),
////    //                y: [
////    //                    parseFloat(points[1]),
////    //                    parseFloat(points[2]),
////    //                    parseFloat(points[3]),
////    //                    parseFloat(points[4])
////    //                ]
////    //            });
////    //        }
////    //    }
////    //    chart.render();
////    //}

////}