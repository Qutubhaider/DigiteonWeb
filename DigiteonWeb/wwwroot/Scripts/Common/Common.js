function loadMyRequest(fsURL, fsMethod, foJsonBody, fsSuccessFunction, fsErrorFunction = null, foElement = null, foDate = null) {

    $.ajax({
        url: fsURL,
        method: fsMethod,
        data: foJsonBody,
        success: function (data) {
            if (foElement != null && foDate != null)
                fsSuccessFunction(data, foElement, foDate);
            else if (foElement != null)
                fsSuccessFunction(data, foElement);
            else
                fsSuccessFunction(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (textStatus == "error") {
                if (jqXHR.status == "403") {
                    window.location.href = '/Account/AccessDenied'
                }
                else {
                    if (fsErrorFunction == null)
                        alert("Error");
                    else
                        fsErrorFunction(jqXHR, textStatus, errorThrown);
                }
            }
            else {
                if (fsErrorFunction == null)
                    alert("Error");
                else
                    fsErrorFunction(jqXHR, textStatus, errorThrown);
            }
        }
    });
}

$(".allownumericwithoutdecimal").on("keypress keyup blur", function (event) {
    $(this).val($(this).val().replace(/[^\d].+/, ""));
    if ((event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});
$(".allownumericwithdecimal").on("keypress keyup blur", function (event) {
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) &&
        ((event.which < 48 || event.which > 57) &&
            (event.which != 0 && event.which != 8))) {
        event.preventDefault();
    }

    var text = $(this).val();

    if ((text.indexOf('.') != -1) &&
        (text.substring(text.indexOf('.')).length > 2) &&
        (event.which != 0 && event.which != 8) &&
        ($(this)[0].selectionStart >= text.length - 2)) {
        event.preventDefault();
    }
});

function showLoading() {
    if ($("div._loading").length === 0) {
        $("body").prepend("<div class='_loading'><div class='_loading_img'><i style='font-size:100px; color:#0bb197;' class='fas fa-spinner fa-spin'></i></div></div>");
    }

    $("div._loading").show();
}

function hideLoading() {
    $("div._loading").hide();
}

function validateEmail(email) {
    if (/^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(email)) {
        return (true)
    }
    if (email != "") {
        toastr.error('Please enter Vaild Email.');
    }
    return (false)
}

function convertDateStringToDate(dateStr) {
    let months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    let date = new Date(dateStr);
    let str = months[date.getMonth()]
        + ' ' + date.getDate()
        + ', ' + date.getFullYear()
    return str;
}



