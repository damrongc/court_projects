"use strict";

$("body").addClass('hold-transition');
$("body").addClass('sidebar-mini');
var url = "https://org1platformbc-dev.cpf.co.th/api/platform/documents";
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
  ConnectionOverlays: [["Arrow", {
    location: 0.989,
    visible: true,
    width: 16,
    length: 16,
    id: "arrow" // events: {
    //     click: function () {
    //         alert("you clicked on the arrow overlay")
    //     }
    // }

  }], ["Label", {
    location: 0.1,
    id: "label",
    cssClass: "aLabel" // events: {
    //     tap: function () {
    //         alert("hey");
    //     }
    // }

  }]],
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
  overlays: [["Label", {
    location: [0.5, 1.5],
    //label: "Drag",
    cssClass: "endpointSourceLabel",
    visible: false
  }]]
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
  overlays: [["Label", {
    location: [0.5, -0.5],

    /*label: "Drop",*/
    cssClass: "endpointTargetLabel",
    visible: false
  }]]
},
    init = function init(connection) {
  connection.getOverlay("label").setLabel(connection.sourceId + " - " + connection.targetId);
};

create();

var _addEndpoints = function _addEndpoints(toId, sourceAnchors, targetAnchors) {
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

var _addEndpointOrganizations = function _addEndpointOrganizations(toId, targetAnchors) {
  for (var j = 0; j < targetAnchors.length; j++) {
    var targetUUID = toId + targetAnchors[j];
    instance.addEndpoint(toId, targetEndpoint, {
      anchor: targetAnchors[j],
      uuid: targetUUID
    });
  }
};

function GetFormChangeName(id, GUID, top, left) {
  var divCoverNewChangeNameForm, newChangeNameForm, divInNewChangeForm, leftPosition, topPosition, title, labelName, nameLabel, inputName, saveBtn, tagCenter;
  return regeneratorRuntime.async(function GetFormChangeName$(_context) {
    while (1) {
      switch (_context.prev = _context.next) {
        case 0:
          console.log(GUID);
          divCoverNewChangeNameForm = document.createElement('div');
          $(divCoverNewChangeNameForm).attr("id", "formchangenamepop");
          newChangeNameForm = document.createElement('form');
          $(newChangeNameForm).attr("onsubmit", "return false");
          divInNewChangeForm = document.createElement('div');
          divInNewChangeForm.style.padding = "10px 20px;";
          leftPosition = left - $("#canvas").offset().left;
          topPosition = top - $("#canvas").offset().top;
          title = document.createElement('h5');
          title.append("Current name : " + $("#" + GUID).attr('name'));
          labelName = document.createElement('label');
          $(labelName).attr("id", "labelFake");
          $(labelName).attr("name", id);
          nameLabel = labelName.appendChild(document.createElement('h4'));
          nameLabel.append("Name :  ");
          inputName = document.createElement('input');
          $(inputName).attr("id", "nameChangeInput");
          $(inputName).attr("name", "nameChangeInput");
          $(inputName).attr("placeholder", "New name");
          saveBtn = document.createElement('button');
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
          tagCenter = document.createElement('center');
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
            var swalWithBootstrapButtons = Swal.mixin({
              customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger'
              },
              buttonsStyling: false
            });

            if ($("#nameChangeInput").val() != "") {
              swalWithBootstrapButtons.fire({
                title: 'Are you sure?',
                text: "Are you sure you want to change this name",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No, cancel!',
                reverseButtons: true
              }).then(function (result) {
                if (result.isConfirmed) {
                  if ($("#nameChangeInput").val() != "") {
                    Swal.fire({
                      position: 'top-end',
                      icon: 'success',
                      title: 'Your Name Have Changed',
                      showConfirmButton: false,
                      timer: 1500
                    });
                    ChangeName(GUID, $("#nameChangeInput").val());
                    $("#formchangenamepop").remove();
                  } else {
                    e.preventDefault();
                    Swal.fire({
                      icon: 'error',
                      title: 'Oops...',
                      text: 'Input Change Name!!!'
                    });
                  }
                } else if (
                /* Read more about handling dismissals below */
                result.dismiss === Swal.DismissReason.cancel) {
                  e.preventDefault();
                  swalWithBootstrapButtons.fire('Cancelled', 'Your imaginary file is safe :)', 'error');
                }
              });
            } else {
              e.preventDefault();
              Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Input Change Name!!!'
              });
            }
          });

        case 38:
        case "end":
          return _context.stop();
      }
    }
  });
}

function callService(url, type, jData, callBack) {
  $.ajax({
    dataType: 'json',
    contentType: "application/json; charset=utf-8",
    type: type,
    url: url,
    data: jData,
    success: function success(results) {
      callBack(results);
    },
    error: function error(_error) {
      if (_error.responseJSON == undefined) {
        console.log(_error.statusText);
      } else {
        Swal.fire({
          icon: 'error',
          title: 'Oops... ' + _error.status + "(" + _error.statusText + ")",
          text: "Save Fail, Please check your data."
        });
      }
    }
  });
}

function ChangeName(GUID, name) {
  return regeneratorRuntime.async(function ChangeName$(_context2) {
    while (1) {
      switch (_context2.prev = _context2.next) {
        case 0:
          $("#" + GUID).attr("name", name);
          $("#" + GUID).hide().html(name).fadeIn('fast');

        case 2:
        case "end":
          return _context2.stop();
      }
    }
  });
}

function getFormPop(ui, GUID) {
  var divCoverNewForm, newForm, divInNewForm, labelUuid, UuidLabel, inputUuid, labelName, nameLabel, inputName, labelPrefix, prefixLabel, inputPrefix, saveBtn, closeBtn, tagCenter;
  return regeneratorRuntime.async(function getFormPop$(_context3) {
    while (1) {
      switch (_context3.prev = _context3.next) {
        case 0:
          divCoverNewForm = document.createElement('div');
          $(divCoverNewForm).attr("id", "formpop");
          newForm = document.createElement('form');
          $(newForm).attr("id", "Nameform");
          $(newForm).attr("onsubmit", "return false");
          divInNewForm = document.createElement('div');
          leftPosition = ui.offset.left - $("#canvas").offset().left;
          topPosition = ui.offset.top - $("#canvas").offset().top;
          divInNewForm.style.padding = "10px 20px;";
          labelUuid = document.createElement('label');
          $(labelUuid).css("padding-left", "30px");
          UuidLabel = labelUuid.appendChild(document.createElement('h6'));
          UuidLabel.append('Id :');
          inputUuid = document.createElement('input');
          $(inputUuid).attr("id", "UuidInput");
          $(inputUuid).attr("name", "UuidInput");
          $(inputUuid).addClass("nameInput");
          $(inputUuid).attr("placeholder", GUID);
          $(inputUuid).attr("readonly", "");
          labelName = document.createElement('label');
          nameLabel = labelName.appendChild(document.createElement('h6'));
          nameLabel.append("Name : ");
          inputName = document.createElement('input');
          $(inputName).attr("id", "nameInput");
          $(inputName).attr("name", "nameInput");
          $(inputName).attr("val", "");
          $(inputName).addClass("nameInput");
          $(inputName).attr("placeholder", "Type your name here");
          labelPrefix = document.createElement('label');
          $(labelPrefix).css("padding-left", "2px");
          prefixLabel = labelPrefix.appendChild(document.createElement('h6'));
          prefixLabel.append("Prefix : ");
          inputPrefix = document.createElement('input');
          $(inputPrefix).attr("id", "prefixInput");
          $(inputPrefix).attr("name", "prefixInput");
          $(inputPrefix).attr("onkeypress", "return /[a-z]/i.test(event.key)");
          $(inputPrefix).attr('type', 'text');
          $(inputPrefix).css("text-transform", "uppercase");
          $(inputPrefix).attr('maxlength', '3');
          $(inputPrefix).addClass("nameInput");
          $(inputPrefix).attr("placeholder", "Type your prefix here");
          saveBtn = document.createElement('button');
          $(saveBtn).attr('type', 'button');
          $(saveBtn).addClass("button");
          $(saveBtn).attr("id", "okButton");
          $(saveBtn).css({
            "width": "80px"
          });
          saveBtn.append("OK");
          $(saveBtn).addClass("btn btn-success");
          closeBtn = document.createElement('button');
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
          tagCenter = document.createElement('center');
          tagCenter.appendChild(saveBtn);
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
          $("#nameInput").focus();
          $("#okButton").click(function (e) {
            document_prefix = $("#prefixInput").val().toUpperCase();
            if (document_prefix != "" && $("#nameInput").val() != "") OnEnterCreateBlock(e, $("#nameInput").val(), leftPosition, topPosition, GUID);
          });
          $("#closeButton").click(function (e) {
            $("#formpop").remove();
          });

        case 75:
        case "end":
          return _context3.stop();
      }
    }
  });
}

function OnEnterCreateBlock(event, name, leftPosition, topPosition, GUID) {
  $("#formpop").hide(1000);

  if (name != "") {
    getControlStationConfigRelation(name, leftPosition, topPosition, GUID);
  } else {
    event.preventDefault();
    Swal.fire({
      icon: 'error',
      title: 'Oops...',
      text: 'Input Name Of the Block'
    });
  }
}

function getControlStationConfigRelation(name, left, top, GUID) {
  var coverNewBox = document.createElement('div');
  $(coverNewBox).attr("style", "width:105;height:105;background-color:gray");
  var newBox = document.createElement('div');
  var id = "flowchartWindow" + numFlowchart;
  $(newBox).attr("id", id);
  $(newBox).attr("name", GUID);
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
  $(divT).attr("id", GUID);
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

function getStationId() {
  stationId++;

  if (stationId < 10) {
    return "Station0" + stationId;
  } else {
    return "Station" + stationId;
  }
}

function CloseElement(e) {
  console.log(e.id); //console.log(GUID);

  remove(e, e.id);
}

function remove(e, parentDiv) {
  Swal.fire({
    title: 'Are you sure?',
    text: "",
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#d33',
    cancelButtonColor: '#3085d6',
    confirmButtonText: 'Yes, delete it!'
  }).then(function (result) {
    if (result.isConfirmed) {
      var mychild = $("#" + parentDiv).children("div").attr("id"); //console.log(GUID);
      // for (var i = 0; i < dict.length; i++) {
      //     //console.log("look this dict " + dict[i].key);
      //     if (mychild == dict[i].key) {
      //         delete dict[i];
      //         //break;
      //     }
      // }
      // $(e).remove();

      dict.splice(0, dict.length);
      instance.removeAllEndpoints(parentDiv);
      console.log("THIS IS dict =>" + dict);
      $("#" + parentDiv).remove();
      Swal.fire('Deleted!', 'Your Block has been deleted.', 'success');
    }
  });
}

$("#canvas").droppable({
  accept: "#drag1",
  drop: function drop(event, ui) {
    var generateID = generateUUID();
    console.log(generateID);
    $("#formpop").remove();
    getFormPop(ui, generateID);
  }
});

function create() {
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
          console.log(dict[index]);
          dict.splice(index, 1);
        }
      });
    }

    console.log(dict);
  });
  instance.draggable(jsPlumb.getSelector(".flowchart-demo .window"), {
    grid: [20, 20]
  });
  instance.bind("click", function (conn, originalEvent) {
    var GUIDchild = $("#" + conn.sourceId).attr("id");
    var t = document.getElementById(GUIDchild).innerText;
    Swal.fire({
      title: 'Are you sure?',
      text: "Delete connection from " + t + " to " + conn.targetId + "?",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Yes, delete it!'
    }).then(function (result) {
      if (result.isConfirmed) {
        dict.forEach(function (item, index) {
          if (GUIDchild == item.key && conn.targetId == item.value) {
            console.log(dict[index]);
            dict.splice(index, 1);
          }
        });
        instance.detach(conn);
        Swal.fire('Deleted!', 'Your file has been deleted.', 'success');
      }
    }); // if (confirm("Delete connection from " + conn.sourceId + " to " + conn.targetId + "?"));
    // instance.detach(conn);
    // delete dict[conn.sourceId];
  }); // jsPlumb.fire("jsPlumbDemoLoaded", instance);
}

$("#drag1").draggable({
  revert: "invalid",
  stack: ".draggable",
  helper: 'clone'
}); //Handler Click Event DIV

function contextChangeName(divId, GUID) {
  var idOfdiv = divId.id;
  console.log(idOfdiv);
  var nameOfdiv = $('#' + GUID).attr('name');
  var positionOfDiv = $("#" + idOfdiv).offset();
  Swal.fire({
    title: 'Are you sure?',
    text: "Change the Box " + nameOfdiv,
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Yes, Change it!'
  }).then(function (result) {
    if (result.isConfirmed) {
      var checkFormPopExist = document.getElementById("formchangenamepop");

      if (checkFormPopExist != null) {
        alert(show);
      } else {
        $("#formchangenamepop").val("");
        GetFormChangeName(idOfdiv, GUID, positionOfDiv.top, positionOfDiv.left);
      }
    }
  });
}

$(document).keyup(function (event) {
  if ($("#prefixInput").is(":focus") && event.key == "Enter") {
    if ($("#prefixInput").val() != "" && $("#nameInput").val() != "") {
      var GUID = $("#UuidInput").attr('placeholder');
      OnEnterCreateBlock(event, $("#nameInput").val(), leftPosition, topPosition, GUID);
    }
  }

  if ($("#nameChangeInput").is(":focus") && event.key == "Enter") {
    var myDivElement = $("#labelFake").attr("name");
    var mychild = $("#" + myDivElement).children("div").attr("id"); //var blockId = myDivElement.attr("name");
    //alert(myDivElement.attr("name"));
    //var newBlockChange = $("#" + id + ">div");

    if ($("#nameChangeInput").val() == "" || $("#nameChangeInput").val() == null) {
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Ples Input New Name'
      });
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
  }).then(function (result) {
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
        callService(url, 'POST', JSON.stringify(jData), function (results) {
          if (results != "") {
            console.log("OK");
            Swal.fire({
              icon: 'success',
              title: 'Successfully saved'
            });
          } else {
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Save Fail, Please check your data.'
            });
            s;
          }
        });
      }
    }
  });
});

function contextMenus(divId) {
  document.addEventListener('contextmenu', function (event) {
    return event.preventDefault();
  });
  var id = divId.id;

  if ($(this).hasClass('active')) {
    return;
  } // Check for open menus, If any are open close them...


  if ($('.folderContextMenu').is(':visible')) {
    $('#' + id).removeClass('active');
    $('.folderContextMenu:visible').hide();
  } // Implement new menu


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
      }

      ;
    });
    $("#contextRename").click(function () {
      $('#parent').removeClass('active');
      $('.folderContextMenu:visible').hide();
      var GUID = $('#' + id + "> div ").attr('id');
      contextChangeName(divId, GUID);
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

function generateUUID() {
  // Public Domain/MIT
  var d = new Date().getTime(); //Timestamp

  var d2 = performance && performance.now && performance.now() * 1000 || 0; //Time in microseconds since page-load or 0 if unsupported

  return 'xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx'.replace(/[x]/g, function (c) {
    var r = Math.random() * 16; //random number between 0 and 16

    if (d > 0) {
      //Use timestamp until depleted
      r = (d + r) % 16 | 0;
      d = Math.floor(d / 16);
    } else {
      //Use microseconds since page-load if supported
      r = (d2 + r) % 16 | 0;
      d2 = Math.floor(d2 / 16);
    }

    return (c === 'x' ? r : r & 0x3 | 0x8).toString(16);
  });
}