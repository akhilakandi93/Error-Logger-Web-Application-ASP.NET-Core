using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ErrorLoggerDataBase;

namespace LoadersAndLogic
{
    public class dbInitializer : DropCreateDatabaseIfModelChanges<userDBContext>
    {

        protected override void Seed(userDBContext context)
        {

            userDetails user = new userDetails()
            {
                firstName = "Akhila",
                lastName = "Kandi",
                email = "akandi@syr.edu",
                lastLoggedTime = DateTime.Now,
                role = true, 
                userDetailsID = 1,
                status="Active",
                apps=new List<appInformation> { }

            };



            Logger logs = new Logger()
            {
                LoggerID = 1,
                AppID = new appInformation()
                {
                    appInformationID = 1,
                    AppName = "APP1",
                    AppDescription = "This is Application 1",
                    createdDate = System.DateTime.Now,
                    status = "Active",
                    users = new List<userDetails> { }

                },
                ErrorDescription = "",
                errorTime = DateTime.Now,
                type = Logger.errorType.DEBUG
                
            };
           // context.applications.Add(AppInfo);
            context.users.Add(user);
            context.logs.Add(logs);
            base.Seed(context);
            Console.WriteLine("Table created");
            
        }
    }
}
