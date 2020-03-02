const express = require('express');

const fs = require('fs');
const httpPort = 3000;
const bodyParser= require('body-parser')
const mongo  = require('mongodb').MongoClient;
const uri = "mongodb+srv://IMBackend:vo5tq6ujAkNIJZ8R@justthebackend-m7gsi.gcp.mongodb.net/test?retryWrites=true&w=majority";
const app = express();
const client = new mongo(uri, {useUnifiedTopology: true});

app.use(bodyParser.urlencoded({extended: true}));

var db
client.connect(err => {
    if (err) return console.log(err)
    db = client.db("Asteroids")
    app.listen(httpPort, () => {
    console.log('http listening on 3000');
    });
    
   
})

app.get('/scores', ( req, res) => {
    allScores = db.collection('scores').find().toArray();
    res.send(allScores);
});

app.post('/scores', (req, res) => {
    db.collection('scores').insertOne(req.body, (err, results) => {
        if (err) return console.log(err);
    });
    
});