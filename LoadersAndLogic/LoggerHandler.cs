using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLoggerDataBase;

namespace LoadersAndLogic
{
   public class LoggerHandler
    {
        public static void addlog(Logger log)
        {
            userDBContext db = new userDBContext();
            db.logs.Add(log);
            db.SaveChanges();
        }
    }
}
