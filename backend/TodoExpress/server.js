console.log('May the Node be with you');

const express = require('express');
const http = require('http');
const https = require('https');
const fs = require('fs');
const httpPort = 3000;
const httpsPort = 3001;
const bodyParser= require('body-parser')
const mongo  = require('mongodb').MongoClient;
const uri = "mongodb+srv://IMBackend:vo5tq6ujAkNIJZ8R@justthebackend-m7gsi.gcp.mongodb.net/test?retryWrites=true&w=majority";
const app = express();
const client = new mongo(uri, {useUnifiedTopology: true});
var key = fs.readFileSync(__dirname + '/certs/selfsigned.key');
var cert = fs.readFileSync(__dirname + '/certs/selfsigned.crt');

app.use(bodyParser.urlencoded({extended: true}));

var httpServer = http.createServer(app);
var httpsServer = https.createServer(certs, app)
var db
client.connect(err => {
    if (err) return console.log(err)
    db = client.db("star-wars-quotes")
    httpServer.listen(httpPort, () => {
        console.log('http listening on 3000');
    });
    
    httpsServer.listen(httpsPort, () => {
        console.log('https listening on 3001');
    });
})
var certs = {
    key: key,
    cert: cert
}




app.get('/', ( req, res) => {
    res.sendFile(__dirname + '/index.html');
    var cursor = db.collection('quotes').find().sort('rank', -1).toArray( (err, results) => {
        console.log(results);
    });

});

app.post('/quotes', (req, res) => {
    db.collection('quotes').insertOne(req.body, (err, results) => {
        if (err) return console.log(err);

        console.log('saved to database')
        res.redirect('/')

    })
    
})


