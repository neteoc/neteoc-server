using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetEOC.Shared.Aws.DynamoDb
{
    public class BaseDynamoModel
    {
        public Guid Id { get; set; }

        private DateTime _createDate;
        public DateTime CreateDate
        {
            get
            {
                if (_createDate == default(DateTime))
                {
                    _createDate = DateTime.Now;
                }
                return _createDate;
            }
            set
            {
                _createDate = value;
            }
        }

        public long CreateTimestamp { get { return CreateDate.Ticks; } }

        public string Type => GetType().Name;
    }
}
