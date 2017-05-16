using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLoggerDataBase;
using System.Web.ModelBinding;

namespace LoadersAndLogic
{

    public class userDataHandler
    {
       public static string GetName(string mail)
        {
            using (userDBContext context = new userDBContext())
            {
                if (context.users.Any(x => x.email == mail))
                {
                    return context.users.FirstOrDefault(x => x.email == mail).firstName;
                }
            }
            return null;
        }

        public  int getUserByEmail(string emailID)
        {
            using (var db=new userDBContext())
            {
                return db.users.First(x => x.email == emailID).userDetailsID;
            }
        }
        public  void addUser(userDetails user)
        {
            userDBContext context = new userDBContext();
            
            if(!context.users.Any(x => x.email == user.email))
            {
                context.users.Add(user);

                context.SaveChanges();
            }
            return;
        }

        public  appInformation addApplication(appInformation newApp)
        {
            userDBContext context = new userDBContext();
            if (!context.applications.Any(x => x.appInformationID == newApp.appInformationID))
            {
                context.applications.Add(newApp);
            }
            return newApp;
        }

        public List<appInformation> getUser(String email, String password)
        {
            using(userDBContext db=new userDBContext())
            {
                if(db.users.Any(x=> x.email == email)){
                    //if (db.users.First(y => y.email == email).password == password)
                    //{
                    //    return db.users.FirstOrDefault(x => x.email == email).apps.ToList();
                    //}
                }
            }
            return null;
        }

        public static String getNameByEmail(String email)
        {
            using(userDBContext db=new userDBContext())
            {
                if(db.users.Any(x=>x.email == email))
                {
                    return db.users.First(x => x.email == email).firstName;
                }
            }
            return null;
        }

        public  bool checkIfActive(string email)
        {
            try
            {
                using (var db = new userDBContext())
                {
                    return db.users.First(x=>x.email==email).status=="Active";
                }
            }
            catch
            {
                return false;
            }
        }
        public List<userDetails> getUsersById(int appID)
        {
            using (userDBContext context = new userDBContext())
            {
                if (context.applications.Any(x => x.appInformationID == appID))
                {
                    return context.applications.Single(x => x.appInformationID == appID).users.ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        

        //public static void deleteApplication(appInformation app)
        //{
        //    if(apps.Any(x => x.AppID == app.AppID){
        //        apps.Remove(app);
        //        if(logs.Any(x => x.AppID == app.AppID))
        //        {
        //            logs.Remove(a);
        //        }

        //    }
        //} 
    }
}
