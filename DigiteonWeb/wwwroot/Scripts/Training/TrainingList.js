var lsSortCol = '';
var lsSortOrder = '';
var lsPageNo = 1;

function sort(fsSortColumn, element) {
    if ($(element).closest('th').hasClass("sorting_asc")) {
        $(element).closest('th').removeClass("sorting_asc").addClass("sorting_desc");
        lsSortOrder = 'desc';
        lsSortCol = fsSortColumn;
        getTrainingData(lsPageNo, fsSortColumn, "desc");
    }
    else {
        $(element).closest('th').removeClass("sorting_desc").addClass("sorting_asc");
        lsSortOrder = 'asc';
        lsSortCol = fsSortColumn;
        getTrainingData(lsPageNo, fsSortColumn, "asc");
    }
}

function getTrainingData(fsPageNo, fsSortColumn = lsSortCol, fsSortDirection = lsSortOrder) {
    showLoading();   
    var liSize = $('#ddPageSize').val();

    var loData = new Object();
    var pg = fsPageNo;
   
    loData.sort_column = fsSortColumn;
    loData.sort_order = fsSortDirection;
    loData.pg = pg;
    loData.size = liSize;

    loadMyRequest(msGetTrainingDataList, "GET", loData, getTrainingDataSuccess, getTrainingDataError);
    $('#preloader').hide();
}

function getTrainingDataError(foResponse) {
    hideLoading();
}

function getTrainingDataSuccess(foResponse) {
    showLoading();
    if (foResponse.indexOf('No records found.') > -1) {
        lshasMoreRecords = false;
        $('#TrainingList').find("table tbody").html(foResponse);
        $('#paginationList').html('');
        $('#spnNoOfRecordsMessage').html('');
        $('#ddPageSize').hide();
        hideLoading();
    }
    else {
        $('#ddPageSize').show();
        $('#TrainingList').find("table tbody").empty();
        $('#TrainingList').find("table tbody").append(foResponse);
        if (lsSortCol != '') {
            $(".sorting").removeClass("sorting_asc");
            $(".sorting").removeClass("sorting_desc");
            if (lsSortOrder == "asc") {
                $("#" + lsSortCol).addClass('sorting_asc');
            }
            else {
                $("#" + lsSortCol).addClass('sorting_desc');
            }
        }
        GeneratePageListpanel(null);
        $('#spnNoOfRecordsMessage').html($("#hdnNoOfRecordsMessage").val());
        hideLoading();
    }
}



function GeneratePageListpanel(fsvalue, ffName) {
    $('#paginationList').html('');
    var liListPanelSize = 5;
    if (fsvalue == 1) {
        var fiPageNoListPanelNext = parseInt($('#hdnPageNoListPanel').val()) + 1;
        $('#hdnPageNoListPanel').val(fiPageNoListPanelNext);
    }
    else if (fsvalue == 0) {
        var fiPageNoListPanelNext = parseInt($('#hdnPageNoListPanel').val()) - 1;
        $('#hdnPageNoListPanel').val(fiPageNoListPanelNext);
    }
    else if (fsvalue == "First") {
        $('#hdnPageNoListPanel').val(1);
    }
    else if (fsvalue == "Last") {
        var liTotalRec = $('#hdnTotalRecords').val();
        var liPageSize = $('#ddPageSize').val();
        var liTotalpages = Math.ceil(liTotalRec / liPageSize);
        var TotalPanel = Math.ceil(liTotalpages / 5);

        $('#hdnPageNoListPanel').val(TotalPanel);
    }
    else {
        var fiPageNoListPanelPrev = 1;
        if ($('#hdnCurrentPage').val() == 1) {
            $('#hdnPageNoListPanel').val(fiPageNoListPanelPrev);
        }
    }

    var liCounter = 0;
    var lsHTML = "<ul class='pagination'>";
    var liTotalRec = $('#hdnTotalRecords').val();
    var liPageSize = $('#ddPageSize').val();
    var liTotalpages = Math.ceil(liTotalRec / liPageSize);
    for (liCounter = 0; liCounter <= liListPanelSize + 1; liCounter++) {
        var liPageNo = liListPanelSize * $('#hdnPageNoListPanel').val() - liListPanelSize + liCounter;
        if (liCounter == 0) {
            if (liPageNo > 0) {
                lsHTML = lsHTML + '<li class="paginate_button page-item first"><a href="javascript:;" data-dt-idx="0" tabindex="0" class="page-link">First</a></li>';
                lsHTML = lsHTML + '<li class="paginate_button page-item previous"><a href="javascript:;" data-dt-idx="1" tabindex="0" class="page-link">Previous</a></li>';
            }
            else {
                lsHTML = lsHTML + '<li class="paginate_button page-item first disabled"><a href="javascript:onclick=GeneratePageListpanel(0);" data-dt-idx="0" tabindex="0" class="page-link">First</a></li>';
                lsHTML = lsHTML + '<li class="paginate_button page-item previous disabled"><a href="javascript:;" data-dt-idx="1" tabindex="0" class="page-link">Previous</a></li>';
            }
        }
        else if (liCounter == liListPanelSize + 1) {

            if (liPageNo <= liTotalpages) {
                lsHTML = lsHTML + '<li class="paginate_button page-item next"><a href="javascript:;" data-dt-idx="5" tabindex="0" class="page-link">Next</a></li>';
                lsHTML = lsHTML + '<li class="paginate_button page-item last"><a href="javascript: onclick=SetPageOrder(0);" data-dt-idx="6" tabindex="0" class="page-link">Last</a></li>';
            }
            else {
                lsHTML = lsHTML + '<li class="paginate_button page-item next disabled"><a href="javascript:;" data-dt-idx="5" tabindex="0" class="page-link">Next</a></li>';
                lsHTML = lsHTML + '<li class="paginate_button page-item last disabled"><a href="javascript:;" data-dt-idx="6" tabindex="0" class="page-link">Last</a></li>';
            }
        }
        else {
            if (liPageNo <= liTotalpages) {
                lsHTML = lsHTML + '<li id="pagging_' + liPageNo + '" class="paginate_button page-item "><a href="javascript:onclick=getTrainingData(' + liPageNo + ');" data-dt-idx="3" tabindex="0" class="page-link" id=aLinkPageNo' + liPageNo + '>' + liPageNo + '</a></li>';
            }
        }
    }
    lsHTML = lsHTML + "</ul>";
    $('#paginationList').html(lsHTML);
    $("#pagging_" + $('#hdnCurrentPage').val()).addClass("active");

}

function SetPageOrder(Pageindex) {
    if (Pageindex == 1) {
        GeneratePageListpanel('First');
    }
    else {
        GeneratePageListpanel('Last');
    }
}