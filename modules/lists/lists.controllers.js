'use strict';

var List = require('./lists.models');
User = require('mongoose').model('User');




exports.all = function(req, res, next){

    List
        .find({})
        .exec(function (err, lists) {
            if (err) return handleError(err);
            //console.log('The creator is %s', message._author.name);
            res.send(lists);
            // prints "The creator is Aaron"
        });
};


exports.create = function(req, res, next, name, discription){

    List.findOrCreate({name: name}, {discription: discription}, function(err, list, created){
        res.send(list);
    });


};



exports.findone = function(req, res, next){

    List.findOne({name: req.body.list.name})
        .populate('members')
        .exec(function (err, lists) {
            if (err) return handleError(err);
            //console.log('The creator is %s', message._author.name);
            res.send(lists);
            // prints "The creator is Aaron"
        });
};


exports.adduser = function(req, res, next){

    List.findOne({name: req.body.list.name})
        .populate('members')
        .exec( function(err, list){
        User.findOne({displayName: req.body.user.name}, function (err, user) {
            list.members.push(user._id);
            console.log(req.user);
            list.save();
            res.send({list: list, user: user});
        });

    });
};

exports.removeuser = function(req, res, next){

    List.findOne({name: req.body.list.name})
        .populate('members')
        .exec( function(err, list){
            User.findOne({displayName: req.body.user.name}, function (err, user) {
                for(var i = list.members.length-1; i--;){
                    if (list.members[i] === user._id) ;list.members.splice(i, 1);
                }
                list.save();
                res.send({list: list});
            });

        });
};

exports.deletelist = function(req, res, next){

    List.findOneAndRemove({'name' : req.body.list.name}, function (err,offer){
            res.send({status: "removed"});
        });
};