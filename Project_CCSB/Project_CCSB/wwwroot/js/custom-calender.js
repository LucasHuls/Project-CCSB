var routeURL = location.protocol + "//" + location.host;
$(document).ready(function () {
    $("#DateAndTime").kendoDateTimePicker({
        value: new Date(),
        dateInput: false
    });
    InitializeCalendar();
});
var calendar;
function InitializeCalendar() {
    try {
        var calendarEl = document.getElementById('calendar');
        if (calendarEl != null) {
            calendar = new FullCalendar.Calendar(calendarEl, {
                locale: 'nl',
                initialView: 'dayGridMonth',
                headerToolbar: {
                    left: 'prev,next,today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                selectable: true,
                weekNumbers: false,
                editable: false,
                select: function (event) {
                    onShowModal(event, null);
                }
            });
            calendar.render();
        }
    }
    catch (e) {
        alert(e);
    }
}

function onShowModal(obj, isEventDeail) { //Opens popup modal
    $("#appointmentInput").modal("show");
}

function onCloseModal(obj, isEventDeail) { //Closes popup modal
    $("#appointmentInput").modal("hide");
}

function onSubmitForm() {
    var requestData = {
        Date: $('#date').val(),
        LicensePlate: $('#licensePlate').val(),
        AppointMentType: $('#appointmentType').val()
    }
    console.log(requestData);

    $.ajax({
        url: routeURL + "/api/AppointmentApi/SaveCalendarData",
        type: "POST",
        data: JSON.stringify(requestData),
        contentType: "application/json",
        success: function (response) {
            if (response.status === 1 || response.status === 2) {
                $.notify(response.message, "succes");
                onCloseModal();
            } else {
                $.notify(response.message, "error");
            }
        },
        error: function (xhr) {
            $.notify("Error", "Foutje");
        }
    })
}