﻿function DeleteBlockedDate() {
    return;

    var data = $('#removeAppointmentDiv').data('oldDate');
    var date = data.start;
    date = date.toString().substring(0, 33);

    $.ajax({
        url: "https://localhost:5001/api/DateBlockingApi/DeleteBlockedDate",
        type: "Delete",
        headers: {
            "startDate": date
        }
    });
}