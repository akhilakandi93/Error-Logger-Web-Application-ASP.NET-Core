using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DLLlib
{
    public class Logger
    {
        public static int applicationId = 13;
        public static int SERVICE_PORT = 38028;
        public static String SERVICE_URL = "http://localhost:{0}/";
        public static String PARAM_URL = "Api/Values/PostLog?log="; //REST Service URL

        //public void intiailize(int appInformationID)
        //{
        //    applicationId = appInformationID;
        //}


        public void Info(Exception ex)
        {
            LogModelView logModelView = new LogModelView();
            logModelView.appInformationID = applicationId;
            logModelView.type = LogModelView.errorType.INFO;
            logModelView.errorTime = DateTime.Now;
            logModelView.ErrorDescription = ex == null ? "Test Info" : ex.Message;

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(logModelView);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(String.Format(SERVICE_URL, SERVICE_PORT));
            var task = client.PostAsync(PARAM_URL, content).Result;
        }
        public void Debug(Exception ex)
        {
            LogModelView logModelView = new LogModelView();
            logModelView.appInformationID = applicationId;
            logModelView.type = LogModelView.errorType.DEBUG;
            logModelView.errorTime = DateTime.Now;
            logModelView.ErrorDescription = ex == null ? "Test Debug" : ex.Message;

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(logModelView);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(String.Format(SERVICE_URL, SERVICE_PORT));
            var task = client.PostAsync(PARAM_URL, content).Result;
        }

        public void Warning(Exception ex)
        {
            LogModelView logModelView = new LogModelView();
            logModelView.appInformationID = applicationId;
            logModelView.type = LogModelView.errorType.WARNING;
            logModelView.errorTime = DateTime.Now;
            logModelView.ErrorDescription = ex == null ? "Test Warning" : ex.Message;


            string json = Newtonsoft.Json.JsonConvert.SerializeObject(logModelView);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(String.Format(SERVICE_URL, SERVICE_PORT));
            var task = client.PostAsync(PARAM_URL, content).Result;
        }

        public void Error(Exception ex)
        {
           LogModelView logModelView = new LogModelView();
            logModelView.appInformationID = applicationId;
            logModelView.type = LogModelView.errorType.ERROR;
            logModelView.errorTime = DateTime.Now;
            logModelView.ErrorDescription = ex == null ? "Test Error" : ex.Message;

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(logModelView);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(String.Format(SERVICE_URL, SERVICE_PORT));
            var task = client.PostAsync(PARAM_URL, content).Result;
        }

        public void Fatal(Exception ex)
        {
            LogModelView logModelView = new LogModelView();
            logModelView.appInformationID = applicationId;
            logModelView.type = LogModelView.errorType.FATAL;
            logModelView.errorTime = DateTime.Now;
            logModelView.ErrorDescription = ex == null ? "Test Fatal" : ex.Message;

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(logModelView);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(String.Format(SERVICE_URL, SERVICE_PORT));
            var task = client.PostAsync(PARAM_URL, content).Result;
        }
    }
}
