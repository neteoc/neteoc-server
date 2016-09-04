var mongoose = require('mongoose'),
	Schema = mongoose.Schema;
var formatedtimeStamps = require('../schemaPlugins/formatedtimeStamps');

var MessageSchema = new Schema({
	shortTitle: {
		type: String,
		trim: true,
		default: ''
	},
	longTitle: {
		type: String,
		trim: true,
		default: ''
	},
	content: {
		type: String,
		trim: true
	},
	attachmentURL: {
		type: String,
		trim: true
	},
	author: { type: String, ref: 'User' },
	list: { type: String, ref: 'List' },
	recipients: [{ type: String, ref: 'User' }],
	additionalData: {}

    },
	{
		timestamps: true
	});


MessageSchema.plugin(formatedtimeStamps);

module.exports = mongoose.model('Message', MessageSchema);
