using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Web2.Models
{
    public enum Gender
    {
        Male, Female
    }
    public enum Religion
    {
        Christian, Hindu, Muslim
    }
    public enum Marital
    {
        Single, Married, Divorce, Widow
    }
    public enum EthiniOrigin
    {
        European, Indian, Asian, African
    }
    public enum Status
    {
        Enquiry, Send_To_Nurse
    }
    public class SecClass1
    {
        [Key]
        public int pid { get; set; }
        

        [Display(Name = "Patient ID")]
       // [Index("PatientId", IsUnique = true)]
        [Required(ErrorMessage = "Enter Patient ID")]
        public int PatientId { get; set; }

        [Display(Name = "Patient Name")]
        [Required(ErrorMessage = "Enter Patient Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name Must be min 3 to 20 Characters")]
        public string pname { get; set; }

        [Display(Name = "SurName")]
        public string SurName { get; set; }

        [Display(Name = "Gender")]
        public Gender Gender { get; set; }

        [Display(Name = "Appointment Date")]
        public string  Appointment { get; set; }

        [Display(Name = "Appointment Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true )]
        public DateTime AppointmentTime { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Date of Birth")]
        public string DOB { get; set; }

        [Display(Name = "Religion")]
        public Religion Religion { get; set; }

        [Display(Name = "EthnicOrigin")]
        public EthiniOrigin EthnicOrigin { get; set; }

        [Display(Name = "Marital Status")]
        public Marital Maritalstatus { get; set; }

        [Display(Name = "Next of Kin")]
        public string NextOfKin { get; set; }

        [Display(Name = "Medical Card")]
        public string MedicalCard { get; set; }

        [Display(Name = "Occupation")]
        public string Occupation { get; set; }

        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "GP Name")]
        public string Gpname { get; set; }

        [Display(Name = "Status")]
        public Status Status { get; set; }

        public int cid { get; set; }
        public Consultant consultant { get; set; }
    }
    public class SecDB : DbContext
    {
        public SecDB() : base("DefaultConnection")
        {

        }
        public DbSet<SecClass1> SecClass1s { get; set; }

        public System.Data.Entity.DbSet<Web2.Models.Consultant> Consultants { get; set; }
    }
}