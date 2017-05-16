using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ErrorLoggerDataBase;
using LoadersAndLogic;

namespace AkhilaKandi_FinalProject.Controllers
{
    public class appInformationsController : Controller
    {
        private userDBContext db = new userDBContext();

        // GET: appInformations
        public ActionResult Index()
        {
            return View(db.applications.ToList());
        }

        // GET: appInformations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            appInformation appInformation = db.applications.Find(id);
            if (appInformation == null)
            {
                return HttpNotFound();
            }
            return View(appInformation);
        }

        // GET: appInformations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: appInformations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "appInformationID,AppName,AppDescription,createdDate,status")] appInformation appInformation)
        {
            if (ModelState.IsValid)
            {
                db.applications.Add(appInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appInformation);
        }

        // GET: appInformations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            appInformation appInformation = db.applications.Find(id);
            if (appInformation == null)
            {
                return HttpNotFound();
            }
            return View(appInformation);
        }

        // POST: appInformations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "appInformationID,AppName,AppDescription,createdDate,status")] appInformation appInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appInformation);
        }

        // GET: appInformations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            appInformation appInformation = db.applications.Find(id);
            if (appInformation == null)
            {
                return HttpNotFound();
            }
            return View(appInformation);
        }

        // POST: appInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            appInformation appInformation = db.applications.Find(id);
            db.applications.Remove(appInformation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
