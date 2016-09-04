var moment = require('moment-timezone');


var time = moment("2016-09-04T03:30:21.977Z").tz("America/New_York").format("DD MMM YYYY - kkmm zz");

var time2 = moment("2016-09-04T03:20:01.977Z").tz("America/New_York").format("DD MMM YYYY - kkmm zz");

var time3 = moment("2016-09-04T03:10:01.977Z").tz("America/New_York").format("DD MMM YYYY - kkmm zz");

console.log(time);
console.log(time2);
console.log(time3);
