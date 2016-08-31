'use strict';

var User = require('mongoose').model('User');
var List = require('mongoose').model('List');
var Message = require('./message.model.js');
var amqp = require('amqplib/callback_api');


var formatnumber2 = function(number){

    var PNF = require('google-libphonenumber').PhoneNumberFormat;
    var phoneUtil = require('google-libphonenumber').PhoneNumberUtil.getInstance();
    var phoneNumber = phoneUtil.parse(number, 'US');
    var formatedNumber = phoneUtil.format(phoneNumber, PNF.E164, function(error){
        console.log(error);
    });

    /**
    console.log(phoneUtil.format(phoneNumber, PNF.RFC3966));
    console.log(phoneUtil.format(phoneNumber, PNF.E164));
    console.log(phoneUtil.format(phoneNumber, PNF.INTERNATIONAL));
    console.log(phoneUtil.format(phoneNumber, PNF.NATIONAL));
    **/
    return formatedNumber;
};

var formatnumber = function(srcnumber){

    /**
    console.log("    ");
    console.log("==================================");
    console.log("Formating number:");
    console.log(srcnumber);
    console.log("----------------------------------");
    console.log("    ");
     **/

    var accountSid = process.env.TWILIO_SID;
    var authToken = process.env.TWILIO_TOKEN;

    const LookupsClient = require('twilio').LookupsClient;
    const client = new LookupsClient(accountSid, authToken);

    return client.phoneNumbers(srcnumber).get().then(function(fixnumber){
        //console.log(fixnumber.phoneNumber);
        return fixnumber;
    });

};


var sendemail = function(data){
    var api_key = process.env.MAILGUN_KEY;
    var domain = process.env.MAILGUN_DOMAIN;
    var mailgun = require('mailgun-js')({apiKey: api_key, domain: domain});

    /**
     var data = {
        from: 'Excited User <me@samples.mailgun.org>',
        to: 'serobnic@mail.ru',
        subject: 'Hello',
        text: 'Testing some Mailgun awesomness!'
    };
    **/
    /** Just testing... move along
    mailgun.messages().send(data, function (error, body) {
        console.log(body);
    });
     **/
    console.log("    ");
    console.log("==================================");
    console.log("Would have sent the following email:");
    console.log(data);
    console.log("----------------------------------");
    console.log("    ");
};


var sendsms = function(to, message){
    var accountSid = process.env.TWILIO_SID;
    var authToken = process.env.TWILIO_TOKEN;

    var twilio = require('twilio');
    var client = new twilio.RestClient(accountSid, authToken);

    var destNumber = "";

    var sms_data = {};


    var sendit = function(data){

        if (process.env.NODE_ENV == "production") {


             client.messages.create(sms_data, function(err, message) {
                console.log(message.sid);
             });
        } else {
            console.log("    ");
            console.log("==================================");
            console.log("would have sent the following SMS: ");
            console.log(data);
            console.log("----------------------------------");
            console.log("    ");
        }
    };


    formatnumber(to).then(function(number){
        //console.log("formant number call");
        //console.log(number.phoneNumber);
        var data = {
            body: message,
            to: number.phoneNumber,
            from: process.env.TWILIO_NUMBER // From a valid Twilio number
        };
        sendit(data);
    });



};

var sendTEL = function (to, messageid){
    var accountSid = process.env.TWILIO_SID;
    var authToken = process.env.TWILIO_TOKEN;

    var twilio = require('twilio');
    var client = new twilio.RestClient(accountSid, authToken);



    var sendit = function(call_data){

        if (process.env.NODE_ENV == "production") {

            client.makeCall(call_data, function(err, responseData) {

            });




        } else {


            console.log("    ");
            console.log("==================================");
            console.log("would have sent the following Tel Phone Call: ");
            console.log(call_data);
            console.log("----------------------------------");
            console.log("    ");

        }

    };

    formatnumber(to).then(function(number){

        var call_data = {

            to: number.phoneNumber,
            from: process.env.TWILIO_NUMBER,
            url: process.env.FLARE_URL + '/twil/msg/' +  messageid

        };
        sendit(call_data);

    });


};



exports.list = function (req, res, next) {

    Message
        .find({})
        .populate('_author')
        .exec(function (err, message) {
            if (err) return handleError(err);
            //console.log('The creator is %s', message._author.name);
            res.send(message);
            // prints "The creator is Aaron"
        });

};

exports.send = function (req, res, next) {

    var amqmessage = req.body.data;

    amqmessage.author = req.user._id;

    //var fluffy = new Kitten({ name: 'fluffy' });

    var newMessage = new Message({
        shortTitle: amqmessage.title,
        longTitle: amqmessage.title,
        content: amqmessage.content,
        author: amqmessage.author,
        list: amqmessage.list
    });

    List.findById(newMessage.list)
        .populate('members')
        .exec(function(err, list){
            list.members.forEach(function (member) {
                var data = {
                    //from: 'Robot Overlord <robot.overlord@neteoc.com>',
                    from: req.user.displayName + ' <' + req.user._id + '@neteoc.com>',
                    to: member.flareemail,
                    subject: newMessage.shortTitle,
                    text: newMessage.content
                };
                //sendemail(data);

                /**
                console.log("    ");
                console.log("    ");
                console.log("    ");
                console.log("==================================");
                console.log("Starting send process for: ");
                console.log(member);
                console.log("----------------------------------");
                console.log("    ");
                 **/

                if(member.flaresms) {
                    sendsms(member.flaresms, newMessage.shortTitle);
                } else {
                    /**
                    console.log("    ");
                    console.log("    ");
                    console.log("    ");
                    console.log("no flaresms");
                    console.log("==================================");
                     **/
                }


                if(member.flaretel) {
                    sendTEL(member.flaretel, newMessage.shortTitle);
                } else {
                    /**
                    console.log("    ");
                    console.log("    ");
                    console.log("    ");
                    console.log("no flaretel")
                    console.log("==================================");
                     **/
                }




            });

        });



    //newMessage.save();


/***
    amqp.connect('amqp://erbgldis:rMoZSWt5CYJQCO1XM4vaMHQXJr4_dvL-@reindeer.rmq.cloudamqp.com/erbgldis', function(err, conn) {
        conn.createChannel(function(err, ch) {
            var q = 'createFlare';
            var msg = req;

            ch.assertQueue(q, {durable: false});
            // Note: on Node 6 Buffer.from(msg) should be used
            ch.sendToQueue(q, new Buffer(msg));
            console.log(" [x] Sent %s", msg);
        });
        setTimeout(function() { conn.close(); process.exit(0) }, 500);
    });
 ***/

    console.log(newMessage);

    res.send(newMessage);


};

exports.findById = function (req, res, next) {
    Message.findById(req.params.msgId)
        .populate('author')
        .populate('list')
        .exec(function(err, msg){
            res.send(msg)
        });

};



