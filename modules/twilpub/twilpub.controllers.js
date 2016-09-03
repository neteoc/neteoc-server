var Message = require('mongoose').model('Message');
var twilio = require('twilio');


exports.voicemessage = function(req, res, next){

    var token = process.env.TWILIO_TOKEN;
    var header = req.headers['x-twilio-signature'];
    var url = process.env.FLARE_URL;

    if (twilio.validateRequest(token, header, url, req.body) || process.env.NODE_ENV == "development") {

          msgdetails = {};
          var twiml = new twilio.TwimlResponse();

          function say(text) {
              twiml.say(text, { voice: 'alice'});
          }

          function respond() {
              res.type('text/xml');
              res.send(twiml.toString());
          }

          Message.findById(req.params.msgId)
              //.populate('author')
              //.populate('list')
              .exec(function(err, msg){
                  //say('Terribly sorry, but an error has occurred. Goodbye.');
                  //console.log(msg);
                  //say("Flare subject ");
                  //twiml.pause({length: 1});
                  say(msg.shortTitle);
                  twiml.pause({length: 1});
                  //say("Fare content ")
                  //twiml.pause({length: 1});
                  say(msg.content);
                  twiml.hangup();
                  respond();
              });




      } else {
        res.type('text/plain');
        res.status(401).send("no soup for you");
      }




};
