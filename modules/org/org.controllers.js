'use strict';
var mongoose = require('mongoose');
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
        org.admins = req.body.admins;
        org.save();
        res.send(org);
    });
};

exports.orgInvite = {};

exports.orgInvite.createInvite = function(req, res, next) {

  var emails = req.body.email.split(",");
  emails.forEach(function(inviteEmailRaw){
    let inviteEmail = inviteEmailRaw.trim()
    OrgInvite.create({
      email: inviteEmail,
      owner: req.user._id,
      org: req.params.orgId,
      status: 'pending'
    }, function(err, invite){
          var api_key = process.env.MAILGUN_KEY;
          var domain = process.env.MAILGUN_DOMAIN;
          var mailgun = require('mailgun-js')({apiKey: api_key, domain: domain});
           var data = {
              from: 'No Reply <noreply@neteoc.com>',
              to: invite.email,
              subject: req.user.displayName + ' has invited you to use NetEOC',
              text: 'Please go to https://neteoc.com/ui/org/' + invite.org + '/invite/' + invite._id + ' to acccpet or deny your invitation'
          };
          mailgun.messages().send(data, function (error, body) {
          });
        });
      });
      res.send({message: "sent"})
};

exports.orgInvite.deleteInvite = function(req, res, next) {
  OrgInvite.findById(req.params.inviteId, function(err, invite){
     if (invite.owner == req.user._id) {
       invite.isArchived = true;
       invite.save();
       res.send({message: "inviteArchived"})
     }

  });

};

exports.orgInvite.getInvitesForOrg = function(req, res, net) {
  OrgInvite.find({org: req.params.orgId})
  .where("isArchived").ne(true)
  .populate('acceptedBy')
  .exec(function(err, invite){
    res.send(invite);
  })
};

exports.orgInvite.findById = function(req, res, next) {
  OrgInvite.findById(req.params.inviteId)
  .exec(function(err, invite){
    //res.send({status: invite.status, createdAt: invite.createdAt});
    res.send(invite);
  });

};

exports.orgInvite.updateById = function(req, res, next) {
  OrgInvite.findById(req.params.inviteId)
  .where("isArchived").ne(true)
  .exec(function(err, invite){
    if (invite.status == 'pending'){
        invite.status = req.body.status;
        invite.acceptedBy = req.user._id;
        if (invite.status == 'accepted'){
          Org.findById(req.params.orgId, function(err, org){
              for(var i =  org.members.length - 1; i >= 0; i--) {
                      if( org.members[i] === req.user._id) {
                          org.members.splice(i, 1);
                      }
                  }
              org.members.push(req.user._id);
              org.save();
          });
          //console.log(mongoose.Types.ObjectId(req.user._id));
          //.find({_id: ObjectId('57cb9382df98e02d03c87f42')})
          User.findById(req.user._id)
          .exec(function(err, user){
              if (!user.flareemail) {
                user.flareemail = invite.email;
                user.save();
              }
          });

        };

        invite.save();
        res.send({message: 'saved'});
    }  else {
      res.send({message: 'not changed'});
    }
  })
};
