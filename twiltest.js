var accountSid = 'AC7fa6a65966982d4d186f42f7cff88832'; // Your Account SID from www.twilio.com/console
var authToken = 'e7c0e0c3a836036217edd731010708d0';   // Your Auth Token from www.twilio.com/console

var client = require('twilio')(accountSid, authToken);

//console.log(client);

client.sendMessage({
  body: 'Hello from Node',
  to: '+14789518703',  // Text this number
  from: '14782922263 ' // From a valid Twilio number
}, function(err, text) {
    console.log(err);
});
