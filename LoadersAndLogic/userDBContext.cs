namespace LoadersAndLogic
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using ErrorLoggerDataBase;

    public class userDBContext : DbContext
    {
        // Your context has been configured to use a 'dbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'LoadersAndLogic.dbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'dbContext' 
        // connection string in the application configuration file.
        public userDBContext()
            : base("name=userDBContext")
        {
            Database.SetInitializer(new dbInitializer()); //DropCreateDatabaseIfModelChanges<userDBContext>());
        }

        public DbSet<userDetails> users { get; set; }
        public DbSet<appInformation> applications { get; set; }
        public DbSet<Logger> logs { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}