using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AkhilaKandi_FinalProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            HttpException httpException = exception as HttpException;

            if (httpException != null)
            {
                string action;

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found
                        action = "HttpError404";
                        break;
                    case 500:
                        // server error
                        action = "HttpError500";
                        break;
                    default:
                        action = "General";
                        break;

                }

                String fileName = HttpContext.Current.Server.MapPath("~/logFiles/");
                if (!Directory.Exists(fileName))
                {
                    Directory.CreateDirectory(fileName);
                }
                fileName = fileName + "log.txt";
                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Dispose();
                }

                using (StreamWriter sw = File.AppendText(fileName))
                {
                    String error = DateTime.Now.ToString()+"\n"+exception.ToString()+"\n";
                    sw.WriteLine(error);
                    sw.Flush();
                    sw.Close();
                }

                // clear error on server
                Server.ClearError();

                Response.Redirect(String.Format("~/Error/{0}/?message={1}", action, ""));

            }
            else if (exception != null)
            {
                string action = "General";
                Response.Redirect(String.Format("~/Error/{0}/?message={1}", action, ""));
            }
        }
    }
}
