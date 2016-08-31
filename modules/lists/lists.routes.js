var express = require('express');
var passport = require('passport');
var List = require('./lists.controllers');
var router = express.Router();


module.exports = function() {

    router.route('/')
        .get(function(req, res, next){
            List.all(req, res, next)
        })
        .post(function(req, res, next){
            List.create(req, res, next)
        })
        .delete(function (req, res, next) {
            List.deletelist(req, res, next)
        });

    router.route('/:listId')
        .get(function(req, res, next){
            List.findById(req, res, next)
        })
        .put(function(req, res, next){
            List.updateById(req, res, next)
        })
        .delete(function (req, res, next) {
            List.deletelist(req, res, next)
        });



    router.route('/find')
        .post(function(req, res, next){
            List.findone(req, res, next)
        });

    router.route('/removeuser')
        .post(function(req, res, next){
            List.removeuser(req, res, next)
        });

    return router;
};