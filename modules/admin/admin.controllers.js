'use strict';

var User = require('mongoose').model('User');


exports.findUserById = function(req, res, next) {
    User.findById(req.params.userId, function(err, user){
        res.send(user)
    });
};


exports.findUsersToApprove = function(req, res, next) {
    User.find({status: undefined}, function(err, users){
        res.send(users)
    });
};
