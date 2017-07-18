const config = require('./settings.json');

var jwt = require('jsonwebtoken');
var request = require('request');

class JWTauth {
    constructor() {
        //this.db = db;
        //this.mailer = mailer;
    }

    read(token, callback) {






        var secret = new Buffer(config.auth0.secret, "base64");
        jwt.verify(token, secret, function(err, decodedjwt) {


            if (err){
                console.error(err);
            }
        });
    }
}

module.exports = JWTauth;