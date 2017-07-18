var request = require('request');
var S3FS = require('s3fs');
const config = require('./settings.json');
var fsImpl = new S3FS(config.userData.bucket);

class Users {
    constructor() {
        //this.db = db;
        //this.mailer = mailer;

    }

    save(user, callback){
        fsImpl.writeFile(user.user_id, JSON.stringify(user)).then(function() {
            callback(true);
            console.log(user.name + '\'s user data saved!');
        }, function(reason) {
            throw reason;
        });
    }

    get(token, callback) {
        request.post('https://neteoc.auth0.com/tokeninfo', {form:{id_token:token}}, function(err,httpResponse,body){
                var info = JSON.parse(body);
                fsImpl.readFile(info.user_id, 'UTF-8', (err, fd) => {
                    if (err) {
                        if (err.code === "NoSuchKey") {
                            console.error('myfile does not exist');
                            let newUser = {
                                user_id: info.user_id,
                                name: info.name
                            };
                            fsImpl.writeFile(newUser.user_id, JSON.stringify(newUser)).then(function() {
                                console.log(newUser.name + '\'s user data saved!');
                            }, function(reason) {
                                throw reason;
                            });
                            callback(JSON.stringify(newUser));
                            return;
                        } else {
                            console.error(err.code);
                        }
                    } else {
                        console.log(JSON.parse(fd));
                        info.userdata = JSON.parse(fd);
                        callback(fd);
                    }

                });
            //console.log(body);
            // callback(body);


        });

    }
}

module.exports = Users;