var mongoose = require('mongoose'),
	Schema = mongoose.Schema;


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
	author: { type: Number, ref: 'User' },
	list: { type: String, ref: 'List' },
	additionalData: {}

});

module.exports = mongoose.model('Message', MessageSchema);
