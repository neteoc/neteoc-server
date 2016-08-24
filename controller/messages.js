var Message = require('../modules/message/message.model.js');

var get = function(req, res, next, shortTitle){

Message
.findOne({ shortTitle: shortTitle })
.populate('_author')
.exec(function (err, message) {
  if (err) return handleError(err);
  //console.log('The creator is %s', message._author.name);
  res.send({ message: message });
  // prints "The creator is Aaron"
});



};

var create = function(req, res, next, shortTitle, longTitle, content) {

  var newmessage = new Message({ shortTitle: shortTitle, longTitle: longTitle, content: content });
  newmessage.save();
  res.send({message: "saved"});
};

var getall = function(req, res, next, shortTitle) {

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

module.exports = {
  get,
  create,
  getall
};
