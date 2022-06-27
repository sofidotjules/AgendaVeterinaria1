DevExpress.localization.locale(navigator.language);
var date = new Date().getTime();
var fechaGlobal;
$(function () {
    $('#Especialidades').on('change', '', function (e) {
        listaCargada = false;
        $('#horariosDisponiblesOpciones div').empty();
        $("#divSolicitarTurno").css('visibility', 'hidden');
    });

});

function OnInitialized(s, e) {
    minValue_changed(true);
    disabledDates_changed(true);
    firstDayOfWeek_changed(false)
}

function getCalendarInstance() {
    return $("#calendar-container").dxCalendar("instance");
}

function isWeekend(date) {
    var day = date.getDay();

    return day === 0 || day === 6;
}

function getCellTemplate(data) {
    var cssClass = "";

    if (data.view === 'month') {
        if (isWeekend(data.date))
            cssClass = "weekend";

        $.each([[1, 0], [4, 6], [25, 11]], function (_, item) {
            if (data.date.getDate() === item[0] && data.date.getMonth() === item[1]) {
                cssClass = "holyday";
                return false;
            }
        });
    }

    return "<span class='" + cssClass + "'>" + data.text + "</span>";
}

function calendar_valueChanged(data) {
    $("#selected-date").dxDateBox("instance").option("value", data.value);
}

function minValue_changed(data) {
    var calendar = getCalendarInstance();
    //if (data.value) {

    calendar.option("min", new Date(date - 1000 * 60 * 60 * 24 * 3));
    //  } else {
    //    calendar.option("min", null);
    //}
}

function maxValue_changed(data) {
    var calendar = getCalendarInstance();
    if (data.value) {
        calendar.option("max", new Date(date + 1000 * 60 * 60 * 24 * 3));
    } else {
        calendar.option("max", null);
    }
}

function disabledDates_changed(data) {
    var calendar = getCalendarInstance();
    // if (data.value) {
    calendar.option("disabledDates", function (data) {
        return isWeekend(data.date);
    });
    // } else {
    //    calendar.option("disabledDates", null);
    // }
}

function firstDayOfWeek_changed(data) {
    var calendar = getCalendarInstance();
    if (data.value) {
        calendar.option("firstDayOfWeek", 1);
    } else {
        calendar.option("firstDayOfWeek", 0);
    }
}

function useCustomTemplate(data) {
    getCalendarInstance().option("cellTemplate", data.value ? getCellTemplate : "cell");
}

function disabledState_changed(data) {
    getCalendarInstance().option("disabled", data.value);
}

function zoomLevel_changed(data) {
    getCalendarInstance().option("zoomLevel", data.value);
}

function selectedDate_changed(data) {

    getCalendarInstance().option("value", data.value);
    $('#txtDetalle').val('');
    $('#horariosDisponiblesOpciones div').empty();
    loadTablaTurnos(data.value);
}

function loadTablaTurnos(fecha) {

    var idEspecialidad = $("#Especialidades option:selected").val();
    fechaGlobal = fecha.toLocaleDateString("fr-CA", { year: "numeric", month: "2-digit", day: "2-digit" }).split('T')[0];
    $.ajax({
        type: "GET",
        url: "@Url.Action("ObtenerHorasTurno","Turno")",
        data: { fecha: fechaGlobal, idEspecialidad: idEspecialidad, tipoTurno: "Veterinario" },
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result[0] != undefined) {
                var horariosDisponibles = result;
                $('#horariosDisponiblesOpciones div').empty();
                $('#txtDetalle').empty();
                $("#divSolicitarTurno").css('visibility', 'visible');
                var div = document.createElement("div");
                div.id = "horariosDisponibles";
                div.style = 'padding-left:10px'
                var foo = document.getElementById("horariosDisponiblesOpciones");
                foo.appendChild(div);
                var i = 0
                $.each(horariosDisponibles, function (index, value) {
                    i++
                    var element = document.createElement("a");
                    element.type = "button";
                    element.className = "btn btn-info btn-small pull-left"
                    element.text = value;
                    element.id = fecha;
                    element.style = ' margin-right: 4px; margin-top:4px; color:white'
                    element.onclick = function () {
                        $(".active").removeClass("active");
                        $(this).addClass("active");
                    };

                    var foo = document.getElementById("horariosDisponibles");
                    foo.appendChild(element);

                });
            }
            else {
                swal("Error", "No se han encontrado profesionales para la fecha indicada", "error");

            }

        },
        error: function (response) {
            swal("Error", "No se han encontrado profesionales para la fecha indicada", "error");

        }
    });

}

function msjConfirmar() {
    swal({
        text: "Se reservará un turno para la especialidad: " + $("#Especialidades option:selected").text() + ". El día " + fechaGlobal + " a las " + $(" .active").text(),
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((aceptar) => {
            if (aceptar) {
                swal("Exito! Le llegará un correo con la información necesaria para el turno", {
                    icon: "success",

                });
                //document.form.submit();
                document.forms['Test'].submit();
            }
        });
}