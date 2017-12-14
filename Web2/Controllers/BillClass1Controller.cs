using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web2.Models;

using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;

namespace Web2.Controllers
{
    public class BillClass1Controller : Controller
    {
        private billsDB db = new billsDB();

        private FollowDB db2 = new FollowDB();

        // GET: BillClass1
        public ActionResult Index()
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            string user = ViewBag.user;
            string role = ViewBag.role;
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Admin") || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("LoginPage", "LogClass1");
            }
            else
            {
                List<BillClass1> list1 = new List<BillClass1>();

                IQueryable<BillClass1> q = from s in db.BillClass1s where s.Status == 0 select s;
                foreach (var i in q)
                {
                    list1.Add(i);
                }
                return View(list1);
                //return View(db.BillClass1s.ToList());
            }
        }


        [HttpPost]
        [ActionName("Index")]  //search
        public ActionResult Post_Index()
        {
            List<BillClass1> list1 = new List<BillClass1>();
            try
            { 
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            
            string pid = Request["pid"];
            string pname = Request["pname"];
            if (pid != "")
            {
                int pid1 = Convert.ToInt32(Request["pid"].ToString());
                IQueryable<BillClass1> q = from s in db.BillClass1s where s.PatientId == pid1 select s;
                foreach (var i in q)
                {
                    list1.Add(i);
                }
                ViewBag.pid = pid;
            }
            else if (pname != "")
            {
                IQueryable<BillClass1> q = from s in db.BillClass1s where s.pname == pname select s;
                foreach (var i in q)
                {
                    list1.Add(i);
                }
                ViewBag.pname = pname;
            }
            else if ((pid == "") && (pname == ""))
            {
                IQueryable<BillClass1> q = from s in db.BillClass1s select s;
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

        // GET: BillClass1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillClass1 billClass1 = db.BillClass1s.Find(id);
            if (billClass1 == null)
            {
                return HttpNotFound();
            }
            return View(billClass1);
        }

        //get print
        public ActionResult Print(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                return Redirect("~/WebForm1.aspx?BillNo=" + id);
            }
            return View();
        }


        DataSet1 ds = new DataSet1();
        public ActionResult BillReport()
        {
            //ds.Reset();
            //ReportViewer reportViewer = new ReportViewer();
            //reportViewer.ProcessingMode = ProcessingMode.Local;
            //reportViewer.SizeToReportContent = true;
            //string conString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            //SqlConnection cn = new SqlConnection();
            //cn = new SqlConnection(conString);
            //SqlDataAdapter adp = new SqlDataAdapter("SELECT PatientId,pname FROM BillClass1", cn);
            //adp.Fill(ds);
            
            //reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Report1.rdlc";
            //reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", ds.BillClass1.TableName));
            //ViewBag.ReportViewer = reportViewer;
            //ViewBag.path = reportViewer.LocalReport.ReportPath;

            return View();  
        }

    // GET: BillClass1/Create
    public ActionResult Create()
        {
            return View();
        }

        // POST: BillClass1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bid,PatientId,pname,Appointment,AppointmentTime,DOB,Gender,Prescription,Amount,Lab_Amount,Total_Amount,Vat,Net_Amount,Status,cname")] BillClass1 billClass1)
        {
            if (ModelState.IsValid)
            {
                db.BillClass1s.Add(billClass1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(billClass1);
        }

        // GET: BillClass1/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            string user = ViewBag.user;
            string role = ViewBag.role;
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
                BillClass1 billClass1 = db.BillClass1s.Find(id);
                if (billClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(billClass1);
            }
        }

        // POST: BillClass1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bid,PatientId,pname,Appointment,AppointmentTime,DOB,Gender,Prescription,Amount,Lab_Amount,Total_Amount,Vat,Net_Amount,Status,cname")] BillClass1 billClass1)
        {
            try
            {
                object o17 = billClass1.Status; string oo17 = o17.ToString();
                if (ModelState.IsValid)
                {
                    if (oo17 == "Bill_Inbox")
                    {
                        db.Entry(billClass1).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        FollowClass1 fc1 = new FollowClass1();
                        fc1.PatientId = billClass1.PatientId;
                        fc1.pname = billClass1.pname;
                        //fc1.Appointment = "";
                        //fc1.AppointmentTime = "";
                        fc1.DOB = billClass1.DOB;
                        fc1.Gender = billClass1.Gender;
                        fc1.cname = billClass1.cname;

                        db2.FollowClass1s.Add(fc1);
                        db2.SaveChanges();              //data send to followsup


                        db.Entry(billClass1).State = EntityState.Modified;
                        db.SaveChanges();               // update to bills

                        //return RedirectToAction("Index");
                        return Redirect("~/WebForm1.aspx?BillNo=" + billClass1.bid);
                    }

                }
            }
            catch (Exception e1) { ViewBag.err2 = e1.Message.ToString(); }
            return View(billClass1);

            
            
            
            //if (ModelState.IsValid)
            //{
            //    db.Entry(billClass1).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(billClass1);
        }

        // GET: BillClass1/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            string user = ViewBag.user;
            string role = ViewBag.role;
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
                BillClass1 billClass1 = db.BillClass1s.Find(id);
                if (billClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(billClass1);
            }
        }

        // POST: BillClass1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BillClass1 billClass1 = db.BillClass1s.Find(id);
            db.BillClass1s.Remove(billClass1);
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
