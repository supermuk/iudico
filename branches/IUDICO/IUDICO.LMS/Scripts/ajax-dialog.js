function openDialog(title) {
    $("#dialogInner").html('<span class="loading"><img src="/Content/Images/wait.gif"/></span>');
    $("#dialog").dialog("option", "title", title);
    $("#dialog").dialog("open");
}

function openShareDialog(url, data, onSuccess) {
    $("#shareDialogInner").html('<span class="loading"><img src="/Content/Images/wait.gif"/></span>');
    $("#shareDialog").dialog("open");
    
    $.ajax({
        type: "get",
        url: url,
        data: data,
        success: function (r) {
            var form = "<form action='" + url + "' data-onSuccess='" + onSuccess + "' data-onFailure='onFailure'>" + r + "</form>";

            $("#shareDialogInner").html(form);

            var table = $("#shareUserTable").dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                "sScrollY": "360px",
                iDisplayLength: 8,
                "bSort": true,
                "bAutoWidth": false,
                "aoColumns": [
                           { "bSortable": false },
                           { "bSortable": false },
                           null
                       ]
            });

            for(var prop in data) {
                $('<input />').attr('type', 'hidden')
                  .attr('name', prop)
                  .attr('value', data[prop])
                  .appendTo('#shareDialogInner > form');
            }
        }
    });
}
        
function fillDialogInner(html, itemName, itemId) {
    $("#dialogInner").html(html);
    $('<input />').attr('type', 'hidden')
        .attr('name', itemName)
        .attr('value', itemId)
        .appendTo('#dialogInner > form');
}
function onFailure(r) {
    alert("error");
}
function setupDialog(submitName, cancelName) {
    $("#dialog").dialog({
        autoOpen: false,
        modal: true,
        buttons: [
            {
                text: submitName,
                click: function() {
                    var $form = $("#dialog").find("form");

                    $.ajax({
                        type: "post",
                        url: $form.attr("action"),
                        data: $form.serialize(),
                        success: function(r) {
                            window[$form.attr("data-onSuccess")](r);
                        },
                        error: function(r) {
                            window[$form.attr("data-onFailure")](r);
                        }
                    });
                },
                'class': "blueButton",
                id: "DialogSubmitButton"
            },
            {
                text: cancelName,
                click: function() {
                    $(this).dialog("close");
                },
                'class': "blueButton"
            }
        ]
    });

    $("body").delegate("#dialog form", "submit", function (event) {
        event.preventDefault();
        event.stopPropagation()
        $('#DialogSubmitButton').click(); 
    });
}

function setupShareDialog (shareName, cancelName) {
    $("#shareDialog").dialog({
        autoOpen: false,
        modal: true,
        width: 450,
        height: 552,
        dialogClass: "hiddenTitle",
        resizable: false,
        buttons: [
            {
                text: shareName,
                click: function() {
                    var $form = $("#shareDialog").find("form");

                    //submit rows not visible in table(on other pages)
                    var table = $("#shareUserTable").dataTable();
                    var hiddenRows = table.fnGetHiddenNodes();
                    $(hiddenRows).css('display', 'none');
                    table.find("tbody").append(hiddenRows);

                    $.ajax({
                        type: "post",
                        url: $form.attr("action"),
                        data: $form.serialize(),
                        success: function(r) {
                            window[$form.attr("data-onSuccess")](r);
                        },
                        error: function(r) {
                            window[$form.attr("data-onFailure")](r);
                        }
                    });
                },
                'class': "blueButton"
            },
            {
                text: cancelName,
                click: function () {
                    $(this).dialog("close");
                },
                'class': "blueButton"
            }
        ]
    });
}