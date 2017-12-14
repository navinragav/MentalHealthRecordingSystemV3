using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web2.Models;
using System.Data.Entity.Migrations;

namespace Web2.Controllers
{
    public class SecClass1Controller : Controller
    {
        private SecDB db = new SecDB();

        private HospitalDB db1 = new HospitalDB();
        private NurseDB db2 = new NurseDB();

        // GET: SecClass1
        [HttpGet]
        public ActionResult Index()
        {
            List<SecClass1> list1 = new List<SecClass1>();
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                IQueryable<SecClass1> q = from s in db.SecClass1s where s.Status == 0 select s;
                foreach (var i in q)
                {
                    list1.Add(i);
                }
                return View(list1);
                //var secClass1s = db.SecClass1s.Include(s => s.consultant);
                //return View(secClass1s.ToList());                               
            }
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post_Index()
        {
            List<SecClass1> list1 = new List<SecClass1>();
            try
            {            
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];            
            string pid = Request["pid"];
            string pname = Request["pname"];
            if (pid != "")
            {
                int pid1 = Convert.ToInt32(Request["pid"].ToString());
                IQueryable<SecClass1> q = from s in db.SecClass1s where s.PatientId == pid1 select s;
                foreach (var i in q)
                {
                    list1.Add(i);
                }
                ViewBag.pid = pid;
            }
            else if(pname !="")
            {                
                IQueryable<SecClass1> q = from s in db.SecClass1s where s.pname == pname  select s;
                foreach (var i in q)
                {
                    list1.Add(i);                                        
                }
                ViewBag.pname = pname;
            }
            else if((pid=="")&&(pname==""))
            {
                IQueryable<SecClass1> q = from s in db.SecClass1s select s;
                foreach (var i in q)
                {
                    list1.Add(i);
                }
                ViewBag.pid = "";
                ViewBag.pname = "";
            }
            }
            catch (Exception e1) { ViewBag.err1 = "Incorrect Format"; ViewBag.err2 = e1.Message.ToString(); }
            return View(list1);
            
        }
        // GET: SecClass1/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SecClass1 secClass1 = db.SecClass1s.Find(id);
                if (secClass1 == null)
                {
                    return HttpNotFound();
                }            
            return View(secClass1);
            }
        }

        // GET: SecClass1/Create
        public ActionResult Create()
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                try
                {
                    int PatientId = db.SecClass1s.Max(u => u.PatientId);
                    //int PatientId = 100;
                    ViewBag.pid = PatientId + 1;

                    ViewBag.cid = new SelectList(db.Consultants, "cid", "cname");
                }
                catch (Exception e1)
                {
                    //int PatientId = db.SecClass1s.Max(u => u.PatientId);
                    int PatientId = 100;
                    ViewBag.pid = PatientId + 1;

                    ViewBag.cid = new SelectList(db.Consultants, "cid", "cname");
                    ViewBag.err2 = e1.Message.ToString();
                }
                return View();

            }
        }

        // POST: SecClass1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pid,PatientId,pname,SurName,Gender,Appointment,AppointmentTime,ContactNumber,DOB,Religion,EthnicOrigin,Maritalstatus,NextOfKin,MedicalCard,Occupation,Address,Gpname,Status,cid")] SecClass1 secClass1)
        {
            try
            { 
            int PatientId = db.SecClass1s.Max(u => u.PatientId);
            //int PatientId = 100;
            ViewBag.pid = PatientId + 1;

            if (ModelState.IsValid)
            {
                db.SecClass1s.Add(secClass1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cid = new SelectList(db.Consultants, "cid", "cname", secClass1.cid);
            }
            catch (Exception e1)
            {
                ViewBag.err1 = "User Already Exists"; ViewBag.err2 = e1.Message.ToString();
                //int PatientId = db.SecClass1s.Max(u => u.PatientId);
                int PatientId = 100;
                ViewBag.pid = PatientId + 1;

                if (ModelState.IsValid)
                {
                    db.SecClass1s.Add(secClass1);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.cid = new SelectList(db.Consultants, "cid", "cname", secClass1.cid);
            }
            return View(secClass1);
        }

        // GET: SecClass1/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SecClass1 secClass1 = db.SecClass1s.Find(id);
                if (secClass1 == null)
                {
                    return HttpNotFound();
                }
                ViewBag.cid = new SelectList(db.Consultants, "cid", "cname", secClass1.cid);
                return View(secClass1);
            }
        }

        // POST: SecClass1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pid,PatientId,pname,SurName,Gender,Appointment,AppointmentTime,ContactNumber,DOB,Religion,EthnicOrigin,Maritalstatus,NextOfKin,MedicalCard,Occupation,Address,Gpname,Status,cid")] SecClass1 secClass1)
        {
            try
            { 
            if (ModelState.IsValid)
            {
                object o17 = secClass1.Status; string oo17 = o17.ToString();
                if (oo17 == "Enquiry")
                {
                    db.Entry(secClass1).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    object o8 = secClass1.cid;
                    string oo8 = o8.ToString();
                    int ooo8 = Convert.ToInt32(oo8);
                    string sc = (from s in db1.Consultants where s.cid == ooo8 select s.cname).First().ToString();

                    NurseClass1 nc1 = new NurseClass1();
                    //nc1.npid = secClass1.pid;
                    nc1.PatientId = secClass1.PatientId;
                    nc1.pname = secClass1.pname;
                    nc1.Appointment = Convert.ToString(secClass1.Appointment);
                    nc1.AppointmentTime = Convert.ToString(secClass1.AppointmentTime.ToString("hh:mm"));
                    nc1.DOB = secClass1.DOB.ToString();
                    nc1.Gender = Convert.ToString(secClass1.Gender);
                    nc1.PastMedicalHistory = "Nil";
                    nc1.cname = sc;

                    db2.NurseClass1s.Add(nc1); //nurse save
                    db2.SaveChanges(); //nurse save

                    db.Entry(secClass1).State = EntityState.Modified;  //sec save
                    db.SaveChanges(); //sec save

                    return RedirectToAction("Index");
                }

            }
            ViewBag.cid = new SelectList(db.Consultants, "cid", "cname", secClass1.cid);
            }
            catch (Exception e1) { ViewBag.err1 = "Error"; ViewBag.err2 = e1.Message.ToString(); }
            return View(secClass1);
        }

        // GET: SecClass1/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                SecClass1 secClass1 = db.SecClass1s.Find(id);
                if (secClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(secClass1);
            }
        }

        // POST: SecClass1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SecClass1 secClass1 = db.SecClass1s.Find(id);
            db.SecClass1s.Remove(secClass1);
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
