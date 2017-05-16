using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorLoggerDataBase
{
    public class Logger
    {
        [Key]
        public int LoggerID{get; set;}
        [Required]
        public int appInformationID { get; set; }
        public virtual appInformation AppID{get; set;}
        public String ErrorDescription{get; set;}
        public DateTime errorTime { get; set; }

        public errorType type { get; set; }
        
        public enum errorType
        {
            INFO, DEBUG, WARNING, FATAL, ERROR
        };



       
}
}
