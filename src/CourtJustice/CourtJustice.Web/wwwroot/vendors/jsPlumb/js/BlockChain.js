
var document_id;
var document_name;
var document_prefix;
var position_top;
var position_left;
var mappings = [];

var dict = [];
var numFlowchart = 0;

jsPlumb.ready(function () {
    instance.batch(function () {
        _addEndpointOrganizations(org0, ["BottomCenter"]);
        _addEndpointOrganizations(org1, ["BottomCenter"]);
        _addEndpointOrganizations(org2, ["BottomCenter"]);
    });
    jsPlumb.fire("jsPlumbDemoLoaded", instance);
});




var instance = window.jsp = jsPlumb.getInstance({
    DragOptions: {
        cursor: 'pointer',
        zIndex: 2000
    },
    ConnectionOverlays: [
        ["Arrow", {
            location: 0.989,
            visible: true,
            width: 16,
            length: 16,
            id: "arrow",
            // events: {
            //     click: function () {
            //         alert("you clicked on the arrow overlay")
            //     }
            // }
        }],
        ["Label", {
            location: 0.5,
            id: "label",
            cssClass: "aLabel",
            // events: {
            //     tap: function () {
            //         alert("hey");
            //     }
            // }
        }]
    ],
    Container: "canvas"
});

var connectorPaintStyle = {
        lineWidth: 4,
        strokeStyle: "#61B7CF",
        joinstyle: "round",
        outlineStroke: "white",
        outlineWidth: 5
    },
    // .. and this is the hover style.
    connectorHoverStyle = {
        strokeWidth: 3,
        strokeStyle: "#216477",
        outlineWidth: 5,
        outlineStroke: "white"
    },
    endpointHoverStyle = {
        fillStyle: "#216477",
        strokeStyle: "#216477"
    },
    // the definition of source endpoints (the small blue ones)
    sourceEndpoint = {
        endpoint: "Dot",
        uuid: "flowchartWindow0",
        paintStyle: {
            strokeStyle: "#7AB02C",
            fillStyle: "transparent",
            radius: 9,
            strokeWidth: 1
        },
        isSource: true,
        connector: ["Flowchart", {
            stub: [40, 60],
            gap: 10,
            cornerRadius: 5,
            alwaysRespectStubs: false
        }],
        connectorStyle: connectorPaintStyle,
        hoverPaintStyle: endpointHoverStyle,
        connectorHoverStyle: connectorHoverStyle,
        dragOptions: {},
        overlays: [
            ["Label", {
                location: [0.5, 1.5],
                //label: "Drag",
                cssClass: "endpointSourceLabel",
                visible: false
            }]
        ]
    },
    // the definition of target endpoints (will appear when the user drags a connection)
    targetEndpoint = {
        endpoint: "Dot",
        paintStyle: {
            fillStyle: "#7AB02C",
            radius: 9
        },
        hoverPaintStyle: endpointHoverStyle,
        maxConnections: -1,
        dropOptions: {
            hoverClass: "hover",
            activeClass: "active"
        },
        isTarget: true,
        overlays: [
            ["Label", {
                location: [0.5, -0.5],
                /*label: "Drop",*/
                cssClass: "endpointTargetLabel",
                visible: false
            }]
        ]
    },
    init = function (connection) {
        connection.getOverlay("label").setLabel(connection.sourceId + " - " + connection.targetId);
    };

function ConnectLine(id, org, anchors) {
    console.log(org);
    instance.connect({
        source: id,
        target: org,
        anchors: [anchors, "BottomCenter"],
        connector: ["Flowchart", {
            stub: [40, 60],
            gap: 10,
            cornerRadius: 5,
            alwaysRespectStubs: false
        }],
        paintStyle: {
            lineWidth: 4,
            strokeStyle: "#61B7CF",
        },
        endpointStyle:{fillStyle:'transparent'}

    });

}

Create();

var _addEndpoints = function (toId, sourceAnchors, targetAnchors) {
    for (var i = 0; i < sourceAnchors.length; i++) {
        var sourceUUID = toId + sourceAnchors[i];

        instance.addEndpoint("flowchart" + toId, sourceEndpoint, {
            anchor: sourceAnchors[i],
            uuid: sourceUUID

        });
    }
    for (var j = 0; j < targetAnchors.length; j++) {
        var targetUUID = toId + targetAnchors[j];

        instance.addEndpoint("flowchart" + toId, targetEndpoint, {
            anchor: targetAnchors[j],
            uuid: targetUUID
        });
    }
};

var _addEndpointOrganizations = function (toId, targetAnchors) {
    for (var j = 0; j < targetAnchors.length; j++) {
        var targetUUID = toId + targetAnchors[j];

        instance.addEndpoint(toId, targetEndpoint, {
            anchor: targetAnchors[j],
            uuid: targetUUID
        });
    }

};


async function GetFormChangeName(id, guid, top, left) {

    var divCoverNewChangeNameForm = document.createElement('div');
    $(divCoverNewChangeNameForm).attr("id", "formchangenamepop");

    var newChangeNameForm = document.createElement('form');
    $(newChangeNameForm).attr("onsubmit", "return false");

    var divInNewChangeForm = document.createElement('div');
    divInNewChangeForm.style.padding = "10px 20px;";

    var leftPosition = left - $("#canvas").offset().left;
    var topPosition = top - $("#canvas").offset().top;
    var title = document.createElement('h5')
    title.append("Current name : " + $("#" + guid).attr('name'));

    var labelName = document.createElement('label')
    $(labelName).attr("id", "labelFake");
    $(labelName).attr("name", id);

    var nameLabel = labelName.appendChild(document.createElement('h6'));
    nameLabel.append("Name :  ");

    var inputName = document.createElement('input');
    $(inputName).attr("id", "nameChangeInput");
    $(inputName).attr("name", "nameChangeInput");
    $(inputName).attr("placeholder", "New name");

    var saveBtn = document.createElement('button');
    $(saveBtn).attr('id', 'btnChangeName');
    $(saveBtn).attr('type', 'button');
    $(saveBtn).css({
        "width": "80px"
    });
    saveBtn.append("OK");
    $(saveBtn).addClass("btn btn-success");

    divInNewChangeForm.append(title);
    divInNewChangeForm.append(labelName);
    divInNewChangeForm.append(inputName);
    divInNewChangeForm.append(document.createElement('br'));

    var tagCenter = document.createElement('center');
    tagCenter.appendChild(saveBtn);
    divInNewChangeForm.append(tagCenter);

    newChangeNameForm.append(divInNewChangeForm);
    divCoverNewChangeNameForm.append(newChangeNameForm);
    $(divCoverNewChangeNameForm).css({
        top: topPosition,
        left: leftPosition,
        position: 'absolute'
    });
    $('#parent').after(divCoverNewChangeNameForm);


    $("#btnChangeName").click(function (e) {
        e.preventDefault();
        $("#formchangenamepop").hide(1000);
        var newBlockChange = $("#" + id + ">div");
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
            },
            buttonsStyling: false
        })
        if ($("#nameChangeInput").val() != "") {
            swalWithBootstrapButtons.fire({
                title: 'Are you sure?',
                text: "Are you sure you want to change this name",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No, cancel!',
                reverseButtons: true
            }).then((result) => {

                if (result.isConfirmed) {

                    if ($("#nameChangeInput").val() != "") {

                        Swal.fire({

                            icon: 'success',
                            title: 'Your Name Have Changed',
                            showConfirmButton: false,
                            timer: 1500
                        })
                        ChangeName(guid, $("#nameChangeInput").val());
                        $("#formchangenamepop").remove();
                    } else {

                        e.preventDefault();
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Input Change Name!!!',
                        })
                    }

                } else if (
                    /* Read more about handling dismissals below */
                    result.dismiss === Swal.DismissReason.cancel
                ) {
                    e.preventDefault();
                    swalWithBootstrapButtons.fire(
                        'Cancelled',
                        'Your imaginary file is safe :)',
                        'error'
                    )
                }
            })
        } else {
            e.preventDefault();
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Input Change Name!!!',
            })
        }
    });
}



function CallService(url, type, jData, callBack) {
    $.ajax({
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        type: type,
        url: url,
        data: jData,
        success: function (results) {

            callBack(results);
        },
        error: function (error) {
            if (error.responseJSON == undefined) {
                console.log(error.statusText);
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops... ' + error.status + "(" + error.statusText + ")",
                    text: "Save Fail, Please check your data.",

                })
            }
        }
    });
}

async function ChangeName(guid, name) {
    $("#" + guid).attr("name", name);
    $("#" + guid).hide().html(name).fadeIn('fast');
}

async function GetFormPop(ui, guid) {
    var divCoverNewForm = document.createElement('div');
    $(divCoverNewForm).attr("id", "formpop");
    var newForm = document.createElement('form');
    $(newForm).attr("id", "Nameform");
    $(newForm).attr("onsubmit", "return false");
    var divInNewForm = document.createElement('div');
    leftPosition = ui.offset.left - $("#canvas").offset().left;
    topPosition = ui.offset.top - $("#canvas").offset().top;
    divInNewForm.style.padding = "10px 20px;";

    var labelUuid = document.createElement('label')
    $(labelUuid).css("padding-left", "30px");
    var uuidLabel = labelUuid.appendChild(document.createElement('h6'));
    uuidLabel.append('Id :');


    var inputUuid = document.createElement('input');
    $(inputUuid).attr("id", "UuidInput");
    $(inputUuid).attr("name", "UuidInput");
    $(inputUuid).addClass("nameInput");
    $(inputUuid).attr("placeholder", guid);
    $(inputUuid).attr("readonly", "");
    inputUuid.style.marginLeft = "10px";

    var labelName = document.createElement('label')
    var nameLabel = labelName.appendChild(document.createElement('h6'));
    nameLabel.append("Name : ");
    var inputName = document.createElement('input');
    $(inputName).attr("id", "nameInput");
    $(inputName).attr("name", "nameInput");
    $(inputName).attr("val", "");
    $(inputName).addClass("nameInput");
    $(inputName).attr("placeholder", "Type your name here");
    inputName.style.marginLeft = "10px";

    var labelPrefix = document.createElement('label')
    $(labelPrefix).css("padding-left", "2px");
    var prefixLabel = labelPrefix.appendChild(document.createElement('h6'));
    prefixLabel.append("Prefix : ");
    var inputPrefix = document.createElement('input');
    $(inputPrefix).attr("id", "prefixInput");
    $(inputPrefix).attr("name", "prefixInput");
    $(inputPrefix).attr("onkeypress", "return /[a-z]/i.test(event.key)");
    $(inputPrefix).attr('type', 'text');
    $(inputPrefix).css("text-transform", "uppercase");
    $(inputPrefix).attr('maxlength', '3');
    $(inputPrefix).addClass("nameInput");
    $(inputPrefix).attr("placeholder", "Type your prefix here");
    inputPrefix.style.marginLeft = "10px";

    var saveBtn = document.createElement('button');
    $(saveBtn).attr('type', 'button');
    $(saveBtn).addClass("button");
    $(saveBtn).attr("id", "okButton");
    $(saveBtn).css({
        "width": "80px"
    });
    saveBtn.append("OK");
    $(saveBtn).addClass("btn btn-success");

    var closeBtn = document.createElement('button');
    $(closeBtn).attr('type', 'button');
    $(closeBtn).addClass("button");
    $(closeBtn).attr('id', 'closeButton');
    $(closeBtn).css({
        "width": "80px"
    });
    closeBtn.append("CLOSE");
    $(closeBtn).addClass("btn btn-danger");

    divInNewForm.append(labelUuid);
    divInNewForm.append(inputUuid);
    divInNewForm.append(document.createElement('br'));
    divInNewForm.append(labelName);
    divInNewForm.append(inputName);
    divInNewForm.append(document.createElement('br'));
    divInNewForm.append(labelPrefix);
    divInNewForm.append(inputPrefix);
    divInNewForm.append(document.createElement('br'));
    var tagCenter = document.createElement('center');
    tagCenter.appendChild(saveBtn);
    tagCenter.innerHTML += '&nbsp;';
    tagCenter.innerHTML += '&nbsp;';
    tagCenter.appendChild(closeBtn);
    divInNewForm.append(tagCenter);
    newForm.append(divInNewForm);
    divCoverNewForm.append(newForm);
    $(divCoverNewForm).css({
        top: topPosition - 10,
        left: leftPosition - 10,
        position: 'absolute'
    });
    $('#parent').after(divCoverNewForm);
    $("#nameInput").focus()


    $("#okButton").click(function (e) {
        document_prefix = $("#prefixInput").val().toUpperCase();
        if(document_prefix != "" && $("#nameInput").val() != ""){
            OnEnterCreateBlock(e, $("#nameInput").val(), leftPosition, topPosition, guid);
        }
    });
    $("#closeButton").click(function (e) {
        $("#drag1").show();
        $("#drag1").css({
            "left":"0px",
            "top":"10px",
            "right":"0px"
        });
        $("#formpop").remove();

    });
}

function OnEnterCreateBlock(event, name, leftPosition, topPosition, guid) {


    $("#formpop").hide(1000);
    if (name != "" && document_prefix != "") {
        GetControlStationConfigRelation(name, leftPosition, topPosition, guid);
    } else {
        event.preventDefault();
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Input Name Of the Block and Premix',
        })
    }
}

function GetControlStationConfigRelation(name, left, top, guid) {

    var coverNewBox = document.createElement('div');
    $(coverNewBox).attr("style", "width:105;height:105;background-color:gray")
    var newBox = document.createElement('div');
    var id = "flowchartWindow" + numFlowchart;

    $(newBox).attr("id", id);
    $(newBox).attr("name", guid);
    $(newBox).attr("oncontextmenu", "contextMenus(" + id + ")");
    $(newBox).addClass("window");
    $(newBox).addClass("jtk-node");
    $(newBox).addClass("some-class");


    $(newBox).css({
        top: top,
        left: left,
        position: 'absolute'
    });



    var img = document.createElement('img');
    img.src = "/jsPlumb/img/Block.png";
    img.width = "80";

    $(newBox).append(img);
    $(coverNewBox).append(newBox);
    $('#parent').after(coverNewBox);

    var divT = document.createElement('div');
    $(divT).attr("id", guid);
    $(divT).attr("name", name);

    divT.append(name);
    img.append(divT);
    $(newBox).append(divT);

    instance.batch(function () {
        _addEndpoints('Window' + numFlowchart + '', ["TopCenter", "LeftMiddle", "RightMiddle"], ["BottomCenter"]);
        numFlowchart++;
        instance.draggable(jsPlumb.getSelector(".flowchart-demo .window"), {
            grid: [20, 20]
        });




    });
    jsPlumb.fire("jsPlumbDemoLoaded", instance);
}


function CloseElement(e) {

    Remove(e, e.id);
}

function Remove(e, parentDiv) {

    Swal.fire({
        title: 'Are you sure?',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            var mychild = $("#" + parentDiv).children("div").attr("id");
            dict.splice(0, dict.length);
            instance.removeAllEndpoints(parentDiv);
            $("#drag1").show();
            $("#drag1").css({
                 "left":"0px",
                 "top":"10px",
                 "right":"0px"
            });

            $("#" + parentDiv).remove();
            Swal.fire(
                'Deleted!',
                'Your Block has been deleted.',
                'success'
            )
        }
    })
}

$("#canvas").droppable({

    accept: "#drag1",
    drop: function (event, ui) {
        var generateID = generateUUID();
        $("#drag1").hide();
        $("#formpop").remove();
        GetFormPop(ui, generateID);

    }

});

function Create() {

    checkConnection = true;
    instance.bind("connection", function (connInfo, originalEvent) {

        var source = $("#" + connInfo.sourceId).attr("name");
        var target = $("#" + connInfo.targetId).attr("name");

        same = false;
        if (dict.length <= 0) {
            dict.push({
                key: source,
                value: target
            });
            console.log(dict);
            return;
        }
        $.each(dict, function (index, val) {
            if (val.key == source) {
                if (val.value == target) {
                    console.log("same");
                    same = true;
                    checkConnection = false;
                    instance.detach(connInfo);
                    console.log(dict);
                    return;
                }
            }
        });
        if (!same) {
            dict.push({
                key: source,
                value: target
            });


            console.log(dict);
            checkConnection = true;

        }
        same = false;

    });


    instance.bind("connectionDetached", function (info, originalEvent) {


        if (checkConnection) {
            var mychild = $("#" + info.sourceId).children("div").attr("id");
            info.targetId;
            dict.forEach(function (item, index) {
                if (mychild == item.key && info.targetId == item.value) {

                    dict.splice(index, 1);
                }
            });

        }



    });


    instance.draggable(jsPlumb.getSelector(".flowchart-demo .window"), {
        grid: [20, 20]
    });

    instance.bind("click", function (conn, originalEvent) {



        var dataValue = document.getElementById(conn.sourceId).innerText;
        Swal.fire({
            title: 'Are you sure?',
            text: "Delete connection from " + dataValue + " to " + $("#"+conn.targetId).data('value') + " ?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {

                dict.forEach(function (item, index) {
                    if (conn.sourceId == item.key && conn.targetId == item.value) {

                        dict.splice(index, 1);
                    }
                });
                instance.detach(conn);

                Swal.fire(
                    'Deleted!',
                    'Your file has been deleted.',
                    'success'
                )
            }
        })
        // if (confirm("Delete connection from " + conn.sourceId + " to " + conn.targetId + "?"));
        // instance.detach(conn);

        // delete dict[conn.sourceId];


    });


    // jsPlumb.fire("jsPlumbDemoLoaded", instance);
}




$("#drag1").draggable({
    revert: "invalid",
    stack: ".draggable",
    //helper: 'clone',
    start: function() {
        $('#drag1').show();
    }

});

//Handler Click Event DIV

function ContextChangeName(divId, guid) {

    var idOfdiv = divId.id;

    var nameOfdiv = $('#' + guid).attr('name');
    var positionOfDiv = $("#" + idOfdiv).offset();
    Swal.fire({
        title: 'Are you sure?',
        text: "Change the Box " + nameOfdiv,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Change it!'
    }).then((result) => {
        if (result.isConfirmed) {
                $("#formchangenamepop").val("");
                GetFormChangeName(idOfdiv, guid, positionOfDiv.top, positionOfDiv.left);
        }
    })

}


$(document).keyup(function (event) {

    if (($("#nameInput").is(":focus") || $("#prefixInput").is(":focus")) && event.key == "Enter") {
        document_prefix = $("#prefixInput").val().toUpperCase();
        if (document_prefix != "" && $("#nameInput").val() != "") {
            var guid = $("#UuidInput").attr('placeholder');
            OnEnterCreateBlock(event, $("#nameInput").val(), leftPosition, topPosition, guid)
        }

    }
    if ($("#nameChangeInput").is(":focus") && event.key == "Enter") {
        var myDivElement = $("#labelFake").attr("name");

        var mychild = $("#" + myDivElement).children("div").attr("id");
        if ($("#nameChangeInput").val() == "" || $("#nameChangeInput").val() == null) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Ples Input New Name',
            })
        } else {
            $("#" + myDivElement).attr("name", $("#nameChangeInput").val());
            ChangeName(mychild, $("#nameChangeInput").val());

            $("#formchangenamepop").remove();
        }

    }
});



function HorizontallyBound(parentDiv, childDiv) {
    var parentRect = parentDiv.getBoundingClientRect();
    var childRect = childDiv.getBoundingClientRect();

    return parentRect.left >= childRect.right || parentRect.right <= childRect.left;
}


$('#divSaveButton').click(function () {
    Swal.fire({
        title: 'Are you sure?',
        //text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#28A745',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Save'
    }).then((result) => {
        if (result.isConfirmed) {

            document_id = dict[0].key;
            document_name = $('#' + document_id).attr('name');
            var toolbar = $('#' + document_id);
            position_top = $('#' + document_id).offsetParent().position().top;
            position_left = $('#' + document_id).offsetParent().position().left;
            mappings.splice(0, mappings.length);
            dict.forEach(function (item, index) {
                mappings.push({
                    document_id: dict[index].key,
                    msp_id: dict[index].value,
                    is_owner: 0
                });
            });

            var jData = {};
            jData.document_id = document_id;
            jData.document_name = document_name;
            jData.document_prefix = document_prefix;
            jData.position_top = position_top;
            jData.position_left = position_left;
            jData.mappings = mappings;
            console.log(JSON.stringify(jData));

            if (JSON.stringify(jData) != "") {
                CallService(url, 'POST', JSON.stringify(jData), function (results) {
                    if (results != "") {
                        Swal.fire({
                            icon: 'success',
                            title: 'Successfully saved',
                        }).then((result) => {
                            if (result.isConfirmed) {
                                window.location.href = "/Home/BlockConfig";
                            }

                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Save Fail, Please check your data.',
                        });
                        s
                    }
                });

            }
        }
    })
});

function contextMenus(divId) {

    document.addEventListener('contextmenu', event => event.preventDefault());
    var id = divId.id

    if ($(this).hasClass('active')) {
        return;
    }

    // Check for open menus, If any are open close them...
    if ($('.folderContextMenu').is(':visible')) {
        $('#' + id).removeClass('active');
        $('.folderContextMenu:visible').hide();
    }

    // Implement new menu
    $(this).addClass('active');

    $(".folderContextMenu:hidden").css({

        top: $('#' + id).offset().top + 50,
        left: $('#' + id).offset().left + 50,
        zIndex: 1001
    }).fadeIn('fast', function () {
        $(document).bind('click', function (e) {

            if (!$(e.target).hasClass('contextMenuRow') && !$(e.target).parent().hasClass('contextMenuRow')) {
                // Click is not on context menu, hide it...
                $('#' + id).removeClass('active');
                $('.folderContextMenu:visible').hide();
            };
        });
        $("#contextRename").click(function () {
            $('#parent').removeClass('active');
            $('.folderContextMenu:visible').hide();
            var guid = $('#' + id + "> div ").attr('id');

            ContextChangeName(divId, guid);

        });
        $("#contextRemove").click(function () {
            $('#parent').removeClass('active');
            $('.folderContextMenu:visible').hide();

            CloseElement(divId);

        });
        $("#contextConfig").click(function () {
            $('#parent').removeClass('active');
            $('.folderContextMenu:visible').hide();
            document.location.href = '/Home/BlockConfig';

        });


    });
}

function generateUUID() { // Public Domain/MIT
    var time = new Date().getTime(); //Timestamp
    var performances = (performance && performance.now && (performance.now() * 1000)) || 0; //Time in microseconds since page-load or 0 if unsupported
    return 'xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx'.replace(/[x]/g, function (c) {
        var random = Math.random() * 16; //random number between 0 and 16
        if (time > 0) { //Use timestamp until depleted
            random = (time + random) % 16 | 0;
            time = Math.floor(time / 16);
        } else { //Use microseconds since page-load if supported
            random = (performances + random) % 16 | 0;
            performances = Math.floor(performances / 16);
        }
        return (c === 'x' ? random : (random & 0x3 | 0x8)).toString(16);
    });
}