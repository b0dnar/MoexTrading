$(document).ready(function () {
});


function myFunction() {
    var comboboxValue = $('#test > option:selected').attr("value");
    $.ajax({
        contentType: 'application/json',
        type: "POST",
        url: "/api/values/postsetstakan",
        data: JSON.stringify({
            Id: $('#test > option:selected').attr("value")
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
    //console.log(comboboxValue);
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

//var trace1;
//$.ajax({
//    url: "/api/values/getdatacommon",
//    type: "get",
//    async: false,
//    success: function (datas) {
//        var a1 = [], a2 = [], a3 = [], a4 = [], a5 = [];
//        for (var i = 0; i < datas.length; i++) {
//            a1.push(datas[i].Time);
//            a2.push(datas[i].ClosePrice);
//            a3.push(datas[i].MaxPrice);
//            a4.push(datas[i].MinPrice);
//            a5.push(datas[i].OpenPrice);
//        }

//        trace1 = {


//            x: datas. a1,//['2017-01-04', '2017-01-05', '2017-01-06', '2017-01-09', '2017-01-10', '2017-01-11', '2017-01-12', '2017-01-13', '2017-01-17', '2017-01-18', '2017-01-19', '2017-01-20', '2017-01-23', '2017-01-24', '2017-01-25', '2017-01-26', '2017-01-27', '2017-01-30', '2017-01-31', '2017-02-01', '2017-02-02', '2017-02-03', '2017-02-06', '2017-02-07', '2017-02-08', '2017-02-09', '2017-02-10', '2017-02-13', '2017-02-14', '2017-02-15'],
//            close: a2,//[116.019997, 116.610001, 117.910004, 118.989998, 119.110001, 119.75, 119.25, 119.040001, 120, 119.989998, 119.779999, 120, 120.080002, 119.970001, 121.879997, 121.940002, 121.949997, 121.629997, 121.349998, 128.75, 128.529999, 129.080002, 130.289993, 131.529999, 132.039993, 132.419998, 132.119995, 133.289993, 135.020004, 135.509995],
//            decreasing: { line: { color: '#7F7F7F' } },
//            high: a3,//[116.510002, 116.860001, 118.160004, 119.43, 119.379997, 119.93, 119.300003, 119.620003, 120.239998, 120.5, 120.089996, 120.449997, 120.809998, 120.099998, 122.099998, 122.440002, 122.349998, 121.629997, 121.389999, 130.490005, 129.389999, 129.190002, 130.5, 132.089996, 132.220001, 132.449997, 132.940002, 133.820007, 135.089996, 136.270004],
//            increasing: { line: { color: '#17BECF' } },
//            line: { color: 'rgba(31,119,180,1)' },
//            low: a4,//[115.75, 115.809998, 116.470001, 117.940002, 118.300003, 118.599998, 118.209999, 118.809998, 118.220001, 119.709999, 119.370003, 119.730003, 119.769997, 119.5, 120.279999, 121.599998, 121.599998, 120.660004, 120.620003, 127.010002, 127.779999, 128.160004, 128.899994, 130.449997, 131.220001, 131.119995, 132.050003, 132.75, 133.25, 134.619995],
//            open: a5,//[115.849998, 115.919998, 116.779999, 117.949997, 118.769997, 118.739998, 118.900002, 119.110001, 118.339996, 120, 119.400002, 120.449997, 120, 119.550003, 120.419998, 121.669998, 122.139999, 120.93, 121.150002, 127.029999, 127.980003, 128.309998, 129.130005, 130.539993, 131.350006, 131.649994, 132.460007, 133.080002, 133.470001, 135.520004],
//            type: 'candlestick',
//            xaxis: 'x',
//            yaxis: 'y'
//        };
//    },
//    error: function () {
//        connectionError();
//    }
//});


//var data = [trace1];

//var layout = {
//    dragmode: 'zoom',
//    margin: {
//        r: 10,
//        t: 25,
//        b: 40,
//        l: 60
//    },
//    showlegend: false,
//    xaxis: {
//        autorange: true,
//        domain: [0, 1],
//        range: ['2017-01-03 12:00', '2017-02-15 12:00'],
//        rangeslider: { range: ['2017-01-03 12:00', '2017-02-15 12:00'] },
//        title: 'Date',
//        type: 'date'
//    },
//    yaxis: {
//        autorange: true,
//        domain: [0, 1],
//        range: [114.609999778, 137.410004222],
//        type: 'linear'
//    }
//};

//Plotly.plot('chart_div', data, layout);




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