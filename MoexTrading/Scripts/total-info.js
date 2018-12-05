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
                x: new Date(datas.ArrayTime[i]),
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
    if (id == 0) {

    }
    
    var callback = function (data) {
        var text = '<div id="deal-row"><div class="row"><div class="col-md-1" id="deal-cursor"></div><button class="col-md-4 deal-sell"><div class="text-sell">Продажа</div><div class="value-deal">' + data.Sell + '</div></button><div class="col-md-2"><input class="text-value" type="text" value="0.00"></div><button class="col-md-4 deal-buy"><div class="text-buy">Покупка</div><div class="value-deal">' + data.Buy + '</div></button></div></div>';
        $('#deal-info').html(text);
    }

    $.get("/api/values/getdealbyid/" + id, callback);
}

var get = function (el) {
    if (typeof el === 'string') {
        return document.querySelector(el);
    }
    return el;
};

var dragable = function (parentEl, dragEl) {
    var parent = get(parentEl);
    var target = get(dragEl);
    var drag = false;
    offsetX = 0;
    offsetY = 0;
    var mousemoveTemp = null;

    if (target) {
        var mouseX = function (e) {
            if (e.pageX) {
                return e.pageX;
            }
            if (e.clientX) {
                return e.clientX + (document.documentElement.scrollLeft ?
                    document.documentElement.scrollLeft :
                    document.body.scrollLeft);
            }
            return null;
        };

        var mouseY = function (e) {
            if (e.pageY) {
                return e.pageY;
            }
            if (e.clientY) {
                return e.clientY + (document.documentElement.scrollTop ?
                    document.documentElement.scrollTop :
                    document.body.scrollTop);
            }
            return null;
        };

        var move = function (x, y) {
            var xPos = parseInt(target.style.left) || 0;
            var yPos = parseInt(target.style.top) || 0;

            target.style.left = (xPos + x) + 'px';
            target.style.top = (yPos + y) + 'px';
        };

        var mouseMoveHandler = function (e) {
            e = e || window.event;
            if (!drag) { return true };

            var x = mouseX(e);
            var y = mouseY(e);
            if (x != offsetX || y != offsetY) {
                move(x - offsetX, y - offsetY);
                offsetX = x;
                offsetY = y;
            }
            return false;
        };

        var start_drag = function (e) {
            e = e || window.event;

            offsetX = mouseX(e);
            offsetY = mouseY(e);
            drag = true; // basically we're using this to detect dragging

            // save any previous mousemove event handler:
            if (document.body.onmousemove) {
                mousemoveTemp = document.body.onmousemove;
            }
            document.body.onmousemove = mouseMoveHandler;
            return false;
        };

        var stop_drag = function () {
            drag = false;

            // restore previous mousemove event handler if necessary:
            if (mousemoveTemp) {
                document.body.onmousemove = mousemoveTemp;
                mousemoveTemp = null;
            }
            return false;
        };

        target.onmousedown = start_drag;
        parent.onmouseup = stop_drag;
    }
}

dragable('#container', '#deal-info');

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
