var connection = new signalR.HubConnectionBuilder().withUrl("/clockHub").build();

connection.on("DisplayTime", function (hours, minutes,seconds) {

    var p = document.createElement("p");
    p.textContent = `${hours}:${minutes}:${seconds}`;
    document.getElementById("serverDate").innerHTML = "";
    document.getElementById("serverDate").appendChild(p);
});

connection.start().then(function () {

}).catch(function (err) {
    return console.error(err.toString());
});

setInterval(function () {

    connection.invoke("PrintTime").catch(function (err) {
        return console.error(err.toString());
    });

}, 1000);