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
                    var account = db.UserAccounts
                        .FirstOrDefault(a => a.FriendlyName == transfer.MyBankAccount &&
                                             a.User.UserName == User.Identity.Name);

                    if (account == null)
                        return RedirectToAction("Error");
                    
                    var account1 = db.UserAccounts
                        .FirstOrDefault(a => a.FriendlyName == transfer.OtherBankAccount);

                    if (account1 == null)
                    {
                        return RedirectToAction("Error");

                    }

                    var transactionKey = Guid.NewGuid();

                    var transaction = new Transaction
                    {
                        UserAccountId = account.Id,
                        Key = transactionKey,
                        Type = TransactionType.Withdrawal,
                        EvenDate = DateTime.Now,
                        Amount = transfer.Balance,
                        
                    };


                    var transaction2 = new Transaction
                    {
                        UserAccountId = account1.Id,
                        Key = transactionKey,
                        Type = TransactionType.Deposit,
                        EvenDate = DateTime.Now,
                        Amount = transfer.Balance,
                        Comment = "Направен е превод от банкова сметка " + account.FriendlyName + " към банкова сметка " + account1.FriendlyName + " на стойност от " + transfer.Balance + " лв."

                      
                    };


                    db.Transactions.Add(transaction);
                    db.Transactions.Add(transaction2);

                    if (transaction.Amount < 0)
                    {
                        ModelState.AddModelError(string.Empty, "Грешка! Не може сумата която искате да внесете да е отрицателна.");

                        return View();
                    }

                    if (account.Balance < transfer.Balance)
                    {
                        ModelState.AddModelError(string.Empty, "Грешка! Нямате достатъчно пари за да направите превод. Вашата сума пари е " + account.Balance + " лв." );
                        return View();
                    }
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
                        Amount = deposit.Balance,
                        Comment = "Направен е депозит към банкова сметка " + account.FriendlyName + " на стойност от " + deposit.Balance + " лв."
                    };
                    db.Transactions.Add(transaction);
                    if (transaction.Amount < 0)
                    {
                        ModelState.AddModelError(string.Empty,"Грешка! Не може сумата която искате да внесете да е отрицателна.");
                        
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
                        Amount = withdrawal.Balance,
                        Comment = "Направено е теглене от банкова сметка " + account.FriendlyName + " на стойност от " + withdrawal.Balance + " лв."
                    };
                    db.Transactions.Add(transaction);
                    if (transaction.Amount < 0)
                    {
                        ModelState.AddModelError(string.Empty, "Грешка! Не може сумата която искате да изтеглите да е отрицателна.");

                        return View();
                    }


                    if (account.Balance < withdrawal.Balance)
                    {
                        ModelState.AddModelError(string.Empty, "Грешка! Нямате достатъчно пари, за да направите теглене. Вашата сума пари е " + account.Balance + " лв.");
                        return View();
                    }

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

        public ActionResult AccountCheck()
        {
            return View();
        }

        [HttpPost]

        public ActionResult AccountCheck(AccountCheck check)
        {
            if (ModelState.IsValid)
            {
                using (var db = new OurDbContext())
                {
                    var account = db.UserAccounts.FirstOrDefault(a => a.FriendlyName == check.MyBankAccount &&
                                                                 a.User.UserName==User.Identity.Name);
                    if (account == null)
                        return RedirectToAction("Error");
                }
            }
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Options()
        {
            return View();
        }
    }
}