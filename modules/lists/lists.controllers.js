'use strict';

var List = require('./lists.models');
var User = require('mongoose').model('User');




exports.all = function(req, res, next){

    //req.user._id
    //PersonModel.find({ favouriteFoods: "sushi" }, ...);
    //console.log(req);
    //res.send(req.user);

    List
        .find({ admins: req.user._id})
        .exec(function (err, lists) {
            if (err) return handleError(err);
            //console.log('The creator is %s', message._author.name);
            res.send(lists);
            // prints "The creator is Aaron"
        });
};



exports.findById = function(req, res, next) {
    List.findById(req.params.listId)
        .populate('members')
        .exec(function(err, list){
        res.send(list)
    });
};


exports.findByIdPopulated = function(req, res, next) {
    List.findById(req.params.listId)
        .populate('members')
        .exec(function(err, list){
            res.send(list)
        });
};

exports.updateById = function(req, res, next) {
    List.findById(req.params.listId, function(err, list){
        console.log(list);
        //res.send(list)
        list.name = req.body.name;
        list.discription = req.body.discription;
        list.admins = req.body.admins;
        list.members = req.body.members;
        list.save();
        res.send(req.body);
    });
};



exports.create = function(req, res, next){
        List.findOrCreate({name: req.body.data.name}, {discription: req.body.data.discription, admins: [req.user._id]}, function(err, list, created){
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

    List.findOneAndRemove({'_id' : req.params.listId}, function (err, list){
            res.send({status: "removed"});
        });
};