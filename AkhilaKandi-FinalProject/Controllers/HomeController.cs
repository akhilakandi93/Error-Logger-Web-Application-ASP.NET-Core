using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ErrorLoggerDataBase;
using LoadersAndLogic;

namespace AkhilaKandi_FinalProject.Controllers
{
    public class HomeController : Controller
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index()
        {
            return RedirectToAction("homePage");
        }
        public ActionResult homePage()
        {
            return View();
        }
        public ActionResult errorPage()
        {
            return View();
        }
        public ActionResult errorPage1()
        {
            return View();
        }
        public ActionResult logOut()
        {
            return View();
        }
        public ActionResult startPage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult userHome(String user, String pass)
        {
            ICollection<appInformation> data = new List<appInformation>();
            if (ModelState.IsValid)
            {
                userDataHandler udh = new userDataHandler();
                try
                {
                    data = udh.getUser(user,pass);
                }catch(Exception ex)
                {
                    logger.Debug("*********");
                    logger.Error(ex.Message);
                    logger.Debug("*********");
                    Server.ClearError();
                    ViewBag.Message = "You have already Registered. Please go back and Login";
                    return View("startPage");
                }
            }
            ViewBag.uname = userDataHandler.getNameByEmail(user);
            return View(data);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(userDetails user)
        {
            user.role = false;
            user.lastLoggedTime = System.DateTime.Now;
            userDBContext db = new userDBContext();
         
            if (ModelState.IsValid)
            {
                userDataHandler udh = new userDataHandler();
               
                try
                {
                    udh.addUser(user);
                  return RedirectToAction("startPage");
                }
                catch (Exception ex)
                {
                    logger.Debug("*********");
                    logger.Error(ex.Message);
                    logger.Debug("*********");
                    Server.ClearError();
                    ViewBag.Message = "You have already Registered. Please go back and Login";
                    return View();
                }
            //    ModelState.Clear();
            }
            ModelState.Clear();
            return View("startPage");
        }

        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}