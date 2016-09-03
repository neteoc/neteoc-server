var mongoose = require('mongoose'),
	Schema = mongoose.Schema;
var findOrCreate = require('mongoose-findorcreate');

var SiteSettingsSchema = new Schema({
	name: String,
  discription: String,
  domain: String,
  owner: { type: String, ref: 'User' },
	admins: [{ type: String, ref: 'User' }],
  enableMessaging: {
		type: Boolean,
		default: false
	},
	additionalData: {}
	},
	{
		timestamps: true
	});


  OrgSchema.plugin(findOrCreate);

exports.SiteSettings = mongoose.model('SiteSettings', SiteSettingsSchema);
