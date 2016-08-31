'use strict';

var User = require('mongoose').model('User');
var List = require('mongoose').model('List');
var Org = require('./org.model').Org;
var OrgInvite = require('./org.model').OrgInvite;


exports.findById = function(req, res, next) {
    Org.findById(req.params.orgId)
    .populate('members')
    .exec(function(err, org){
        res.send(org)
    });
};

exports.all = function(req, res, next) {
    Org.find({ $or:[{admins: req.user._id}, {owner: req.user._id} ]}, function(err, orgs){
        res.send(orgs)
    });
};

exports.create = function(req, res, next){
        Org.findOrCreate({
          name: req.body.name
        }, {
          discription: req.body.discription,
          admins: [req.user._id],
          members: [req.user._id],
          owner: req.user._id
        },
          function(err, org, created){
        res.send(org);
        });
};

exports.updateById = function(req, res, next) {
    Org.findById(req.params.orgId, function(err, org){
        console.log(req.body);
        //res.send(list)
        //org.name = req.body.name;
        //org.discription = req.body.discription;
        org.admins = req.body.admins;
        //org.members = req.body.members;
        org.save();
        res.send(org);
    });
};

exports.orgInvite = {};

exports.orgInvite.createInvite = function(req, res, next) {
  OrgInvite.create({
    email: req.body.email,
    owner: req.user._id,
    org: req.params.orgId,
    status: 'pending'
  }, function(err, invite){
    res.send(invite)
  });



};

exports.orgInvite.getInvitesForOrg = function(req, res, net) {
  OrgInvite.find({org: req.params.orgId})
  .exec(function(err, invite){
    res.send(invite);
  })
};

exports.orgInvite.findById = function(req, res, next) {
  res.send({message: "hello"})
};

exports.orgInvite.updateById = function(req, res, next) {
  OrgInvite.findById(req.params.inviteId, function(err, invite){
    console.log(invite);
    invite.status = req.body.status;
    if (invite.status == 'accepted'){
      Org.findById(req.params.orgId, function(err, org){
          console.log(org);

          for(var i =  org.members.length - 1; i >= 0; i--) {
                  if( org.members[i] === req.user._id) {
                      org.members.splice(i, 1);
                  }
              }


          org.members.push(req.user._id);
          //res.send(list)
          //org.name = req.body.name;
          //org.discription = req.body.discription;
          //org.admins = req.body.admins;
          //org.members = req.body.members;
          org.save();
          console.log(org);
      });

    };

    invite.save();
    res.send({message: 'saved'});
  })
};
