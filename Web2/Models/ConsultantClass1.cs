using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Web2.Models
{
    public enum CStatus
    {
        Consultant_Inbox, Send_To_Bill
    }
    public class ConsultantClass1
    {
        [Key]
        public int cpid { get; set; }

        [Display(Name = "Patient ID")]
        public int PatientId { get; set; }

        [Display(Name = "Patient Name")]
        public string pname { get; set; }

        [Display(Name = "Appointment Date")]
        public string Appointment { get; set; }

        [Display(Name = "Appointment Time")]
        public string AppointmentTime { get; set; }

        [Display(Name = "Date of Birth")]
        public string DOB { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Observation")]
        public string Observation { get; set; }

        [Display(Name = "ICD10")]
        public string ICD10 { get; set; }

        [Display(Name = "Treatment Plan")]
        public string TreatmentPlan { get; set; }

        [Display(Name = "Prescription")]
        [DataType(DataType.MultilineText)]
        public string Prescription { get; set; }

        [Display(Name = "Advice Bill Amount")]
        public int Amount { get; set; }

        [Display(Name = "Status")]
        public CStatus Status { get; set; }

        [Display(Name = "Consultant Name")]
        public string cname { get; set; }
    }
    public class ConsDB : DbContext
    {
        public ConsDB() : base("DefaultConnection")
        {

        }
        public DbSet<ConsultantClass1> ConsultantClass1s { get; set; }
    }
}