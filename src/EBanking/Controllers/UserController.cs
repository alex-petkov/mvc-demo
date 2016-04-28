using EBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EBanking.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/Create
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /User/Create
        [HttpPost]
        public ActionResult Register(RegisterUserModel newUser)
        {
            if (!ModelState.IsValid)
                return View(newUser);

            // TODO: instert record into database
            // TODO: check for a duplicate username and report error

            FormsAuthentication.SetAuthCookie(newUser.Username, false);

            return RedirectToAction("Index", "Home");
        }
    }
}
