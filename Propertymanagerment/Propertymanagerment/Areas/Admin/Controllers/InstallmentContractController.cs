using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Propertymanagerment.Models;

namespace Propertymanagerment.Areas.Admin.Controllers
{
    public class InstallmentContractController : Controller
    {
        private PPCDBEntities db = new PPCDBEntities();

        // GET: Admin/InstallmentContract
        public ActionResult Index()
        {
            var installment_Contract = db.Installment_Contract.Include(i => i.Property);
            return View(installment_Contract.ToList());
        }

        // GET: Admin/InstallmentContract/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Installment_Contract installment_Contract = db.Installment_Contract.Find(id);
            if (installment_Contract == null)
            {
                return HttpNotFound();
            }
            return View(installment_Contract);
        }

        // GET: Admin/InstallmentContract/Create
        public ActionResult Create()
        {
            ViewBag.Property_ID = new SelectList(db.Properties, "ID", "Property_Code");
            return View();
        }

        // POST: Admin/InstallmentContract/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Installment_Contract_Code,Customer_Name,Year_Of_Birth,SSN,Customer_Address,Mobile,Property_ID,Date_Of_Contract,Installment_Payment_Method,Payment_Period,Price,Deposit,Loan_Amount,Taken,Remain,Status")] Installment_Contract installment_Contract)
        {
            if (ModelState.IsValid)
            {
                db.Installment_Contract.Add(installment_Contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Property_ID = new SelectList(db.Properties, "ID", "Property_Code", installment_Contract.Property_ID);
            return View(installment_Contract);
        }

        // GET: Admin/InstallmentContract/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Installment_Contract installment_Contract = db.Installment_Contract.Find(id);
            if (installment_Contract == null)
            {
                return HttpNotFound();
            }
            ViewBag.Property_ID = new SelectList(db.Properties, "ID", "Property_Code", installment_Contract.Property_ID);
            return View(installment_Contract);
        }

        // POST: Admin/InstallmentContract/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Installment_Contract_Code,Customer_Name,Year_Of_Birth,SSN,Customer_Address,Mobile,Property_ID,Date_Of_Contract,Installment_Payment_Method,Payment_Period,Price,Deposit,Loan_Amount,Taken,Remain,Status")] Installment_Contract installment_Contract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(installment_Contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Property_ID = new SelectList(db.Properties, "ID", "Property_Code", installment_Contract.Property_ID);
            return View(installment_Contract);
        }

        // GET: Admin/InstallmentContract/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Installment_Contract installment_Contract = db.Installment_Contract.Find(id);
            if (installment_Contract == null)
            {
                return HttpNotFound();
            }
            return View(installment_Contract);
        }

        // POST: Admin/InstallmentContract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Installment_Contract installment_Contract = db.Installment_Contract.Find(id);
            db.Installment_Contract.Remove(installment_Contract);
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
