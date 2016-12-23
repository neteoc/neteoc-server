'use strict';

const config = require('./settings.json');

module.exports.hello = (event, context, callback) => {
  const response = {
    statusCode: 200,
    body: JSON.stringify({
      message: 'Go Serverless v1.0! Your function executed successfully!',
      input: event,
    }),
  };

  callback(null, response);

  // Use this code if you don't use the http event with the LAMBDA-PROXY integration
  // callback(null, { message: 'Go Serverless v1.0! Your function executed successfully!', event });
};

var jwt = require('jsonwebtoken');
const Users = require('./user');
let users = new Users();
module.exports.getUser = (event, context, callback) => {
    let token = event.headers.Authorization.split(' ')[1];

    var secret = new Buffer(config.auth0.secret, "base64");
    jwt.verify(token, secret, function(err, decodedjwt) {
        if (!err){
            //console.log(decodedjwt);
            users.get(token, function(data){
                const response = {
                    statusCode: 200,
                    headers: {
                        "Access-Control-Allow-Origin" : "*" // Required for CORS support to work
                    },
                    body: data,
                };

                callback(null, response);

            })
        }

        if (err){
            //console.error(err);
        }
    });
};

module.exports.saveUser = (event, context, callback) => {
    let bodyJSON = JSON.parse(event.body);
    console.log(bodyJSON.userdata);
    let user = bodyJSON.userdata;
    let token = event.headers.Authorization.split(' ')[1];

    var secret = new Buffer(config.auth0.secret, "base64");
    jwt.verify(token, secret, function(err, decodedjwt) {
        if (!err){
            //console.log(decodedjwt);
            users.save(user, function(data){
                const response = {
                    statusCode: 200,
                    headers: {
                        "Access-Control-Allow-Origin" : "*" // Required for CORS support to work
                    },
                    body: JSON.stringify({saved: data}),
                };

                callback(null, response);

            })
        }

        if (err){
            //console.error(err);
        }
    });
};