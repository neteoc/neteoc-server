
process.env.GOOGLE_OAUTH_CLIENT_ID = "123456"
process.env.GOOGLE_OAUTH_CLIENT_SECRET = "123456"
process.env.GOOGLE_OAUTH_CALLBACK = "http://localhost:1212/callback"
process.env.PORT = "1212"
process.env.FLARE_DB = "dockerhost/flartdevtest"


var chai = require('chai');
var chaiHttp = require('chai-http');
var server = require('../app.js');

var should = chai.should();

chai.use(chaiHttp);


describe('APIs', function() {
    it('should respond with homepage html on / GET', function(done) {
        chai.request(server)
            .get('/')
            .end(function(err, res){
                res.should.have.status(200);
                done();
            });
    });
});
