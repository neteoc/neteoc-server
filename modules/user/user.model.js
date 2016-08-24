var mongoose = require('mongoose'),
	Schema = mongoose.Schema;


var UserSchema = new Schema({
	firstName: {
		type: String,
		trim: true,
		default: ''
	},
	lastName: {
		type: String,
		trim: true,
		default: ''
	},
	displayName: {
		type: String,
		trim: true
	},
	avatarUrl: {
		type: String,
		trim: true
	},
	email: {
		type: String,
		trim: true,
		default: ''
	},
	provider: {
		type: String,
		required: 'Provider is required'
	},
	paymentinfo: {
		type: String
	},
	contactinfo: {},
	providerData: {},
	additionalProvidersData: {},
	messagesSent : [{ type: Schema.Types.ObjectId, ref: 'Message' }],
	messagesReceived : [{ type: Schema.Types.ObjectId, ref: 'Message' }]

});

module.exports = mongoose.model('User', UserSchema);
