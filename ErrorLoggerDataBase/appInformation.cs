using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorLoggerDataBase
{
    public class appInformation
    {
        
        public int appInformationID{get; set;} 
        [Required]
        public String AppName {get; set;}
     
        public String AppDescription{get; set;}
        public DateTime createdDate { get; set; }
        [Required]
        public String status{get; set;}

        public appInformation()
        {
            users = new HashSet<userDetails>();
        }

        public virtual ICollection<userDetails> users { get; set; }

}
}
