var alterna = 1;
setInterval(updateComponents, 2000);

function updateComponents() {        
    $.post('http://localhost:55000/api/Values', function (data) {
        console.log(data);

        var saturationValue = data.split("|")[0];
        var fanExhaustorState = data.split("|")[1];

        document.getElementById("pValorSaturacao").innerHTML = saturationValue;        

        var elem = document.createElement("img");
        //elem.setAttribute("src", "images/hydrangeas.jpg");
        elem.setAttribute("height", "173");
        elem.setAttribute("width", "173");
        //elem.setAttribute("alt", "Flower");

        if (fanExhaustorState === "ON") {
            document.getElementById("exhaustorImage").src = '../images/runningExhaustor.gif';
        }
        else if (fanExhaustorState === "OFF") {
            document.getElementById("exhaustorImage").src = '../images/stopedExhaustor.png';
        }
    });

}

