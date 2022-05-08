function OnZoneChange(flgIsEdit = false) {
    var loData = new Object();
    if (flgIsEdit) {
        var liZoneId = $('#inZoneId').val();
    }
    else {
        var liZoneId = $('#ddZone').val();
    }
    loData.fiZoneId = liZoneId;

    if ($("#ddDivision").length > 0) {
        $("#ddDivision").empty();
        $("#ddDivision").append("<option value='" + "" + "'>" + "Select Division" + "</option>");
    }
    if ($("#ddDesk").length > 0) {
        $("#ddDesk").empty();
        $("#ddDesk").append("<option value='" + "" + "'>" + "Select Desk" + "</option>");
    }
    loadMyRequest(msGetDivisionDropDown, "GET", loData, function (response) {
        $("#ddDivision").append("<option value='" + "" + "'>" + "Select Division" + "</option>");
        response.data.forEach(d => $("#ddDivision").append("<option value='" + d.id + "'>" + d.value + "</option>"));
        if (flgIsEdit)
            $("#ddDivision").val($("#inDivisionId").val());
    }, function () { });
}

function OnDivisionChangeLoadDesk(flgIsEdit = false) {
    var loData = new Object();
    if (flgIsEdit) {
        var liDivisionId = $('#inDivisionId').val();
    }
    else {
        var liDivisionId = $('#ddDivision').val();
    }

    loData.fiDivisionId = liDivisionId;
    if ($("#ddDesk").length > 0) {
        $("#ddDesk").empty();
        $("#ddDesk").append("<option value='" + "" + "'>" + "Select District" + "</option>");
    }
    loadMyRequest(msGetDeskDropDown, "GET", loData, function (response) {
        //console.log(response);
        $("#ddDesk").empty();
        $("#ddDesk").append("<option value='" + "" + "'>" + "Select District" + "</option>");
        response.data.forEach(d => $("#ddDesk").append("<option value='" + d.id + "'>" + d.value + "</option>"));
        if (flgIsEdit)
            $("#ddDesk").val($("#inDeskid").val());
    }, function () { });
}

function OnDivisionChangeLoadStore(flgIsEdit = false) {
    var loData = new Object();
    if (flgIsEdit) {
        var liDivisionId = $('#inDivisionId').val();
    }
    else {
        var liDivisionId = $('#ddDivision').val();
    }

    loData.fiDivisionId = liDivisionId;
    if ($("#ddStore").length > 0) {
        $("#ddStore").empty();
        $("#ddStore").append("<option value='" + "" + "'>" + "Select Store" + "</option>");
    }
    loadMyRequest(msGetStoreDropDown, "GET", loData, function (response) {
        //console.log(response);
        $("#ddStore").empty();
        $("#ddStore").append("<option value='" + "" + "'>" + "Select Store" + "</option>");
        response.data.forEach(d => $("#ddStore").append("<option value='" + d.id + "'>" + d.value + "</option>"));
        if (flgIsEdit)
            $("#ddStore").val($("#inStoreId").val());
    }, function () { });
}

function OnStoreChange(flgIsEdit = false) {
    var loData = new Object();
    if (flgIsEdit) {
        var liStoreId = $('#inStoreId').val();
    }
    else {
        var liStoreId = $('#ddStore').val();
    }

    loData.fiStoreId = liStoreId;
    if ($("#ddRoom").length > 0) {
        $("#ddRoom").empty();
        $("#ddRoom").append("<option value='" + "" + "'>" + "Select Room" + "</option>");
    }
    loadMyRequest(msGetRoomDropDown, "GET", loData, function (response) {
        //console.log(response);
        $("#ddRoom").empty();
        $("#ddRoom").append("<option value='" + "" + "'>" + "Select Room" + "</option>");
        response.data.forEach(d => $("#ddRoom").append("<option value='" + d.id + "'>" + d.value + "</option>"));
        if (flgIsEdit)
            $("#ddRoom").val($("#inRoomId").val());
    }, function () { });
}

function OnRoomChange(flgIsEdit = false) {
    var loData = new Object();
    if (flgIsEdit) {
        var liRoomId = $('#inRoomId').val();
    }
    else {
        var liRoomId = $('#ddRoom').val();
    }

    loData.fiRoomId = liRoomId;
    if ($("#ddAlmirah").length > 0) {
        $("#ddAlmirah").empty();
        $("#ddAlmirah").append("<option value='" + "" + "'>" + "Select Almirah" + "</option>");
    }
    loadMyRequest(msGetAlmirahDropDown, "GET", loData, function (response) {
        //console.log(response);
        $("#ddAlmirah").empty();
        $("#ddAlmirah").append("<option value='" + "" + "'>" + "Select Almirah" + "</option>");
        response.data.forEach(d => $("#ddAlmirah").append("<option value='" + d.id + "'>" + d.value + "</option>"));
        if (flgIsEdit)
            $("#ddAlmirah").val($("#inAlmirahId").val());
    }, function () { });
}

function OnAlmirahChange(flgIsEdit = false) {
    var loData = new Object();
    if (flgIsEdit) {
        var liAlmirahId = $('#inAlmirahId').val();
    }
    else {
        var liAlmirahId = $('#ddAlmirah').val();
    }

    loData.fiAlmirahId = liAlmirahId;
    if ($("#ddShelve").length > 0) {
        $("#ddShelve").empty();
        $("#ddShelve").append("<option value='" + "" + "'>" + "Select Shelve" + "</option>");
    }
    loadMyRequest(msGetShelveDropDown, "GET", loData, function (response) {
        //console.log(response);
        $("#ddShelve").empty();
        $("#ddShelve").append("<option value='" + "" + "'>" + "Select Shelve" + "</option>");
        response.data.forEach(d => $("#ddShelve").append("<option value='" + d.id + "'>" + d.value + "</option>"));
        if (flgIsEdit)
            $("#ddShelve").val($("#inShelvesId").val());
    }, function () { });
}

function OnDepartmentChange(flgIsEdit = false) {
    var loData = new Object();
    if (flgIsEdit) {
        var liDepartmentId = $('#inDepartmentId').val();
    }
    else {
        var liDepartmentId = $('#ddDepartment').val();
    }

    loData.fiDepartmentId = liDepartmentId;
    if ($("#ddDesignation").length > 0) {
        $("#ddDesignation").empty();
        $("#ddDesignation").append("<option value='" + "" + "'>" + "Select Designation" + "</option>");
    }
    loadMyRequest(msGetDesignationDropDown, "GET", loData, function (response) {
        //console.log(response);
        $("#ddDesignation").empty();
        $("#ddDesignation").append("<option value='" + "" + "'>" + "Select Designation" + "</option>");
        response.data.forEach(d => $("#ddDesignation").append("<option value='" + d.id + "'>" + d.value + "</option>"));
        if (flgIsEdit)
            $("#ddDesignation").val($("#inDesignationId").val());
    }, function () { });
}

function OnSChange(flgIsEdit = false) {
    var loData = new Object();
    if (flgIsEdit) {
        var liDepartmentId = $('#inDepartmentId').val();
    }
    else {
        var liDepartmentId = $('#ddDepartment').val();
    }

    loData.fiDepartmentId = liDepartmentId;
    if ($("#ddDesignation").length > 0) {
        $("#ddDesignation").empty();
        $("#ddDesignation").append("<option value='" + "" + "'>" + "Select Designation" + "</option>");
    }
    loadMyRequest(msGetDesignationDropDown, "GET", loData, function (response) {
        //console.log(response);
        $("#ddDesignation").empty();
        $("#ddDesignation").append("<option value='" + "" + "'>" + "Select Designation" + "</option>");
        response.data.forEach(d => $("#ddDesignation").append("<option value='" + d.id + "'>" + d.value + "</option>"));
        if (flgIsEdit)
            $("#ddDesignation").val($("#inDesignationId").val());
    }, function () { });
}
