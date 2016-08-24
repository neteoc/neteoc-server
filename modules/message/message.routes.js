var express = require('express');
var passport = require('passport');
var Message = require('./message.controllers');
var router = express.Router();


module.exports = function() {

    router.route('/')
        .get(function(req, res, next){
            Message.list(req, res, next)
        })
        .post(function(req, res, next){
            Message.send(req, res, next)
        });


    return router;
};