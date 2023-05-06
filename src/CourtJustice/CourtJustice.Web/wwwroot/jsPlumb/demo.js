
var data = 0;
var instance = window.jsp = jsPlumb.getInstance({
    // default drag options
    DragOptions: { cursor: 'pointer', zIndex: 2000 },
    // the overlays to decorate each connection with.  note that the label overlay uses a function to generate the label text; in this
    // case it returns the 'labelText' member that we set on each connection in the 'init' method below.
    ConnectionOverlays: [
        ["Arrow", {
            location: 1,
            visible: true,
            width: 11,
            length: 11,
            id: "ARROW",
            events: {
                click: function () { alert("you clicked on the arrow overlay") }
            }
        }],
        ["Label", {
            location: 0.1,
            id: "label",
            cssClass: "aLabel",
            events: {
                tap: function () { alert("hey"); }
            }
        }]
    ],
    Container: "canvas"
});




// this is the paint style for the connecting lines..
var connectorPaintStyle = {
    strokeWidth: 2,
    stroke: "#61B7CF",
    joinstyle: "round",
    outlineStroke: "white",
    outlineWidth: 2
},
    // .. and this is the hover style.
    connectorHoverStyle = {
        strokeWidth: 3,
        stroke: "#216477",
        outlineWidth: 5,
        outlineStroke: "white",

    },
    endpointHoverStyle = {
        fill: "#216477",
        stroke: "#216477"
    },
    // the definition of source endpoints (the small blue ones)
    sourceEndpoint = {
        endpoint: "Dot",
        paintStyle: {
            stroke: "#7AB02C",
            fill: "transparent",
            radius: 7,
            strokeWidth: 1
        },
        isSource: true,
        connector: ["Flowchart", { stub: [40, 60], gap: 10, cornerRadius: 5, alwaysRespectStubs: true }],
        connectorStyle: connectorPaintStyle,
        hoverPaintStyle: endpointHoverStyle,
        connectorHoverStyle: connectorHoverStyle,
        dragOptions: {},
        overlays: [
            ["Label", {
                location: [0.5, 1.5],
                label: "Drag",
                cssClass: "endpointSourceLabel",
                visible: true
            }]
        ]
    },
    // the definition of target endpoints (will appear when the user drags a connection)
    targetEndpoint = {
        endpoint: "Dot",
        paintStyle: { fill: "#7AB02C", radius: 7 },
        hoverPaintStyle: endpointHoverStyle,
        maxConnections: -1,
        dropOptions: { hoverClass: "hover", activeClass: "active" },
        isTarget: true,
        overlays: [
            ["Label", { location: [0.5, -0.5], label: "Drop", cssClass: "endpointTargetLabel", visible: true }]
        ]
    },
    init = function (connection) {
        connection.getOverlay("label").setLabel(connection.sourceId.substring(15) + "-" + connection.targetId.substring(15));
    };

var _addEndpoints = function (toId, sourceAnchors, targetAnchors) {
    for (var i = 0; i < sourceAnchors.length; i++) {
        var sourceUUID = toId + sourceAnchors[i];
        instance.addEndpoint("flowchart" + toId, sourceEndpoint, {
            anchor: sourceAnchors[i], uuid: sourceUUID
        });
    }
    for (var j = 0; j < targetAnchors.length; j++) {
        var targetUUID = toId + targetAnchors[j];
        instance.addEndpoint("flowchart" + toId, targetEndpoint, { anchor: targetAnchors[j], uuid: targetUUID });
    }
};


$("#canvas").droppable({
    accept: "#drag1",
    drop: function (event, ui) {



        var id = "flowchartWindow" + data;
        $('#parent').append('<div class="window jtk-node" id="' + id + '"><strong>' + data + '</strong><br /><br /></div>');


        instance.batch(function () {

            _addEndpoints('Window' + data + '', ["TopCenter", "BottomCenter"], ["LeftMiddle", "RightMiddle"]);

            instance.bind("connection", function (connInfo, originalEvent) {
                init(connInfo.connection);
            });

            instance.draggable(jsPlumb.getSelector(".flowchart-demo .window"), { grid: [20, 20] });

            instance.bind("click", function (conn, originalEvent) {
                if (confirm("Delete connection from " + conn.sourceId + " to " + conn.targetId + "?")) {
                    instance.deleteConnection(conn);
                }
            });

            instance.bind("connectionDrag", function (connection) {
                console.log("connection " + connection.id + " is being dragged. suspendedElement is ", connection.suspendedElement, " of type ", connection.suspendedElementType);
            });

            instance.bind("connectionDragStop", function (connection) {
                console.log("connection " + connection.id + " was dragged");
            });

            instance.bind("connectionMoved", function (params) {
                console.log("connection " + params.connection.id + " was moved");
            });
        });


        data++;
    }
});


$("#drag1").draggable({
    revert: "invalid",
    stack: ".draggable",
    helper: 'clone'
});


jsPlumb.fire("jsPlumbDemoLoaded", instance);




