$(".buttonZone").on("mousedown", ".game-button", function (event) {
    switch (event.which) {
        case 1:
            console.log('Left Mouse button pressed.');
            var buttonNumber = $(this).val();
            //console.log("Button # "+ buttonNumber);
            buttonClick(buttonNumber);
            break;
        case 2:
            console.log('Middle Mouse button pressed.');
            break;
        case 3:
            event.preventDefault();
            console.log('Right Mouse button pressed.');
            var buttonNumber = $(this).val();
            flagCell(buttonNumber);
            updateMessage();
            break;
        default:
            console.log('You have a strange Mouse!');
    }
});


$(".buttonZone").bind("contextmenu", function (e) {
    e.preventDefault();
});



function flagCell(buttonNumber) {
    $.ajax({
        datatype: "json",
        method: "POST",
        url: "/game/rightClickShowOneButton",
        data: {
            "buttonNumber": buttonNumber
        },
        success: function (data) {
            //console.log(data);
            $(".buttonZone").html(data);
        }

    });
}


function buttonClick(buttonNumber) {
    floodFill(buttonNumber);
    updateGrid();
    updateMessage();
}

function updateGrid() {
    $.ajax({
        datatype: "json",
        method: "POST",
        url: "/game/updateGrid",
        data: {
        },
        success: function (data) {
            //console.log(data);
            $(".buttonZone").html(data);
        }

    });
}


function updateMessage() {
    $.ajax({
        datatype: "json",
        method: "POST",
        url: "/game/showGameStatus",
        data: {
        },
        success: function (data) {
            //console.log(data);
            $(".gameMessage").html(data);
        }

    });
}

function floodFill(buttonNumber) {
    console.log("javascript buttonNumber: " + buttonNumber);
    $.ajax({
        datatype: "json",
        method: "POST",
        url: "/game/floodfill",
        data: {
            "buttonNumber": buttonNumber
        },
        success: function (data) {
            //console.log(data);
            //$("#" + buttonNumber).html(data);
        }

    });
}

function doButtonUpdate(buttonNumber) {

    $.ajax({
        datatype: "json",
        method: "POST",
        url: "/game/showOneButton",
        data: {
            "buttonNumber": buttonNumber
        },
        success: function (data) {
            //console.log(data);
            $("#" + buttonNumber).html(data);
        }

    });
};


