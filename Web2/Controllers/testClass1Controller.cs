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
    public class testClass1Controller : Controller
    {
        private testDB db = new testDB();

        // GET: testClass1
        public ActionResult Index()
        {
            return View(db.testClass1s.ToList());
        }

        // GET: testClass1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            testClass1 testClass1 = db.testClass1s.Find(id);
            if (testClass1 == null)
            {
                return HttpNotFound();
            }
            return View(testClass1);
        }

        // GET: testClass1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: testClass1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pid,PatientId,pname,Appointment")] testClass1 testClass1)
        {
            if (ModelState.IsValid)
            {
                db.testClass1s.Add(testClass1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testClass1);
        }

        // GET: testClass1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            testClass1 testClass1 = db.testClass1s.Find(id);
            if (testClass1 == null)
            {
                return HttpNotFound();
            }
            return View(testClass1);
        }

        // POST: testClass1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pid,PatientId,pname,Appointment")] testClass1 testClass1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testClass1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testClass1);
        }

        // GET: testClass1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            testClass1 testClass1 = db.testClass1s.Find(id);
            if (testClass1 == null)
            {
                return HttpNotFound();
            }
            return View(testClass1);
        }

        // POST: testClass1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            testClass1 testClass1 = db.testClass1s.Find(id);
            db.testClass1s.Remove(testClass1);
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
