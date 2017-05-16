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
    public class userDetailsController : ApiController
    {
        private userDBContext db = new userDBContext();

        // GET: api/userDetails
        public IQueryable<userDetails> Getusers()
        {
            return db.users;
        }

        // GET: api/userDetails/5
        [ResponseType(typeof(userDetails))]
        public IHttpActionResult GetuserDetails(int id)
        {
            userDetails userDetails = db.users.Find(id);
            if (userDetails == null)
            {
                return NotFound();
            }

            return Ok(userDetails);
        }

        // PUT: api/userDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutuserDetails(int id, userDetails userDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userDetails.userDetailsID)
            {
                return BadRequest();
            }

            db.Entry(userDetails).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userDetailsExists(id))
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

        // POST: api/userDetails
        [ResponseType(typeof(userDetails))]
        public IHttpActionResult PostuserDetails(userDetails userDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.users.Add(userDetails);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userDetails.userDetailsID }, userDetails);
        }

        // DELETE: api/userDetails/5
        [ResponseType(typeof(userDetails))]
        public IHttpActionResult DeleteuserDetails(int id)
        {
            userDetails userDetails = db.users.Find(id);
            if (userDetails == null)
            {
                return NotFound();
            }

            db.users.Remove(userDetails);
            db.SaveChanges();

            return Ok(userDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userDetailsExists(int id)
        {
            return db.users.Count(e => e.userDetailsID == id) > 0;
        }
    }
}