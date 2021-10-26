var routeURL = location.protocol + "//" + location.host;
$(document).ready(function () {
    $("#DateAndTime").kendoDateTimePicker({
        value: new Date(),
        dateInput: false
    });
    InitializeCalendar();
});
var calendar;
async function InitializeCalendar() {
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
                dateClick: function (info) {
                    onShowModal(event, null, info.dateStr);
                },
                eventClick: function (info) {
                    openEventOnClick();
                },
                events: function (fetchInfo, successCallback, failureCallback) {
                    $.ajax({
                        url: '/api/AppointmentApi/GetCalendarEvents',
                        type: "GET",
                        dataType: "JSON",
                        success: function (result) {
                            var events = [];
                            $.each(result, function (i, data) {
                                events.push(
                                    {
                                        title: data.LicensePlate,
                                        description: data.AppointmentType,
                                        start: data.Date,
                                        end: data.Date,
                                        color: getColorBasedOnType(data.AppointmentType),
                                        textColor: 'black'
                                    });
                            });
                            successCallback(events);
                        },
                    });
                }      
            });
            calendar.render();
        }
    }
    catch (e) {
        alert(e);
    }
}

function onShowModal(obj, isEventDeail, eventDate) { //Opens popup modal
    $("#appointmentInput").modal("show");
    $('#dateInput').eq(0).val(eventDate + "T12:00");
}

function openEventOnClick(eventTitle, eventDate, eventLicensePlate) { //Opens eventeditor modal
    $("#removeAppointment").modal("show");
}

function onCloseModal(obj, isEventDeail) { //Closes popup modal
    $("#appointmentInput").modal("hide");
}

function onSubmitForm() {
    var requestData = {
        Date: $('#dateInput').val(),
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
                calendar.refetchEvents()
            } else {
                $.notify(response.message, "error");
            }
        },
        error: function (xhr) {
            $.notify("Error", "Foutje");
        }
    })
}

function getColorBasedOnType(type) {
    if (type == "Brengen") {
        return "green";
    } else {
        return "red";
    }
}