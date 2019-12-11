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
    public class Full_ContractAdminController : Controller
    {
        private PPCDBEntities db = new PPCDBEntities();

        // GET: Admin/Full_Contract
        public ActionResult Index()
        {
            var full_Contract = db.Full_Contract.Include(f => f.Property);
            return View(full_Contract.ToList());
        }


        // GET: Admin/Full_Contract/Create
        public ActionResult Create(int id)
        {
            //Property property = model.Properties.FirstOrDefault(x => x.ID == id);
            //var inOrder_Installment = model.Installment_Contract.Where(x => x.Property_ID == id);
            //var inOrder_FullContact = model.Full_Contract.Where(x => x.Property_ID == id);
            //var orn = model.Property_Service.Where(x => x.Property_ID == property.ID).ToArray();
            //if (inOrder_FullContact.Count() != 0 || inOrder_Installment.Count() != 0)
            //{
            //    TempData["msg"] = "<script>alert('BẤT ĐỘNG SẢN HIỆN ĐANG TRONG QUÁ TRÌNH CÓ HỢP ĐỒNG!!! VUI LÒNG QUAY LẠI.');</script>";
            //}
            //else
            //{
            //    for (int i = 0; i < orn.Length; i++)
            //    {
            //        var tmp = orn[i];
            //        Property_Service psDell = model.Property_Service.FirstOrDefault(x => x.Property_ID == tmp.Property_ID);
            //        model.Property_Service.Remove(psDell);
            //        model.SaveChanges();

            //    }
            //    model.Properties.Remove(property);
            //    model.SaveChanges();
            //    PopularMessage(true);
            //    return RedirectToAction("Index");
            //}
            //PopularMessage(false);
            //return View(property);

            var property = db.Properties.FirstOrDefault(x => x.ID == id);
            dateYearOfBirth();

            if (property != null)
            {
                var fc = new Full_Contract();
                fc.Price = property.Price;
                ViewData["PP_Code"] = property.Property_Code;
                return View(fc);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customer_Name,Year_Of_Birth,SSN,Customer_Address," +
            "Mobile,Price,Deposit,Remain")] Full_Contract contract, string Property_Code)
        {
            var property = db.Properties.FirstOrDefault(m => m.Property_Code.Equals(Property_Code.ToString()));
            int Property_ID = property.ID;
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    contract.Property_ID = Property_ID;
                    contract.Remain = contract.Price - contract.Deposit;
                    contract.Status = false;
                    contract.Date_Of_Contract = DateTime.Now;
                    db.Full_Contract.Add(contract);
                    db.SaveChanges();
                    scope.Complete();
                }

                return RedirectToAction("Index");
            }
            dateYearOfBirth();

            return View("NewContract", contract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Full_Contract contract, string Mobile, string Customer_Name, decimal Deposit, decimal Remain, string Address, string ssn )
        {
            if (ModelState.IsValid)
            {
                contract = db.Full_Contract.FirstOrDefault(x => x.ID == contract.ID);
                contract.Customer_Name = Customer_Name;
                contract.Mobile = Mobile;
                contract.Deposit = Deposit;
                contract.Customer_Address = Address;
                contract.Remain = contract.Price - contract.Deposit;
                contract.SSN = ssn;
                contract.Date_Of_Contract = DateTime.Now;
                db.Entry(contract).State = EntityState.Modified;         
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit", contract);
        }

        // GET: Admin/Full_Contract/Delete/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index", "notfound");
            }
            Full_Contract contract = db.Full_Contract.FirstOrDefault(x => x.ID == id);

            if (contract == null)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(contract);
        }

        // POST: Admin/Full_Contract/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index", "notfound");
            }
            Full_Contract fc = db.Full_Contract.FirstOrDefault(x => x.ID == id);

            if (fc == null)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(fc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Full_Contract contract = db.Full_Contract.FirstOrDefault(x => x.ID == id);
            db.Full_Contract.Remove(contract);
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
                fc.Date_of_Contract = contract.Date_Of_Contract;
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
        public void dateYearOfBirth()
        {
            ViewBag.Year_Of_Birth = new SelectList(Enumerable.Range(1900, (DateTime.Now.Year - 1900) + 1));
        }
    }
}
