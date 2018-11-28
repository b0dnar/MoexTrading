//import { loadKotirovka } from './kotirovka-run.js';

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
    var dataPoints = [];
    $.ajax({
        url: "/api/values/postdatacandles",
        type: "POST",
        async: false,
        contentType: 'application/json',
        data: JSON.stringify({
            Id: id
        }),
        success: function (datas) {
            if (datas == null)
                return;

            for (var i = 0; i < datas.ArrayCandles.length; i++) {
                dataPoints.push({
                    x: i + 1,//new Date(datas.ArrayTime[i]),
                    y: [
                        datas.ArrayCandles[i],
                        datas.ArrayMax[i],
                        datas.ArrayMin[i],
                        datas.ArrayCandles[i + 1]
                    ]
                });
            }
        }
    });


    var options = {
        chart: {
            type: 'candlestick',
        },
        series: [{
            data: dataPoints
        }],
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

    if (dataPoints == null)
        return;

    chart.render();
}

function myFunction() {
    var id_tool = $('#test > option:selected').attr("value");
    loadGlass(id_tool);
    loadCandles(id_tool);
    loadKotirovka(90, 70, 120); //?
}

loadKotirovka('#cotirovka-info', 90, 70, 120);
