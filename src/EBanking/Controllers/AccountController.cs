using System;
using System.Linq;
using System.Net.Cache;
using System.Web.Mvc;
using System.Web.Security;
using Bank.Library;
using Bank.Models;
using EBanking.Data;

namespace Bank.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public ActionResult Index()
        {
            using (var db = new OurDbContext())
            {
                return View(db.User.ToList());
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                using (var db = new OurDbContext())
                {
                    var hash = PasswordUncode.Hash(account.Password);


                    var user = new DataBaseUserModel
                    {
                        UserName = account.UserName,
                        Password = hash,
                        FullName = account.FullName,
                        Email = account.Email,
                        DateRegistered = DateTime.Now
                    };


                    db.User.Add(user);
                    db.SaveChanges();
                }
                ModelState.Clear();
                //ViewBag.Message = account.FullName + "  successfully registered";
                return RedirectToAction("RegisterSuccessfully");
            }
            return View();
        }

        public ActionResult RegisterSuccessfully()
        {
            return View();
        }

        //Login

        public ActionResult Login()
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index","BankAccount");

            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index","BankAccount");

            using (var db = new OurDbContext())
            {
                var usr = db.User.FirstOrDefault(u => u.UserName==user.UserName);
                var result = usr != null && PasswordUncode.Verify(user.Password,usr.Password);
              
                if (result)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);

                    return RedirectToAction("Index","BankAccount");
                }
                ModelState.AddModelError("", "Username or Password is wrong.");
            }
            return View();
        }

        [Authorize]
        public ActionResult LoggedIn()
        {
            using (var db = new OurDbContext())
            {
                var bankAccounts = db.UserAccounts.Where(u => u.User.UserName == User.Identity.Name)
                    .Select(ua => new ShowBankAccount
                    {
                        Key = ua.Key,
                        FriendlyName = ua.FriendlyName,
                        Balance = ua.Balance
                    }).ToList();

                return View(bankAccounts);
            }
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");

            
        }

    }
}