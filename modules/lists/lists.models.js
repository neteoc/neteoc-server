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
    owner: { type: String, ref: 'User' },
    admins: [{ type: String, ref: 'User' }],
    members: [{ type: String, ref: 'User' }],
    org: {type: String, ref: 'Org'},
    additionalData: {}
    },
    {
        timestamps: true
    });


ListSchema.plugin(findOrCreate);

module.exports = mongoose.model('List', ListSchema);
