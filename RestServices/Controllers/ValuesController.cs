using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ErrorLoggerDataBase;
using LoadersAndLogic;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace RestServices.Controllers
{
    
    public class ValuesController : ApiController
    {
        // GET api/values
       [HttpPost]
       public void PostLog(Logger log)
        {
            try
            {
                using(userDBContext db=new userDBContext())
                {
                    db.logs.Add(log);
                    db.SaveChanges();
                }
            }catch(DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
        }
    }
}
