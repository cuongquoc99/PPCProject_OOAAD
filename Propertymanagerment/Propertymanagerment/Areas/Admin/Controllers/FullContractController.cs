using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Propertymanagerment.Models;

namespace PropertyManagerment.Areas.Admin.Controllers
{
    public class FullContractController : Controller
    {
        private PPCDBEntities db = new PPCDBEntities();

        // GET: Admin/Full_Contract
        public ActionResult Index()
        {
            var full_Contract = db.Full_Contract.Include(f => f.Property);
            return View(full_Contract.ToList());
        }

        // GET: Admin/Full_Contract/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Full_Contract full_Contract = db.Full_Contract.Find(id);
            if (full_Contract == null)
            {
                return HttpNotFound();
            }
            return View(full_Contract);
        }

        // GET: Admin/Full_Contract/Create
        public ActionResult Create()
        {
            ViewBag.Property_ID = new SelectList(db.Properties, "ID", "Property_Code");
            return View();
        }

        // POST: Admin/Full_Contract/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Full_Contract_Code,Customer_Name,Year_Of_Birth,SSN,Customer_Address,Mobile,Property_ID,Date_Of_Contract,Price,Deposit,Remain,Status")] Full_Contract full_Contract)
        {
            if (ModelState.IsValid)
            {
                db.Full_Contract.Add(full_Contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Property_ID = new SelectList(db.Properties, "ID", "Property_Code", full_Contract.Property_ID);
            return View(full_Contract);
        }

        // GET: Admin/Full_Contract/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Full_Contract full_Contract = db.Full_Contract.Find(id);
            if (full_Contract == null)
            {
                return HttpNotFound();
            }
            ViewBag.Property_ID = new SelectList(db.Properties, "ID", "Property_Code", full_Contract.Property_ID);
            return View(full_Contract);
        }

        // POST: Admin/Full_Contract/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Full_Contract_Code,Customer_Name,Year_Of_Birth,SSN,Customer_Address,Mobile,Property_ID,Date_Of_Contract,Price,Deposit,Remain,Status")] Full_Contract full_Contract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(full_Contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Property_ID = new SelectList(db.Properties, "ID", "Property_Code", full_Contract.Property_ID);
            return View(full_Contract);
        }

        // GET: Admin/Full_Contract/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Full_Contract full_Contract = db.Full_Contract.Find(id);
            if (full_Contract == null)
            {
                return HttpNotFound();
            }
            return View(full_Contract);
        }

        // POST: Admin/Full_Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Full_Contract full_Contract = db.Full_Contract.Find(id);
            db.Full_Contract.Remove(full_Contract);
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
        public ActionResult Print(int id)
        {
            var contract = db.Full_Contract.FirstOrDefault(n => n.ID == id);
            if (contract != null)
            {
                FullContractPrintModel fc = new FullContractPrintModel();
                fc.Customer_Name = contract.Customer_Name;
                fc.Customer_Address = contract.Customer_Address;
                fc.Date_Of_Contract = contract.Date_Of_Contract;
                fc.Deposit = contract.Deposit;
                fc.Mobile = contract.Mobile;
                fc.Price = contract.Price;
                fc.Property_Code = contract.Property.Property_Code;
                fc.SSN = contract.SSN;
                fc.Address = contract.Property.Address; ;
                fc.Full_Contract_Code = contract.Full_Contract_Code;
                return View(fc);
            }
            else
            {
                return View();
            }
        }
    }
}
