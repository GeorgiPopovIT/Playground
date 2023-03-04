"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/userHub").build();

connection.on("updateTotalViews", function (value) {
    let newsCountSpan = document.getElementById('totalViews');
    newsCountSpan.innerText = value.toString();
});

connection.on("updateTotalUsers", function (value) {
    let newsCountSpan = document.getElementById('totalUsers');
    newsCountSpan.innerText = value.toString();
});

function newWindowLoadedOnClient() {
    connection.send('NewWindowLoaded');
}

connection.start().then(function () {
    console.log('Connection on hub on Success');
    newWindowLoadedOnClient();
}).catch(function (err) {
    return console.log(err);
});