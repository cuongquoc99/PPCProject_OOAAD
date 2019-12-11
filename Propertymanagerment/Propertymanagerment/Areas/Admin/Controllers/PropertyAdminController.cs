using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PropertyManagerment.Models;
using System.IO;
using System.Transactions;
using System.Data.Entity;

namespace PropertyManagerment.Areas.Admin.Controllers
{
    public class PropertyAdminController : Controller
    {
        PPCDBEntities model = new PPCDBEntities();
        // GET: Admin/PropertyAdmin
        public ActionResult Index()
        {
            var property = model.Properties.ToList();

            return View(property);
        }

        public ActionResult Create()
        {
            PopuparData();

            otherData();

            return View();
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index", "notfound");
            }
            Property property = model.Properties.FirstOrDefault(x => x.ID == id);

            if (property == null)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(property);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index", "notfound");
            }
            Property property = model.Properties.FirstOrDefault(x => x.ID == id);

            if (property == null)
            {
                return RedirectToAction("index", "notfound");
            }
            otherData(property.District.City_ID, model.Property_Service.Where(x => x.Property_ID == property.ID).Select(x => x.Service_ID));
            PopuparData(property.Property_Type_ID, property.District_ID, property.Property_Status_ID);
            return View(property);
        }

        public void PopuparData(object propertypeSelected = null, object districtSelected = null, object statusSelected = null)
        {
            ViewBag.Property_Type_ID = new SelectList(model.Property_Type.ToList(), "ID", "Property_Type_Name", propertypeSelected);
            ViewBag.District_ID = new SelectList(model.Districts.ToList(), "ID", "District_Name", districtSelected);
            ViewBag.Property_Status_ID = new SelectList(model.Property_Status.ToList(), "ID", "Property_Status_Name", statusSelected);

        }
        public void PopularMessage(bool success)
        {
            if (success == true)
            {
                Session["success"] = "Sucessfull";
            }

            else
            {
                Session["success"] = "Failed";
            }

        }
        [HttpPost]

        public ActionResult Create([Bind(Include = "Property_Name,Property_Type_ID,Description,District_ID,Address" +
            ",Area,Bed_Room,Bath_Room,Price,Installment_Rate,Avatar,Album,Property_Status_ID")]
        Property property, HttpPostedFileBase[] files, HttpPostedFileBase file
           , List<int> Service_ID)
        {

            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {

                    model.Properties.Add(property);

                    string album = "";
                    Random random = new Random();
                    if (files != null)
                    {
                        foreach (var imageFile in files)
                        {
                            if (imageFile != null)
                            {
                                var fileName = random.Next(1, 99999).ToString() + Path.GetFileName(imageFile.FileName);
                                var physicalPath = Path.Combine(Server.MapPath("~/Images/"), fileName);

                                // The files are not actually saved in this demo
                                imageFile.SaveAs(physicalPath);
                                album += album.Length > 0 ? ";" + fileName : fileName;
                            }
                        }
                    }
                    property.Album = album;
                    if (file != null)
                    {
                        var avatar = random.Next(1, 99999).ToString() + Path.GetFileName(file.FileName);
                        var physicPath = Path.Combine(Server.MapPath("~/Images/"), avatar);
                        file.SaveAs(physicPath);
                        property.Avatar = avatar;
                    }



                    foreach (var item in Service_ID)
                    {
                        Property_Service properS = new Property_Service();
                        properS.Property_ID = property.ID;
                        properS.Service_ID = item;
                        model.Property_Service.Add(properS);


                    }

                    model.SaveChanges();
                    scope.Complete();
                    PopularMessage(true);
                    return RedirectToAction("Index");

                }
            }
            otherData();
            PopuparData();
            PopularMessage(false);
            return View("Create", property);

        }

        [HttpPost]
        public ActionResult Edit(Property property, string Property_Name, int Property_Type_ID, string Description,
           string Address, int Area, int Bed_Room, int Bath_Room, decimal Price, double Installment_Rate, int District_ID, int Property_Status_ID, HttpPostedFileBase[] files, HttpPostedFileBase file
         , List<int> Service_ID)
        {
            if (ModelState.IsValid)
            {

                using (var scrope = new TransactionScope())
                {

                    property = model.Properties.FirstOrDefault(x => x.ID == property.ID);
                    string album = property.Album;
                    Random random = new Random();
                    if (files != null)
                    {
                        foreach (var imageFile in files)
                        {
                            if (imageFile != null)
                            {
                                var fileName = random.Next(1, 99999).ToString() + Path.GetFileName(imageFile.FileName);
                                var physicalPath = Path.Combine(Server.MapPath("~/Images"), fileName);

                                // The files are not actually saved in this demo
                                imageFile.SaveAs(physicalPath);
                                album += album.Length > 0 ? ";" + fileName : fileName;
                            }
                        }

                    }
                    property.Album = album;
                    // Avatar
                    if (file != null)
                    {
                        var avatar = random.Next(1, 99999).ToString() + Path.GetFileName(file.FileName);
                        var physicPath = Path.Combine(Server.MapPath("~/Images"), avatar);
                        file.SaveAs(physicPath);
                        property.Avatar = avatar;
                    }

                    // add model to database
                    property.Address = Address;
                    property.Area = Area;
                    property.Description = Description;
                    property.Bath_Room = Bath_Room;
                    property.Bed_Room = Bed_Room;
                    property.Installment_Rate = Installment_Rate;
                    property.Price = Price;
                    property.Property_Type_ID = Property_Type_ID;
                    property.Property_Name = Property_Name;
                    property.District_ID = District_ID;
                    property.Property_Status_ID = Property_Status_ID;


                    if (Service_ID != null)
                    {
                        var orn = model.Property_Service.Where(x => x.Property_ID == property.ID).ToArray();
                        for (int i = 0; i < orn.Length; i++)
                        {
                            var tmp = orn[i];
                            Property_Service psDell = model.Property_Service.FirstOrDefault(x => x.Property_ID == tmp.Property_ID);
                            model.Property_Service.Remove(psDell);
                            model.SaveChanges();
                        }
                    }

                    foreach (var item in Service_ID)
                    {
                        Property_Service properS = new Property_Service();
                        properS.Property_ID = property.ID;
                        properS.Service_ID = item;
                        model.Property_Service.Add(properS);
                        model.SaveChanges();
                    }
                    model.Entry(property).State = EntityState.Modified;
                    model.SaveChanges();
                    // save file to app_data
                    scrope.Complete();
                    // all done successfully

                    return RedirectToAction("Index");
                }
            }
            otherData();
            PopuparData();
            return View("Edit", property);
        }


        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Property property = model.Properties.FirstOrDefault(x => x.ID == id);

        //    var orn = model.Property_Service.Where(x => x.Property_ID == property.ID).ToArray();
        //    for (int i = 0; i < orn.Length; i++)
        //    {
        //        var tmp = orn[i];
        //        Property_Service psDell = model.Property_Service.FirstOrDefault(x => x.Property_ID == tmp.Property_ID);
        //        model.Property_Service.Remove(psDell);
        //        model.SaveChanges();
        //    }
        //    model.Properties.Remove(property);
        //    model.SaveChanges();
        //    return RedirectToAction("Index");

        //}

        public void otherData(object selectedCity = null, IQueryable<int> selectedService = null)
        {

            ViewBag.City_ID = new SelectList(model.Cities.ToList(), "ID", "City_Name", selectedCity);


            ViewBag.Service_ID = new MultiSelectList(model.Services.ToList(), "ID", "Service_Name", selectedService);
        }

        public JsonResult GetDistrictByCityId(int id)
        {
            // Disable proxy creation
            model.Configuration.ProxyCreationEnabled = false;
            var listDistrict = model.Districts.Where(x => x.City_ID == id).ToList();
            return Json(listDistrict, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public string deleteImage(string imageName, int id)
        {
            string fullPath = Request.MapPath("~/Images" + imageName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            var property = model.Properties.FirstOrDefault(x => x.ID == id);
            var album = property.Album.Split(';');
            album = album.Where(w => w != imageName).ToArray();
            property.Album = string.Join(";", album);

            model.Entry(property).State = EntityState.Modified;
            model.SaveChanges();
            return property.Album;
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Property property = model.Properties.FirstOrDefault(x => x.ID == id);
            var inOrder_Installment = model.Installment_Contract.Where(x => x.Property_ID == id);
            var inOrder_FullContact = model.Full_Contract.Where(x => x.Property_ID == id);
            var orn = model.Property_Service.Where(x => x.Property_ID == property.ID).ToArray();
            if (inOrder_FullContact.Count() != 0 || inOrder_Installment.Count() != 0)
            {
                TempData["msg"] = "<script>alert('BẤT ĐỘNG SẢN HIỆN ĐANG TRONG QUÁ TRÌNH CÓ HỢP ĐỒNG!!! VUI LÒNG QUAY LẠI.');</script>";
            }
            else
            {
                for (int i = 0; i < orn.Length; i++)
                {
                    var tmp = orn[i];
                    Property_Service psDell = model.Property_Service.FirstOrDefault(x => x.Property_ID == tmp.Property_ID);
                    model.Property_Service.Remove(psDell);
                    model.SaveChanges();

                }  
                model.Properties.Remove(property);
                model.SaveChanges();
                PopularMessage(true);
                return RedirectToAction("Index"); 
            }
            PopularMessage(false);
            return View(property);
        }

    }
}
