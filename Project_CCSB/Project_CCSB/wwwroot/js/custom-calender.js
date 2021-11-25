const { Alert } = require("bootstrap");

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
                    GetUser(document.getElementById("licensePlate").value);
                },
                eventClick: function (info) {   // If appointment is clicked
                    OpenPopUp(info.event);
                    GetUserRemove(document.getElementById("licensePlateRemove").value);
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
                                        textColor: 'black',
                                        userName: data.ApplicationUserFullName
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

function onShowModal(obj, isEventDeail, eventDate) {    // Opens popup modal
    $("#appointmentInput").modal("show");
    $('#dateInput').eq(0).val(eventDate + "T12:00");

    ClearDOMElements();
}

function onCloseModal() {   // Close popup modal
    $("#appointmentInput").modal("hide");
}

function OpenPopUp(e) {  // Opens Remove pop up
    $('#removeAppointmentDiv').data("oldData", e); // Set data so that if inputs are changed appointment can still be deleted
    
    $("#removeAppointment").modal("show");
    $("#subtitleRemove").text(date);
    var date = ConvertStringToDateInput(e.start);
    $('#dateInputRemove').eq(0).val(date);
    $("#appointmentTypeRemove").val(e.extendedProps.description);
    $("#licensePlateRemove").val(e.title);
    $("#userRemove").val(e.extendedProps.userName);
}

function ConvertStringToDateInput(string) {
    var date = new Date(string),
        mnth = ("0" + (date.getMonth() + 1)).slice(-2),
        day = ("0" + date.getDate()).slice(-2);
    hours = ("0" + date.getHours()).slice(-2);
    minutes = ("0" + date.getMinutes()).slice(-2);
    return [date.getFullYear(), mnth, day].join("-") + "T" + hours + ":" + minutes;
}

function ClosePopUp() { // Closes Remove pop up
    $("#removeAppointment").modal("hide");
}

function ClearDOMElements(dateEl, dateElValidation, appointmentEl, appointmentElValidation) {
    $("#" + dateEl).html("");
    $("#" + dateElValidation).html("");

    $("#" + appointmentElValidation).html("");
}

function ValidateNewAppointment(dateEl, dateElValidation, appointmentEl, appointmentElValidation) {
    ClearDOMElements(dateEl, dateElValidation, appointmentEl, appointmentElValidation);

    // Validate appointment type
    var typeCheck = $("#" + appointmentEl).val();
    if (typeCheck == "Kies brengen of ophalen") {
        $("#" + appointmentElValidation).html("Kies tussen brengen of ophalen!");
        return false;
    }

    var date = $("#" + dateEl).val();
    // Validate if date input is a real date
    if (isNaN(Date.parse(date))) {
        $("#" + dateElValidation).html("Vul een geldige datum in!");
        return false;
    }

    // Validate appointment if date is in the past
    var test = new Date(date);

    if (test < new Date()) {
        $("#" + dateElValidation).html("Datum is in het verleden!");
        return false;
    }

    var dateSplit = date.split("-");
    var day = parseInt(dateSplit[2].substring(0, 2));
    var month = parseInt(dateSplit[1]);

    var currentDate = new Date().toISOString();
    var currentDay = parseInt(currentDate.split("-")[2].substring(0, 2));
    var currentMonth = new Date().getMonth() + 1; // getMonth() is 0 Indexed
    
    if ((day - currentDay) <= 2 && month == currentMonth) {
        $("#" + dateElValidation).html("Afspraak moet 2 dagen van tevoren gepland worden!");
        return false;
    }

    // Passed validation
    return true;
}

function onSubmitForm() {
    if (!ValidateNewAppointment("dateInput", "dateValidation", "appointmentType", "appointmentTypeValidation")) {
        return;
    }

    var requestData = {
        Date: $('#dateInput').val(),
        LicensePlate: $('#licensePlate').val(),
        AppointMentType: $('#appointmentType').val()
    }

    $.ajax({
        url: routeURL + "/api/AppointmentApi/SaveCalendarData",
        type: "POST",
        data: JSON.stringify(requestData),
        contentType: "application/json",
        success: function (response) {
            if (response.status === 1 || response.status === 2 || response.status === 3) {
                $.notify(response.message, "succes");
                onCloseModal();
                calendar.refetchEvents()
            } else {
                $.notify("Er is iets fout gegaan.", "error");
            }
        },
        error: function (xhr) {
            $.notify("Error", "Foutje");
        }
    })
}

function DeleteAppointment() {
    var data = $("#removeAppointmentDiv").data("oldData");

    var date = data.start;
    date = date.toString().substring(0, 33);

    $.ajax({
        url: "https://localhost:5001/api/AppointmentApi/DeleteAppointment",
        type: "Delete",
        headers: {
            "startDate": date
        },
        success: function (response) {
            calendar.refetchEvents();
            ClosePopUp();
        }
    });
}

function EditAppointment() {
    if (!ValidateNewAppointment("dateInputRemove", "dateInputRemoveValidation", "appointmentTypeRemove", "appointmentTypeRemoveValidation")) {
        return;
    }

    var requestData = {
        Date: $('#dateInputRemove').val(),
        LicensePlate: $('#licensePlateRemove').val(),
        AppointMentType: $('#appointmentTypeRemove').val()
    }

    var oldDate = $("#removeAppointmentDiv").data("oldData").start;
    oldDate = oldDate.toString().substring(0, 33);

    $.ajax({
        url: routeURL + "/api/AppointmentApi/EditAppointment",
        type: "POST",
        data: JSON.stringify(requestData),
        contentType: "application/json",
        headers: {
            "oldDate": oldDate
        },
        success: function (response) {
            if (response.status === 2) {
                $.notify(response.message, "succes");
                ClosePopUp();
                calendar.refetchEvents();
            } else {
                $.notify(response.message, "error");
            }
        },
        error: function (err) {
            $.notify("Error", "Foutje");
            Alert(err);
        }
    });
}

function getColorBasedOnType(type) {
    if (type == "Brengen") {
        return "green";
    } else {
        return "red";
    }
}

function GetUser(licensePlate) {
    fetch(routeURL + "/api/AppointmentApi/GetUserByVehicle/" + licensePlate, {
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        }
    })
        .then((response) => response.json())
        .then((data) => {
            document.getElementById("userText").innerText = data;
        });
}

function GetUserRemove(licensePlate) {
    fetch(routeURL + "/api/AppointmentApi/GetUserByVehicle/" + licensePlate, {
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        }
    })
        .then((response) => response.json())
        .then((data) => {
            document.getElementById("userTextRemove").innerText = data;
        });
}