namespace EventManager
{
    using EventManager.Model;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DatabaseModel : DbContext
    {
        // Your context has been configured to use a 'DatabaseModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'EventManager.DatabaseModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DatabaseModel' 
        // connection string in the application configuration file.
        public DatabaseModel()
            : base("name=DatabaseModel")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserCredential> Userscredentials { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<UserEvent> Events { get; set; }

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