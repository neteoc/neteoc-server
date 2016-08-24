var express = require('express');
var passport = require('passport');
var router = express.Router();
auth = require('./config');
User = require('mongoose').model('User');
var session = require('./session');

module.exports = function(app) {

router.route('/login')
.get(passport.authenticate('google', {
    successRedirect: '/users/',
    failure: '/error/'
}));

router.route('/google/callback')
  .get(passport.authenticate('google', {
    successRedirect: '/auth/google/redirect',
    failure: '/error/'
}));

    router.route('/google/redirect')
        .get(function(req, res, next){
            res.render('users', {user: req.user});
        });

router.route('/google')
  .get(passport.authenticate('google', {
    scope: [
      'https://www.googleapis.com/auth/userinfo.profile',
      'https://www.googleapis.com/auth/userinfo.email'
    ]
  }));
   //app.use('/api', denyNotLoggedIn, require('./api'));
  // Session Routes

    router.route('/session')
    .get(auth.ensureAuthenticated, session.session)
    .post(session.login)
    .delete(session.logout);

  router.route('/')
    .get(function(req, res, next){
      res.send("hello")
    });

    router.route('/account/load')
        .post(auth.ensureAuthenticated, function(req, res, next){
            // Set your secret key: remember to change this to your live secret key in production
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            var stripe = require("stripe")(process.env.STRIPEKEY);

            // Get the credit card details submitted by the form
            var token = req.body.stripeToken; // Using Express

            stripe.customers.create({
                source: token,
                description: 'payinguser@example.com'
            }).then(function(customer) {
                return stripe.charges.create({
                    amount: 1000, // Amount in cents
                    currency: "usd",
                    customer: customer.id
                });
            }).then(function(charge) {
                // YOUR CODE: Save the customer ID and other info in a database for later!
                //console.log(session.session);
                console.log(charge);
                res.redirect('/Flares/user/account');
            });



        });

    router.route('/account')
        .get(auth.ensureAuthenticated, function(req, res, next){


                res.send({accountinfo: 'jenkins'})
            }

        );

return router;
};
