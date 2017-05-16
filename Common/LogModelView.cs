using ErrorLoggerDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class LogModelView
    {
        public int appInformationID { get; set; }
        public String ErrorDescription { get; set; }
        public DateTime errorTime { get; set; }

        public errorType type { get; set; }

        public enum errorType
        {
            INFO, DEBUG, WARNING, FATAL, ERROR
        };

    }
}
