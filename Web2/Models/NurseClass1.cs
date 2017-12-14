using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Web2.Models
{
    public enum NStatus
    {
        Nurse_Inbox, Send_To_Consultant
    }
    public class NurseClass1
    {
        [Key]
        public int npid { get; set; }

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

        [Required(ErrorMessage = "Medical History")]
        [Display(Name = "Medical History")]
        public string PastMedicalHistory { get; set; }

        [Display(Name = "Family History")]
        public string FamilyHistory { get; set; }

        [Display(Name = "Mental Status Examination")]
        public string MentalStatusExamination { get; set; }

        [Display(Name = "Collateral History")]
        public string CollateralHistory { get; set; }

        [Display(Name = "Nursing Care Plan")]
        public string NursingCarePlan { get; set; }

        [Display(Name = "Alergic Specific")]
        public string AlergicSpecificMedication { get; set; }

        [Display(Name = "Status")]
        public NStatus Status { get; set; }

        [Display(Name = "Consultant Name")]
        public string cname { get; set; }
    }
    public class NurseDB : DbContext
    {
        public NurseDB() : base("DefaultConnection")
        {

        }
        public DbSet<NurseClass1> NurseClass1s { get; set; }
    }
}