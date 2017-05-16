using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLoggerDataBase;

namespace LoadersAndLogic
{
    public class applicationHandler
    {
        public List<appInformation> getAppByUserID(string email)
        {
            using (userDBContext context=new userDBContext())
            {
                if(context.users.Any(x=>x.email == email))
                {
                    if (context.users.Where(x => x.email == email).Any(x => x.status == "Active"))
                    {
                        return context.users.Single(x => x.email == email).apps.Where(x => x.status == "Active").ToList();
                    }
                }else
                {
                    return null;
                }
            }
            return null;
        }

        public void addApplication(appInformation app)
        {
            using (userDBContext db=new userDBContext())
            {
                db.applications.Add(app);
                db.SaveChanges();
            }
        }

        public List<appInformation> getAllActiveApplications()
        {
            using(userDBContext db=new userDBContext())
            {
                return db.applications.Where(x => x.status == "Active").ToList();
            }
           // return null;
        }


        public void enableUserById(int userID)
        {
                using(userDBContext db=new userDBContext())
                {
                    var user = db.users.First(r => r.userDetailsID == userID);
                    user.status = "Active";
                    db.SaveChanges();
                }
        }

        public void disableUserById(int userID)
        {
            using (userDBContext db = new userDBContext())
            {
                var user = db.users.First(r => r.userDetailsID == userID);
                user.status = "InActive";
                db.SaveChanges();
            }
        }

        public List<appInformation> getAllDisabledApplications()
        {
            using (userDBContext db = new userDBContext())
            {
                return db.applications.Where(x => x.status == "InActive").ToList();
            }
        }

        public void changeAppToActive(int appID)
        {
            using (userDBContext db=new userDBContext())
            {
                var app = db.applications.First(r => r.appInformationID == appID);
                app.status = "Active";
                db.SaveChanges();
            }
        }

        public void changeAppToInActive(int appID)
        {
            using (userDBContext db = new userDBContext())
            {
                var app = db.applications.First(r => r.appInformationID == appID);
                app.status = "InActive";
                db.SaveChanges();
            }
        }
        public List<userDetails> getAllActiveUsers()
        {
            using (userDBContext db=new userDBContext())
            {
                return db.users.Where(x => x.status == "Active").ToList();
            }
        }
        public List<userDetails> getAllDisabledUsers()
        {
            using (userDBContext db=new userDBContext())
            {
                return db.users.Where(x => x.status == "InActive").ToList();
            }
        }

        public List<Logger> showLogs(int appID)
        {
            using (userDBContext db=new userDBContext())
            {
                if (db.logs.Any(log => log.AppID.appInformationID == appID)){
                    List<Logger> logs = db.logs.Where(log => log.AppID.appInformationID == appID).ToList();
                    return logs;
                }
            }
            return null;
        }

        public List<userDetails> assignUser(int appID)
        {
            List<userDetails> u = new List<userDetails>();
            using (userDBContext db=new userDBContext())
            {
               
                if(db.applications.Any(app=>app.appInformationID == appID))
                {

                    List<userDetails> users = db.applications.FirstOrDefault(r => r.appInformationID == appID).users.Where(r=>r.status=="Active").ToList();
                 //   users = users.Where(x => x.apps != appID);
                    foreach(var user in db.users)
                    {
                        if(user.role==false && user.status=="Active")
                        if (!users.Exists(x => x.userDetailsID == user.userDetailsID))
                        {
                            u.Add(user);
                        }
                    }
                }
            }
            return u;
        }

        public List<userDetails> unassignUsers(int appID)
        {
            List<userDetails> u = new List<userDetails>();
            using (userDBContext db=new userDBContext())
            {
                if (db.applications.Any(app => app.appInformationID == appID))
                {
                    List<userDetails> users = db.applications.FirstOrDefault(r => r.appInformationID == appID).users.ToList();
                    foreach (var user in db.users)
                    {
                        if (users.Exists(x => x.userDetailsID == user.userDetailsID))
                        {
                            u.Add(user);
                        }
                    }
                }
            }
            return u;
        }

        public void AssignToUser(int userID, int appID)
        {
            using (userDBContext db=new userDBContext())
            {
                if (db.applications.Any(app => app.appInformationID == appID))
                {
                    if (db.users.Any(user => user.userDetailsID == userID))
                    {

                        var user = db.users.FirstOrDefault(u => u.userDetailsID == userID);
                        db.applications.FirstOrDefault(a => a.appInformationID == appID).users.Add(user);
                        db.SaveChanges();
                    }
                }
            }
            
        }

        public void UnAssignedUser(int userID, int appID)
        {
            using (userDBContext db=new userDBContext())
            {
                if (db.applications.Any(app => app.appInformationID == appID))
                {
                    if(db.users.Any(user=>user.userDetailsID == userID))
                    {
                        var user = db.users.FirstOrDefault(u => u.userDetailsID == userID);
                        db.applications.FirstOrDefault(a => a.appInformationID == appID).users.Remove(user);
                        db.SaveChanges();
                    }
                }
            }
        }


    }
}
