var mongoose = require('mongoose'),
	Schema = mongoose.Schema;
var findOrCreate = require('mongoose-findorcreate');

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
  status: String
	},
	{
		timestamps: true
	});


  OrgSchema.plugin(findOrCreate);

exports.OrgInvite = mongoose.model('OrgInvite', OrgInviteSchema);
