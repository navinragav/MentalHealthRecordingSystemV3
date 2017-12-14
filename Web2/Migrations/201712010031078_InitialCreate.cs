namespace Web2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BillClass1",
                c => new
                    {
                        bid = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        pname = c.String(),
                        Appointment = c.String(),
                        AppointmentTime = c.String(),
                        DOB = c.String(),
                        Gender = c.String(),
                        Prescription = c.String(),
                        Amount = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        cname = c.String(),
                    })
                .PrimaryKey(t => t.bid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BillClass1");
        }
    }
}
