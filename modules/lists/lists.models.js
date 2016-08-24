var mongoose = require('mongoose'),
    Schema = mongoose.Schema;

var findOrCreate = require('mongoose-findorcreate');


var ListSchema = new Schema({
    name: {
        type: String,
        trim: true,
        default: ''
    },
    discription: {
        type: String,
        trim: true,
        default: ''
    },
    admins: [{ type: String, ref: 'User' }],
    members: [{ type: String, ref: 'User' }],
    additionalData: {}

});

ListSchema.plugin(findOrCreate);

module.exports = mongoose.model('List', ListSchema);
