$(document).ready(function () {
});

function loadGlass(id) {
    var callback = function (stakan) {
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

    $.get("/api/values/getstakanbyid/" + id, callback);
}

function loadCandles(id) {
    $("#chart_div").html('');
    var dataPoints = [];
    var callback = function (datas) {
        if (datas == null)
            return;

        for (var i = 0; i < datas.ArrayCandles.length; i++) {
            dataPoints.push({
                x: new Date(2018,11,i+1),//datas.ArrayTime[i]),
                y: [
                    datas.ArrayCandles[i],
                    datas.ArrayMax[i],
                    datas.ArrayMin[i],
                    datas.ArrayCandles[i + 1]
                ]
            });
        }
    }

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

    $.ajax({
        url: "/api/values/getdatacandlestikbyid/" + id,
        type: "get",
        async: false,
        success: callback
    });

    if (dataPoints == null)
        return;

    chart.render();
}

function loadDeal(id) {
    var callback = function (data) {
        console.log(data);
    }

    $.get("/api/values/getdealbyid/" + id, callback);
}

function myFunction() {
    var id_tool = $('#test > option:selected').attr("value");
    var name = $('#test > option:selected').text();
   document.title = 'Инструмент ' + name;  
    loadGlass(id_tool);
    loadCandles(id_tool);
    loadKotirovka(90, 70, 120);
    loadDeal(id_tool);
}

loadKotirovka('#cotirovka-info', 90, 70, 120);
