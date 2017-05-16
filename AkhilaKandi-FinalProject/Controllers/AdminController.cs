using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ErrorLoggerDataBase;
using LoadersAndLogic;
using System.Web.Helpers;

namespace AkhilaKandi_FinalProject.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private static int currentApp;
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult createNew()
        {
            return View();
        }
        [HttpPost]
        public ActionResult createNew(appInformation app)
        {
            app.createdDate = System.DateTime.Now;
            app.status = "Active";
            applicationHandler newApp = new applicationHandler();
            newApp.addApplication(app);
            return View("Index");
        }

        public ActionResult activeApps()
        {
            applicationHandler app = new applicationHandler();
            List<appInformation> activeApps = app.getAllActiveApplications();

            return View(activeApps);
        }

        public ActionResult disabledApps()
        {
            applicationHandler app = new applicationHandler();
            List<appInformation> disabledApps = app.getAllDisabledApplications();
            return View(disabledApps);
        }

        public ActionResult changeStatusOfAppToActive(int appID)
        {
            applicationHandler app = new applicationHandler();
            app.changeAppToActive(appID);
            return RedirectToAction("disabledApps");
        }

        public ActionResult changeStatusOfAppToInActive(int appID)
        {
            applicationHandler app = new applicationHandler();
            app.changeAppToInActive(appID);
            return RedirectToAction("activeApps");
        }
        public ActionResult activeUsers()
        {
            applicationHandler app = new applicationHandler();
            List<userDetails> activeUsers = app.getAllActiveUsers();
            return View(activeUsers);
        }

        public ActionResult changeStatusToInActive(int userId)
        {
            applicationHandler app = new applicationHandler();
            app.disableUserById(userId);
            return RedirectToAction("activeUsers");
        }

        public ActionResult changeStatusToActive(int userId)
        {
            applicationHandler app = new applicationHandler();
            app.enableUserById(userId);
            return RedirectToAction("inactiveUsers");
        }

        public ActionResult inactiveUsers()
        {
            applicationHandler app = new applicationHandler();
            List<userDetails> disabledUsers = app.getAllDisabledUsers();
            return View(disabledUsers);
        }


        public ActionResult showLogs(int appId, string sortOrder, string currentFilter, string searchString)
        {

            LogHandler myLogs = new LogHandler();
            userDBContext db = new userDBContext();
            userDataHandler ud = new userDataHandler();
            if (!db.applications.Any(x => x.appInformationID == appId))
            {
                return RedirectToAction("errorPage1", "Home");
            }
            var userID=ud.getUserByEmail(User.Identity.Name);
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

        public ActionResult assignToUser(int appID)
        {
            TempData["application"] = appID.ToString();
            applicationHandler app = new applicationHandler();
            userDBContext db = new userDBContext();
            if (!db.applications.Any(x => x.appInformationID == appID))
            {
                ViewBag.Message = "No Such Application exists";
            }
            List<userDetails> users = app.assignUser(appID);
            return View(users);
        }

        public ActionResult assignedUser(int userID)
        {
            applicationHandler app = new applicationHandler();
            int appID = Int32.Parse(TempData["application"] as string);
            app.AssignToUser(userID, appID);
            return RedirectToAction("assignToUser", new { appID });
        }

        public ActionResult unassignUsers(int appID)
        {
            TempData["application"] = appID.ToString();
            applicationHandler app = new applicationHandler();
            List<userDetails> users = app.unassignUsers(appID);
            return View(users);
        }

        public ActionResult unassignedUser(int userID)
        {
            applicationHandler app = new applicationHandler();
            int appID = Int32.Parse(TempData["application"] as string);
            app.UnAssignedUser(userID, appID);
            return RedirectToAction("unassignUsers", new { appID });
        }
        public ActionResult LogDetails(int logID, int appID)
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