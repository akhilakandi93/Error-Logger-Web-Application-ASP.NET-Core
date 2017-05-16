using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ErrorLoggerDataBase;
using LoadersAndLogic;

namespace RestServices.Controllers
{
    public class appInformationsController : ApiController
    {
        private userDBContext db = new userDBContext();

        // GET: api/appInformations
        public IQueryable<appInformation> Getapplications()
        {
            return db.applications;
        }

        // GET: api/appInformations/5
        [ResponseType(typeof(appInformation))]
        public IHttpActionResult GetappInformation(int id)
        {
            appInformation appInformation = db.applications.Find(id);
            if (appInformation == null)
            {
                return NotFound();
            }

            return Ok(appInformation);
        }

        // PUT: api/appInformations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutappInformation(int id, appInformation appInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appInformation.appInformationID)
            {
                return BadRequest();
            }

            db.Entry(appInformation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!appInformationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/appInformations
        [ResponseType(typeof(appInformation))]
        public IHttpActionResult PostappInformation(appInformation appInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.applications.Add(appInformation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appInformation.appInformationID }, appInformation);
        }

        // DELETE: api/appInformations/5
        [ResponseType(typeof(appInformation))]
        public IHttpActionResult DeleteappInformation(int id)
        {
            appInformation appInformation = db.applications.Find(id);
            if (appInformation == null)
            {
                return NotFound();
            }

            db.applications.Remove(appInformation);
            db.SaveChanges();

            return Ok(appInformation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool appInformationExists(int id)
        {
            return db.applications.Count(e => e.appInformationID == id) > 0;
        }
    }
}