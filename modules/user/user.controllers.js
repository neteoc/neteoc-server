'use strict';
var mongoose = require('mongoose');
var User = require('mongoose').model('User');
var List = require('mongoose').model('List');


exports.findById = function(req, res, next) {
    User.findById(req.params.userId, function(err, user){
        res.send(user)
    });
};

exports.find = function(req, res, next) {
    User.find({}, function(err, users){
        res.send(users)
    });
};



exports.updateById = function(req, res, next) {
    User.findById(req.params.userId, function(err, user){
        console.log(user);
        //res.send(list)
        user.flareemail = req.body.flareemail;
        user.flaresms = req.body.flaresms;
        user.flaretel = req.body.flaretel;
        user.status = req.body.status;
        user.save();
        res.send(user);
    });
};
