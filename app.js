

var express = require('express');
var path = require('path');
var favicon = require('serve-favicon');
var logger = require('morgan');
var cookieParser = require('cookie-parser');
var bodyParser = require('body-parser');
var passport = require('passport');
var session = require('express-session');
const mongoose = require('mongoose');
var User = require('./modules/user/user.model.js');


const MongoStore = require('connect-mongo')(session);




var app = express();




mongoose.connect('mongodb://' + process.env.FLARE_DB);


// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'ejs');

// uncomment after placing your favicon in /public
//app.use(favicon(path.join(__dirname, 'public', 'favicon.ico')));
app.use(logger('dev'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(cookieParser());

app.use(session({
    secret: '3890w3t8378y3t09w34tyq4b6u',
    store: new MongoStore({ mongooseConnection: mongoose.connection })
}));

app.use(passport.initialize());
app.use(passport.session());

//Module Routes

const AuthTools = require('./modules/auth/auth.tools');

app.use('/auth', require('./modules/auth/routes')(app));
app.use('/lists', require('./modules/lists/lists.routes')(app));
app.use('/message', require('./modules/message/message.routes')(app));
app.use('/user',  require('./modules/user/user.routes')(app));

require('./config/passport')();
require('./routes')(app, passport, express);



module.exports = app;
