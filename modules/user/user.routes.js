var express = require('express');
var passport = require('passport');
var User = require('./user.controllers');
var router = express.Router();


module.exports = function() {

    router.route('/')
        .get(function(req, res, next){
            User.list(req, res, next)
        })
        .post(function(req, res, next){

        });

    router.route('/:userId')
        .get(function(req, res, next){
            User.findById(req, res, next);

        });


    return router;
};


