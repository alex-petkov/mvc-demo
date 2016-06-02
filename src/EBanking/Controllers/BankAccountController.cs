using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Bank.Models;
using EBanking.Data;

namespace Bank.Controllers
{
    [Authorize]
    public class BankAccountController : Controller
    {
        //
        // GET: /BankAccount/
        public ActionResult Index()
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

        public ActionResult CreateBankAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBankAccount(CreateBankAccount account)
        {
            if (ModelState.IsValid)
            {
                using (var db = new OurDbContext())
                {
                    var usr = db.User.FirstOrDefault(u => u.UserName == User.Identity.Name);
                    var bankAccount = new BankAccount
                    {
                        UserId = usr.Id,
                        Key = Guid.NewGuid(),
                        FriendlyName = account.FriendlyName,
                        Balance = 0
                    };

                    db.UserAccounts.Add(bankAccount);
                    db.SaveChanges();
                }
                return RedirectToAction("CreatedSuccessfully");
            }
            return View();
        }

        public ActionResult CreatedSuccessfully()
        {
            return View();
        }

        //Transactions
        public ActionResult Transfer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Transfer(TransferModel transfer)
        {
            if (ModelState.IsValid)
            {
                using (var db = new OurDbContext())
                {
                    var account = db.UserAccounts.FirstOrDefault(a => a.FriendlyName == transfer.MyBankAccount);
                    if (account == null)
                    {
                        return RedirectToAction("Error");

                    }

                    var usr = db.User.FirstOrDefault(u => u.UserName == User.Identity.Name);
                    if (usr.Id != account.UserId)
                    {
                        return RedirectToAction("Error");

                    }


                    var account1 = db.UserAccounts.FirstOrDefault(a => a.FriendlyName == transfer.OtherBankAccount);
                    if (account1 == null)
                    {
                        return RedirectToAction("Error");

                    }

                    var transaction = new Transaction
                    {
                        UserAccountId = account.Id,
                        Key = Guid.NewGuid(),
                        Type = TransactionType.Transfer,
                        EvenDate = DateTime.Now,
                        Amount = transfer.Balance
                    };
                    db.Transactions.Add(transaction);
                    
                    account.Balance = account.Balance - transaction.Amount;
                    account1.Balance = account1.Balance + transaction.Amount;
                    db.SaveChanges();
                 
                }
                return RedirectToAction("TransferSuccessfully");
            }
            return View();
        }

        public ActionResult TransferSuccessfully()
        {
            return View();
        }


        public ActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Deposit(DepositModel deposit)
        {
            if (ModelState.IsValid)
            {
                using (var db = new OurDbContext())
                {
                    var account = db.UserAccounts.FirstOrDefault(a => a.FriendlyName == deposit.MyBankAccount);
                    if (account == null)
                    {
                        return RedirectToAction("Error");
                    }

                    var usr = db.User.FirstOrDefault(u => u.UserName == User.Identity.Name);
                    if (usr.Id != account.UserId)
                    {
                         return RedirectToAction("Error");
                    }


                    var transaction = new Transaction
                    {
                        UserAccountId = account.Id,
                        Key = Guid.NewGuid(),
                        Type = TransactionType.Deposit,
                        EvenDate = DateTime.Now,
                        Amount = deposit.Balance
                    };
                    db.Transactions.Add(transaction);
                    if (transaction.Amount < 0)
                    {
                        ViewBag.Message = "Грешка! Не може сумата която искате да внесете да е отрицателна.";
                        
                        return View();
                    }
                    account.Balance = account.Balance + transaction.Amount;
                    db.SaveChanges();
                   
                }
                return RedirectToAction("DepositSuccessfully");
            }
            return View();
        }

        public ActionResult DepositSuccessfully()
        {
            return View();
        }


        public ActionResult WithDrawAl()
        {
            return View();
        }

        [HttpPost]
        public  ActionResult WithDrawAl(WithdrawalModel withdrawal)
        {
            if (ModelState.IsValid)
            {
                using (var db = new OurDbContext())
                {
                    var account = db.UserAccounts.FirstOrDefault(a => a.FriendlyName == withdrawal.MyAccount);
                    if (account == null)
                    {
                        return RedirectToAction("Error");

                    }

                    var usr = db.User.FirstOrDefault(u => u.UserName == User.Identity.Name);
                    if (usr.Id != account.UserId)
                    {
                        return RedirectToAction("Error");

                    }


                    var transaction = new Transaction
                    {
                        UserAccountId = account.Id,
                        Key = Guid.NewGuid(),
                        Type = TransactionType.Withdrawal,
                        EvenDate = DateTime.Now,
                        Amount = withdrawal.Balance
                    };
                    db.Transactions.Add(transaction);
                    account.Balance = account.Balance - transaction.Amount;
                    db.SaveChanges();
                  
                }
                return RedirectToAction("WithDrawAlSuccessfully");
            }

            return View();
        }

        public ActionResult WithDrawAlSuccessfully()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}