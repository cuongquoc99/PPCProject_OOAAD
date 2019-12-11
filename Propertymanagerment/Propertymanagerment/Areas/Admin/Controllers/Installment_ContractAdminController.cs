using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Propertymanagerment.Models;

namespace Propertymanagerment.Areas.Admin.Controllers
{
    public class Installment_ContractAdminController : Controller
    {
        private PPCDBEntities db = new PPCDBEntities();

        // GET: Admin/Installment_ContractAdmin
        public ActionResult Index()
        {
            var installment_Contract = db.Installment_Contract.Include(i => i.Property);
            return View(installment_Contract.ToList());
        }

        // GET: Admin/Installment_ContractAdmin/Create
        public ActionResult Create(int id)
        {
            var property = db.Properties.FirstOrDefault(x => x.ID == id);
            dateYearOfBirth();

            if (property != null)
            {
                var ic = new Installment_Contract();
                ic.Price = property.Price;
                ViewData["PP_Code"] = property.Property_Code;
                return View(ic);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Customer_Name,Year_Of_Birth,SSN,Customer_Address," +
            "Mobile," +
            "Payment_Period,Price,Deposit,Loan_Amount,Taken,Remain")] Installment_Contract contract, string Property_Code)
        {

            var property = db.Properties.FirstOrDefault(m => m.Property_Code.Equals(Property_Code.ToString()));
            int Property_ID = property.ID;
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    contract.Property_ID = Property_ID;
                    contract.Installment_Payment_Method = "Tháng";
                    contract.Remain = contract.Price - contract.Deposit;
                    contract.Status = false;
                    contract.Date_Of_Contract = DateTime.Now;
                    db.Installment_Contract.Add(contract);
                    db.SaveChanges();
                    scope.Complete();
                }

                return RedirectToAction("Index");
            }
            dateYearOfBirth();

            return View("NewContract", contract);
        }

        // GET: Admin/Full_Contract/Delete/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index", "notfound");
            }
            Installment_Contract contract = db.Installment_Contract.FirstOrDefault(x => x.ID == id);

            if (contract == null)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(contract);
        }

        [HttpPost]
        public ActionResult Edit(Installment_Contract contract, string Mobile, string Customer_Name, decimal Taken)
        {
            if (ModelState.IsValid)
            {
                contract = db.Installment_Contract.FirstOrDefault(x => x.ID == contract.ID);
                contract.Customer_Name = Customer_Name;
                contract.Mobile = Mobile;
                contract.Taken = Taken;
                contract.Remain = contract.Price - contract.Taken - contract.Deposit;
                db.Entry(contract).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View("Edit", contract);
        }



        // GET: Admin/Installment_ContractAdmin/Delete/5
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

        // POST: Admin/Installment_ContractAdmin/Delete/5
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
        public ActionResult Print(int id)
        {
            var contract = db.Installment_Contract.FirstOrDefault(n => n.ID == id);
            if (contract != null)
            {
                InstallmentContractPrintModel fc = new InstallmentContractPrintModel();
                fc.Customer_Name = contract.Customer_Name;
                fc.Customer_Address = contract.Customer_Address;
                fc.Date_of_Contract = contract.Date_Of_Contract;
                fc.Deposit = contract.Deposit;
                fc.Mobile = contract.Mobile;
                fc.Price = contract.Price;
                fc.Property_Code = contract.Property.Property_Code;
                fc.SSN = contract.SSN;
                fc.Address = contract.Property.Address; ;
                fc.Installment_Contract_Code = contract.Installment_Contract_Code;
                return View(fc);
            }
            else
            {
                return View();
            }
        }
        public void dateYearOfBirth()
        {
            ViewBag.Year_Of_Birth = new SelectList(Enumerable.Range(1900, (DateTime.Now.Year - 1900) + 1));
            ViewBag.Payment_Period = new SelectList(Enumerable.Range(1, 12));
        }
    }
}
