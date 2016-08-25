var express = require('express');
var passport = require('passport');
var router = express.Router();
var Twil = require('./twilpub.controllers');



module.exports = function() {


    router.route('/msg/:msgId')
        .get(function(req, res, next){
            Twil.voicemessage(req, res, next);

        });



    return router;
};