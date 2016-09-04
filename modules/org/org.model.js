var mongoose = require('mongoose'),
	Schema = mongoose.Schema;
var findOrCreate = require('mongoose-findorcreate');
var formatedtimeStamps = require('../schemaPlugins/formatedtimeStamps');
var moment = require('moment-timezone');

var OrgSchema = new Schema({
	name: String,
  discription: String,
  owner: { type: String, ref: 'User' },
	admins: [{ type: String, ref: 'User' }],
	members: [{ type: String, ref: 'User' }],
	additionalData: {}
	},
	{
		timestamps: true
	});


  OrgSchema.plugin(findOrCreate);

exports.Org = mongoose.model('Org', OrgSchema);


var OrgInviteSchema = new Schema({
  email: String,
  owner: { type: String, ref: 'User' },
	org: { type: String, ref: 'Org' },
	acceptedBy: { type: String, ref: 'User' },
  status: String,
	isArchived: Boolean,
	additionalData: {}
	},
	{
		timestamps: true
	});

	OrgInviteSchema.post('init', function(doc) {
		if (moment(doc.createdAt).diff(moment(), 'days') <= -3 && doc.status == 'pending'){
			doc.status = 'expired';
    }
  });

  OrgInviteSchema.plugin(formatedtimeStamps);
  OrgInviteSchema.plugin(findOrCreate);

exports.OrgInvite = mongoose.model('OrgInvite', OrgInviteSchema);
