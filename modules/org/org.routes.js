var express = require('express');
var passport = require('passport');
var Org = require('./org.controllers');
var router = express.Router();


module.exports = function() {

    router.route('/')
        .get(function(req, res, next){
            Org.all(req, res, next);
        })
        .post(function(req, res, next){
            Org.create(req, res, next)
        });



    router.route('/:orgId/invite')
      .get(function(req, res, next){
        Org.orgInvite.getInvitesForOrg(req, res, next)
      })
      .post(function(req, res, next){
        Org.orgInvite.createInvite(req, res, next);
      });



    router.route('/:orgId/invite/:inviteId')
        .get(function(req, res, next){
          Org.orgInvite.findById(req, res, next);
        })
        .put(function(req, res, next){
          Org.orgInvite.updateById(req, res, next);
        })
        .delete(function(req, res, next){
          Org.orgInvite.deleteInvite(req, res, next);
        });

    router.route('/:orgId')
        .get(function(req, res, next){
            Org.findById(req, res, next);
        })
        .put(function (req, res, next) {
            Org.updateById(req, res, next);
        });

    return router;
};
