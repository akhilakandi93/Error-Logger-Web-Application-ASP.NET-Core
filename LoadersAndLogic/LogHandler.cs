using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLoggerDataBase;
namespace LoadersAndLogic
{
    public class LogHandler
    {
        public List<Logger> getLogsByAppId(int appID,int userID)
        {
            using (userDBContext context=new userDBContext())
            {
                var apps = context.applications.Where(x => (x.users.Any(u => u.userDetailsID == userID) && (x.status == "Active"))).ToList();
                if (apps.Any(x => x.appInformationID == appID))
                {
                    return context.logs.Where(x => x.appInformationID == appID).ToList();
                }
                return new List<Logger>();
              //  return context.logs.Include("AppID").Where(x => x.AppID.appInformationID == appID).ToList();
            }
        }

        public Logger getLogDescription(int logID)
        {
            using (userDBContext context=new userDBContext())
            {
                return context.logs.Single(x => x.LoggerID == logID);
            }
        }
    }
}
