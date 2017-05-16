using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ErrorLoggerDataBase
{
    public class userDetails
    {
        [Required]
        [EmailAddress]
        public String email { get; set; }
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userDetailsID { get; set; }
        [Required]
        public String firstName { get; set; }
        [Required]
        public String lastName { get; set; }
              
        public DateTime lastLoggedTime { get; set; }
        [Required]
        public Boolean role { get; set; }
        [Required]
        public String status { get; set; }
        public userDetails()
        {
            apps = new HashSet<appInformation>();
        }
        public virtual ICollection<appInformation> apps { get; set; }
    }
}
