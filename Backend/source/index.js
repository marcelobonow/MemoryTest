var app = require('express')();
var http = require('http').createServer(app); //.createServer();
var io = require('socket.io')(http);

io.attach(3030);

io.on('connection', function(socket){
    console.log('a user connected');
    socket.on('enterServer', function()
    {
        console.log("Open");
        socket.emit("welcome", {id: 30});
    })
  });
  
//   http.listen(8080, function(){
//     console.log('listening on :8080');
//   });