using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Propertymanagerment.Models;

namespace Propertymanagerment.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        // GET: Admin/Auth
        PPCDBEntities model = new PPCDBEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Account account)
        {
            if (ModelState.IsValid)
            {
                var acc = model.Accounts.Where(x => x.Username.Equals(account.Username) && x.Password.Equals(account.Password)).FirstOrDefault();
                if (acc != null)
                {
                    Session["ID"] = acc.ID;
                    Session["Username"] = acc.Username.ToString();
                    Session["Role"] = acc.Role.ToString();
                    return Redirect("/Admin/PropertyAdmin");
                }
            }
            return View(account);
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session["ID"] = null;
            Session["Username"] = null;
            Session["Role"] = null;
            return RedirectToAction("Login");
        }
    }
}