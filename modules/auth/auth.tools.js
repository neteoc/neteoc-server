'use strict';


var express = require('express');

// route middleware to make sure a user is logged in
exports.denyNotLoggedIn = function (req, res, next) {

    // if user is authenticated in the session, carry on
    if (req.isAuthenticated()  && req.user.status === "approved" || process.env.NODE_ENV == "development" )
        //console.log(req.user);
        //req.user.status;
        return next();

    // if they aren't redirect them to the home page
    res.status(401).send({ error: "401" });
};


exports.denyNotAdmin = function (req, res, next) {

    if (req.isAuthenticated()  && req.user.status === "approved" || process.env.NODE_ENV == "development" ) {
        if (req.user.isSiteAdmin) {
            return next();
        }
    }

    res.status(401).send({ error: "401" });

};

// route middleware to make sure a user is logged in
exports.isLoggedIn = function (req, res, next) {

    // if user is authenticated in the session, carry on
    if (req.isAuthenticated())
        return next();

    // if they aren't redirect them to the home page
    res.redirect('/auth/google');
};