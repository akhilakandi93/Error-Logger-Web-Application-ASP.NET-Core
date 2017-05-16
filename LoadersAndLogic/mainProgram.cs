using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorLoggerDataBase;

namespace LoadersAndLogic
{
    class mainProgram
    {
        public static void Main(String[] args)
        {
            userDetails user = new userDetails();
            user.email = "akandi@syr.edu";
            user.firstName = "akhila";
            user.lastName = "kandi";
            user.userDetailsID = 1;
            userDBContext context = new userDBContext();
            context.Database.Initialize(true);
            //userDataHandler.addUser(user);
            Console.WriteLine("User added");
        }
    }
}
