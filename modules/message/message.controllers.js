'use strict';

var User = require('mongoose').model('User');
var List = require('mongoose').model('List');
var Message = require('./message.model.js');


exports.send = function (req, res, next) {

    List.findOne({name: req.body.data.list})
        .populate('members')
        .exec(function (err, list) {
            if (err) return handleError(err);
            //console.log('The creator is %s', message._author.name);
            var newmessage = new Message({ shortTitle: req.body.data.title, longTitle: req.body.data.title, content: req.body.data.content, list: list._id  });
            newmessage.save();
            res.send(list);
            // prints "The creator is Aaron"
        });



    //req.body.message.content

    //List

};

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