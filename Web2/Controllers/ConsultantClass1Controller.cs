using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web2.Models;

namespace Web2.Controllers
{
    public class ConsultantClass1Controller : Controller
    {
        private ConsDB db = new ConsDB();

        private billsDB db2 = new billsDB();

        private HospitalDB db3 = new HospitalDB();

        // GET: ConsultantClass1
        public ActionResult Index()
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            string user = ViewBag.user;
            string role = ViewBag.role;
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Secretary"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                string sc = (from s in db3.Consultants where s.cname == user select s.cname).First().ToString();
                List<ConsultantClass1> list1 = new List<ConsultantClass1>();

                IQueryable<ConsultantClass1> q = from s in db.ConsultantClass1s where s.Status == 0 && s.cname == sc select s;
                foreach (var i in q)
                {
                    list1.Add(i);
                }
                return View(list1);
                //return View(db.ConsultantClass1s.ToList());            
            }
        }

        [HttpPost]
        [ActionName("Index")]  //search
        public ActionResult Post_Index()
        {
            List<ConsultantClass1> list1 = new List<ConsultantClass1>();
            try
            {
                ViewBag.user = Session["uns"];
                ViewBag.role = Session["rol"];

                string pid = Request["pid"];
                string pname = Request["pname"];
                if (pid != "")
                {
                    int pid1 = Convert.ToInt32(Request["pid"].ToString());
                    IQueryable<ConsultantClass1> q = from s in db.ConsultantClass1s where s.PatientId == pid1 select s;
                    foreach (var i in q)
                    {
                        list1.Add(i);
                    }
                    ViewBag.pid = pid;
                }
                else if (pname != "")
                {
                    IQueryable<ConsultantClass1> q = from s in db.ConsultantClass1s where s.pname == pname select s;
                    foreach (var i in q)
                    {
                        list1.Add(i);
                    }
                    ViewBag.pname = pname;
                }
                else if ((pid == "") && (pname == ""))
                {
                    IQueryable<ConsultantClass1> q = from s in db.ConsultantClass1s select s;
                    foreach (var i in q)
                    {
                        list1.Add(i);
                    }
                    ViewBag.pid = pid;
                }
            }
            catch (Exception e1) { ViewBag.err1 = "Error"; ViewBag.err2 = e1.Message.ToString(); }
            return View(list1);
            
        }

        // GET: ConsultantClass1/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            string user = ViewBag.user;
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Nurse"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ConsultantClass1 consultantClass1 = db.ConsultantClass1s.Find(id);
                if (consultantClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(consultantClass1);
            }
        }

        // GET: ConsultantClass1/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "cpid,PatientId,pname,Appointment,AppointmentTime,DOB,Gender,Observation,ICD10,TreatmentPlan,Prescription,Amount,Status,cname")] ConsultantClass1 consultantClass1)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ConsultantClass1s.Add(consultantClass1);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(consultantClass1);
        //}

        // GET: ConsultantClass1/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            string user = ViewBag.user;
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Nurse"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ConsultantClass1 consultantClass1 = db.ConsultantClass1s.Find(id);
                if (consultantClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(consultantClass1);
            }
        }

        // POST: ConsultantClass1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cpid,PatientId,pname,Appointment,AppointmentTime,DOB,Gender,Observation,ICD10,TreatmentPlan,Prescription,Amount,Status,cname")] ConsultantClass1 consultantClass1)
        {
            try
            {
                object o17 = consultantClass1.Status; string oo17 = o17.ToString();
                if (ModelState.IsValid)
                {
                    if (oo17 == "Consultant_Inbox")
                    {
                        db.Entry(consultantClass1).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        BillClass1 bc1 = new BillClass1();
                        bc1.PatientId = consultantClass1.PatientId;
                        bc1.pname = consultantClass1.pname;
                        bc1.Appointment = consultantClass1.Appointment;
                        bc1.AppointmentTime = consultantClass1.AppointmentTime;
                        bc1.DOB = consultantClass1.DOB;
                        bc1.Gender = consultantClass1.Gender;
                        bc1.Prescription = consultantClass1.Prescription;
                        bc1.Amount = consultantClass1.Amount;
                        bc1.Total_Amount = consultantClass1.Amount;
                        bc1.Net_Amount = consultantClass1.Amount;
                        bc1.cname = consultantClass1.cname;

                        db2.BillClass1s.Add(bc1);
                        db2.SaveChanges();              //data send to bills


                        db.Entry(consultantClass1).State = EntityState.Modified;
                        db.SaveChanges();               // update to consultant

                        return RedirectToAction("Index");
                    }

                }
            }
            catch (Exception e1) { ViewBag.err2 = e1.Message.ToString(); }
            return View(consultantClass1);
            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        db.Entry(consultantClass1).State = EntityState.Modified;
            //        db.SaveChanges();
            //        return RedirectToAction("Index");
            //    }
            //}
            //catch (Exception e1) { ViewBag.err1 = "User Already Exists"; ViewBag.err2 = e1.Message.ToString(); }
            //return View(consultantClass1);
        }

        // GET: ConsultantClass1/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            string user = ViewBag.user;
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Nurse"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ConsultantClass1 consultantClass1 = db.ConsultantClass1s.Find(id);
                if (consultantClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(consultantClass1);
            }
        }

        // POST: ConsultantClass1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConsultantClass1 consultantClass1 = db.ConsultantClass1s.Find(id);
            db.ConsultantClass1s.Remove(consultantClass1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
