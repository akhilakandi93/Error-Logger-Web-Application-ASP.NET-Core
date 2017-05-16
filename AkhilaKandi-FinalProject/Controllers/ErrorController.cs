using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkhilaKandi_FinalProject.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HttpError404(string message)
        {
            ViewBag.message = "Oops! An Error!!";
            return View();
        }

        public ActionResult HttpError500(string message)
        {
            ViewBag.message = "Oops! An Error!!";
            return View();
        }

        public ActionResult General(string message)
        {
            ViewBag.message = "Oops! An Error!!";
            return View();
        }
    }
}