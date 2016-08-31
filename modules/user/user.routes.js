var express = require('express');
var passport = require('passport');
var User = require('./user.controllers');
var router = express.Router();


module.exports = function() {

    router.route('/')
        .get(function(req, res, next){
            User.find(req, res, next);

        })
        .post(function(req, res, next){
            res.send("hello")
        });

    router.route('/:userId')
        .get(function(req, res, next){
            User.findById(req, res, next);

        })
        .put(function (req, res, next) {
           User.updateById(req, res, next);
        });

    return router;
};


