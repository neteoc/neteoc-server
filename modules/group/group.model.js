var mongoose = require('mongoose'),
	Schema = mongoose.Schema;


var GroupSchema = new Schema({
	name: {
		type: String,
		trim: true,
		default: ''
	},
	admin: { type: Number, ref: 'User' },
	member: [{ type: Number, ref: 'User' }],
	additionalData: {}

});

module.exports = mongoose.model('Group', GroupSchema);
