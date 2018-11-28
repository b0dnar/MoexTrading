var loadKotirovka = function (tag, width1, width2, width3) {
    $.get("/api/values/getdatakotirovka", function (cotir) {
        var table_body = '<table border="1"><thead><tr align="center"><th width="' + width1 + '">Инструменты</th><th width="' + width2 + '">Цена</th><th width="' + width3 + '">Изминение</th></tr></thead><tbody>';

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
        $(tag).html(table_body);
    });
}

//loadKotirovka();width="90"   width="70"   width="150"