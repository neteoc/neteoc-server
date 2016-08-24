'use strict';

var User = require('mongoose').model('User');
var List = require('mongoose').model('List');


exports.findById = function(req, res, next) {
    User.findById(req.params.userId, function(err, user){
        console.log(user);
        res.send(user)
    });
};