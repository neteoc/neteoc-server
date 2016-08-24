'use strict';


// route middleware to make sure a user is logged in
exports.denyNotLoggedIn = function (req, res, next) {

    // if user is authenticated in the session, carry on
    if (req.isAuthenticated())
        return next();

    // if they aren't redirect them to the home page
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