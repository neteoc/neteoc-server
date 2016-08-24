'use strict';

var User = require('mongoose').model('User');
var List = require('mongoose').model('List');
var Message = require('./message.model.js');
var amqp = require('amqplib/callback_api');




exports.list = function (req, res, next) {

    Message
        .find({})
        .populate('_author')
        .exec(function (err, message) {
            if (err) return handleError(err);
            //console.log('The creator is %s', message._author.name);
            res.send(message);
            // prints "The creator is Aaron"
        });

};

exports.send = function (req, res, next) {

    var amqmessage = req.body.data;

    amqmessage.author = req.user._id;

    //var fluffy = new Kitten({ name: 'fluffy' });

    var newMessage = new Message({
        shortTitle: amqmessage.title,
        longTitle: amqmessage.title,
        content: amqmessage.content,
        author: amqmessage.author,
        list: amqmessage.list
    });

    newMessage.save();


/***
    amqp.connect('amqp://erbgldis:rMoZSWt5CYJQCO1XM4vaMHQXJr4_dvL-@reindeer.rmq.cloudamqp.com/erbgldis', function(err, conn) {
        conn.createChannel(function(err, ch) {
            var q = 'createFlare';
            var msg = req;

            ch.assertQueue(q, {durable: false});
            // Note: on Node 6 Buffer.from(msg) should be used
            ch.sendToQueue(q, new Buffer(msg));
            console.log(" [x] Sent %s", msg);
        });
        setTimeout(function() { conn.close(); process.exit(0) }, 500);
    });
 ***/

    console.log(newMessage);

    res.send(newMessage);


};

exports.findById = function (req, res, next) {
    Message.findById(req.params.msgId)
        .populate('author')
        .populate('list')
        .exec(function(err, msg){
            res.send(msg)
        });

};



