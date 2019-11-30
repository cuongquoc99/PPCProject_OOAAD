using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Transactions;
using Propertymanagerment.Models;

namespace Propertymanagerment.Areas.Admin.Controllers
{
    public class PropertyAdminController : Controller
    {
        PPCDBEntities model = new PPCDBEntities();

        //
        // GET: /Admin/PropertyAdmin/
        public ActionResult Index()
        {
            var property = model.Properties.ToList();
            return View(property);
        }

        [HttpGet]
        public ActionResult Create(){
            PopularData();
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Property_Name, Property_Type_ID, Description, District_ID, Address, Area, Bed_Room, Bath_Room, Price, Installment_Rate, Avatar, Album, Property_Status_ID")] Property property)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {

                    // add model to database
                    var prop= new Property();
                    prop.Property_Name = property.Property_Name;
                    prop.Property_Type_ID = property.Property_Type_ID;
                    prop.Description = property.Description;
                    prop.District_ID = property.District_ID;
                    prop.Address = property.Address;
                    prop.Area = property.Area;
                    prop.Bed_Room = property.Bed_Room;
                    prop.Bath_Room = property.Bath_Room;
                    prop.Price = property.Price;
                    prop.Installment_Rate = property.Installment_Rate;
                    prop.Avatar = property.Avatar;
                    prop.Album = property.Album;
                    prop.Property_Status_ID = property.Property_Status_ID;

                    // save file to app_data
                    model.Properties.Add(property);
                    model.SaveChanges();
                    PopularMessage(true);

                    var path = Server.MapPath("~/App_Data");
                    path = System.IO.Path.Combine(path, property.ID.ToString());
                    Request.Files["Image"].SaveAs(path + "_0");
                    Request.Files["Image1"].SaveAs(path + "_1");
                    Request.Files["Image2"].SaveAs(path + "_2");

                    // all done successfully
                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }
            else
                PopularMessage(false);
                PopularData();
                return View("Create", property);
        }
        public void PopularMessage(bool success)
        {
            if (success)
                Session["success"] = "Successful!";
            else
                Session["success"] = "Fail!";
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var property = model.Properties.FirstOrDefault(x => x.ID == id);
            PopularData(property.Property_Type_ID, property.District_ID, property.Property_Status_ID);
            return View(property);
        }

        [HttpPost]
        public ActionResult Edit(int id, Property pr)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    // add model to database
                    var prop = model.Properties.FirstOrDefault(x => x.ID == id);
                    prop.Property_Name = pr.Property_Name;
                    prop.Property_Type_ID = pr.Property_Type_ID;
                    prop.Description = pr.Description;
                    prop.District_ID = pr.District_ID;
                    prop.Address = pr.Address;
                    prop.Area = pr.Area;
                    prop.Bed_Room = pr.Bed_Room;
                    prop.Bath_Room = pr.Bath_Room;
                    prop.Price = pr.Price;
                    prop.Installment_Rate = pr.Installment_Rate;
                    prop.Avatar = pr.Avatar;
                    prop.Album = pr.Album;
                    prop.Property_Status_ID = pr.Property_Status_ID;

                    // save file to app_data
                    var path = Server.MapPath("~/App_Data");
                    path = System.IO.Path.Combine(path, pr.ID.ToString());

                    if (Request.Files["Image"].ContentLength != 0)
                    {
                        Request.Files["Image"].SaveAs(path + "_0");
                    }

                    if (Request.Files["Image1"].ContentLength != 0)
                    {
                        Request.Files["Image1"].SaveAs(path + "_1");
                    }

                    if (Request.Files["Image2"].ContentLength != 0)
                    {
                        Request.Files["Image2"].SaveAs(path + "_2");
                    }

                    model.Entry(prop).State = EntityState.Modified;
                    model.SaveChanges();
                    PopularMessage(true);

                    // all done successfully
                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }
            else
                PopularMessage(false);
                PopularData();
                return View("Edit", pr);      
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var property = model.Properties.FirstOrDefault(x => x.ID == id);
            return View(property);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var property = model.Properties.FirstOrDefault(x => x.ID == id);
            model.Properties.Remove(property);
            model.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var property = model.Properties.FirstOrDefault(x => x.ID == id);
            return View(property);
        }

        public void PopularData(object propertyTypeSelected = null, object districtSelected = null, object propertStatusSelected = null, object citySelected = null)
        {
            ViewBag.District_ID = new SelectList(model.Districts.ToList(), "ID", "District_Name", districtSelected);
            ViewBag.Property_Type_ID = new SelectList(model.Property_Type.ToList(), "ID", "Property_Type_Name", propertyTypeSelected);
            ViewBag.Property_Status_ID = new SelectList(model.Property_Status.ToList(), "ID", "Property_Status_Name", propertStatusSelected);
            ViewBag.City_ID = new SelectList(model.Cities.ToList(), "ID", "City_Name", citySelected);
        }

        public ActionResult Image(string id)
        {
            var path = Server.MapPath("~/App_Data");
            path = System.IO.Path.Combine(path, id);
            return File(path + "_0", "image/*");
        }

        public ActionResult Image1(string id)
        {
            var path = Server.MapPath("~/App_Data");
            path = System.IO.Path.Combine(path, id);
            return File(path + "_1", "image/*");
        }

        public ActionResult Image2(string id)
        {
            var path = Server.MapPath("~/App_Data");
            path = System.IO.Path.Combine(path, id);
            return File(path + "_2", "image/*");
        }

        public JsonResult GetDistrictByCityId(int id)
        {
            // Disable proxy creation
            model.Configuration.ProxyCreationEnabled = false;
            var listDistrict = model.Districts.Where(x => x.City_ID == id).ToList();
            return Json(listDistrict, JsonRequestBehavior.AllowGet);
        }
    }
}