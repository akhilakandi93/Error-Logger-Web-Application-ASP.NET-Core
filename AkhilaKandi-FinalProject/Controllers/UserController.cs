using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoadersAndLogic;
using System.Web.Helpers;

namespace AkhilaKandi_FinalProject.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private static int currentApp;
        
        // GET: User
        public ActionResult Index()
        {
            var email = User.Identity.Name;
            applicationHandler myApps = new applicationHandler();
            var app=myApps.getAppByUserID(email);
            return View(app);
        }

        public ActionResult ViewLogs(int appId,string sortOrder,string currentFilter,string searchString)
        {
            
            LogHandler myLogs = new LogHandler();
            userDBContext db = new userDBContext();
            if (!db.applications.Any(x => x.appInformationID == appId)){
                return RedirectToAction("errorPage", "Home");
            }
            userDataHandler ud = new userDataHandler();
            var userID = ud.getUserByEmail(User.Identity.Name);
            var logs = myLogs.getLogsByAppId(appId,userID);
            currentApp = appId;
            ViewBag.currentFilter = sortOrder;
            ViewBag.appID = appId;
            ViewBag.TypeSort = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.TimeSort = sortOrder == "time" ? "time_desc" : "time";
            ViewBag.currentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                logs = logs.Where(x => x.ErrorDescription.ToLower().Contains(searchString.ToLower())).ToList();
            }
            switch (sortOrder)
            {
                case "type_desc":
                    logs = logs.OrderByDescending(x => x.type).ToList();
                    break;
                case "time_desc":
                    logs = logs.OrderByDescending(x => x.errorTime).ToList();
                    break;
                case "time":
                    logs = logs.OrderBy(x => x.errorTime).ToList();
                    break;
                default:
                    logs = logs.OrderBy(x => x.type).ToList();
                    break;
            }


            return View(logs);
        }

        public ActionResult LogDetails(int logID,int appID)
        {
            LogHandler viewLog = new LogHandler();
            ViewBag.appID = appID;
            var log = viewLog.getLogDescription(appID);
            return View(log);
         }
        public ActionResult DispChart()
        {
            applicationHandler appHandler = new applicationHandler();
            int appid = currentApp;
            // List<Application> applist = appHandler.GetApplicationByUser(3);
            using (var db = new userDBContext())
            {
                var data = db.logs.Where(r => r.appInformationID == appid).GroupBy(x => x.type).Select(y => new { Name = y.Key, Data = y.Count() });
                Chart ch = new Chart(300, 300);
                string[] xval = new string[5];
                int[] yval = new int[5];
                int i = 0;
                foreach (var d in data)
                {
                    xval[i] = d.Name.ToString();
                    yval[i] = d.Data;
                    i++;
                }

                ch.AddSeries("Default", chartType: "Pie",
                    xValue: xval, yValues: yval).Write("png");

                ch.Save("~/Content/chart", "png");
                return File("~/Content/chart", "png");
            }
        }
    }
}