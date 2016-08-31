var express = require('express');
var passport = require('passport');
var Admin = require('./admin.controllers');
var router = express.Router();


module.exports = function() {

    router.route('/')
        .get(function(req, res, next){
            res.send({api: 'admin'})

        })
        .post(function(req, res, next){
            res.send("hello")
        });

    router.route('/userapprove')
        .get(function(req, res, next){
            Admin.findUsersToApprove(req, res, next);

        })
        .put(function (req, res, next) {
            Admin.findUserById(req, res, next);
        });

    return router;
};


