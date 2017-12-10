$(function () {
    var fromDate = $('#FromDate').val();;
    var toDate = $('#ToDate').val();
    $('#ToDate').attr('min', fromDate);

    if (fromDate != undefined && toDate != undefined && fromDate != '' && toDate != '') {
        var leaveInDays = CalculateLeaveDays(fromDate, toDate);
        CalculateBalance(leaveInDays);
    }
});


//$('#submit').click(function (e) {
//    debugger;
//    var form = $('#form');
//    e.preventDefault();

//    NotyConfirm(form);
//});

$('#FromDate').change(function () {

    if (this.value === "") {
        CalculateBalance(0);
        $("#submit").attr('disabled', 'disabled');
        $("#edit").attr('disabled', 'disabled');
    }

    var fromDate = this.value;
    var toDate = $('#ToDate').val();
    $('#ToDate').attr('min', fromDate);

    if (fromDate != undefined && toDate != undefined && fromDate != '' && toDate != '') {
        var leaveInDays = CalculateLeaveDays(fromDate, toDate);
        CalculateBalance(leaveInDays);
    }
});

$('#ToDate').change(function () {
    if (this.value === "") {
        CalculateBalance(0);
        $("#submit").attr('disabled', 'disabled');
        $("#edit").attr('disabled', 'disabled');
    }

    var toDate = this.value;
    var fromDate = $('#FromDate').val();

    if (fromDate != undefined && toDate != undefined && fromDate != '' && toDate != '') {
        toDate.min = fromDate;
        var leaveInDays = CalculateLeaveDays(fromDate, toDate);
        CalculateBalance(leaveInDays);
    }
});

function CalculateLeaveDays(from, to) {
    var duration = (new Date(to) - new Date(from)) / (1000 * 60 * 60 * 24);
    return duration;
}

function CalculateBalance(leaveInDays) {
    var currentBalance = $("#Balance").val();
    var newBalance = currentBalance - leaveInDays;

    if (newBalance < 0) {
        $("#submit").attr('disabled', 'disabled');
        $("#edit").attr('disabled', 'disabled');
        $("#error").text('Your leave request is not within the available balance. You can only take a maximum of ' + currentBalance + ' days.');
    } else {
        $("#submit").removeAttr('disabled');
        $("#edit").removeAttr('disabled');
        $("#error").text('');
    }

    $("#LeaveRequestedInDays").val(leaveInDays);
    $("#LeaveBalanceInDays").val(newBalance);
}

function NotyConfirm(form) {

    noty({
        title: 'Confirm Leave Request',
        text: 'Are you sure you want to submit the leave request?',
        layout: 'center',
        modal: true,
        buttons: [
          {
              addClass: 'btn btn-primary', text: 'Yes', onClick: function ($noty) {

                  $noty.close(); 
                  form.submit();
              }
          },
          {
              addClass: 'btn btn-danger', text: 'No', onClick: function ($noty) {
                  $noty.close();
                  Noty('You have cancelled your request.', 'warning');
              }
          }
        ]
    });
}

function Noty(message, type) {
    var n = noty({
        text: message,
        type: type,
        timeout: 2000,
        animation: {
            open: { height: 'toggle' },
            close: { height: 'toggle' },
            easing: 'swing',
            speed: 500
        }
    });
}