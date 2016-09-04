var moment = require('moment-timezone');

module.exports = exports = function formatedtimeStamps (schema) {
  schema.post('init', function(doc) {
    if (!doc.additionalData){
      doc.additionalData = {};
    }
    doc.additionalData.createdAtString = moment(this.createdAt).tz("America/New_York").format("DD MMM YYYY - kkmm zz");
    doc.additionalData.updatedAtString = moment(this.updatedAt).tz("America/New_York").format("DD MMM YYYY - kkmm zz");
  });


  }


/*

2016-09-04T03:10:19.977Z

moment(this.createdAt).tz("America/New_York").format("DD MMM YYYY - kkMM zz");


moment("2016-09-04T03:10:19.977Z", "MM-DD-YYYYT");


*/
