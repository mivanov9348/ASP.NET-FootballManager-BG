var connection = new signalR.HubConnectionBuilder().withUrl("/clockHub").build();

connection.on("DisplayTime", function (hours, minutes) {

    var li = document.createElement("li");
    li.textContent = `${hours}:${minutes}`;
    document.getElementById("serverDate").innerHTML = "";
    document.getElementById("serverDate").appendChild(li);
});

connection.start().then(function () {

}).catch(function (err) {
    return console.error(err.toString());
});

setInterval(function () {

    connection.invoke("PrintTime").catch(function (err) {
        return console.error(err.toString());
    });

}, 60000);