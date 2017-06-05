using BankingSolution.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using BankingSolution.Data;
using BankingSolution.Services;
using BankingSolution.ViewModels.BankAccountViewModels;

namespace BankingSolution.Controllers
{
    [Authorize]
    public class BankAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _context;
        private SqlBankAccountData _bankAccountData;
        private SqlHistoryEntryData _historyEntryData;
        public object RandomString { get; private set; }

        public IActionResult Index()
        {
            return View();
        }

        public BankAccountController(UserManager<ApplicationUser> manager,
            ApplicationDbContext context,
            SqlBankAccountData bankAccountData,
            SqlHistoryEntryData historyEntryData)
        {
            _userManager = manager;
            _bankAccountData = bankAccountData;
            _historyEntryData = historyEntryData;
            _context = context;
        }

        internal static string GenerateRandomString(int length)
        {
            Random randomGenerator = new Random();
            byte[] randomBytes = new byte[randomGenerator.Next(length)];
            randomGenerator.NextBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterBankAccountViewModel model, string returnUrl="/BankAccount/List")
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var bankAccount = new BankAccount {
                    AccountNumber = GenerateRandomString(20),
                    DailyLimit = model.DailyLimit,
                    MonthlyLimit = model.MonthlyLimit,
                    AccountBalance = 0,
                    BankAccountType = BankAccountType.Bronze,
                    AccountOwnerId = _userManager.GetUserId(User)
                };

                _bankAccountData.Add(bankAccount);
                
                return RedirectToLocal(returnUrl);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult List(ListBankAccountViewModel model, string returnUrl)
        {
            model.bankAccounts = _bankAccountData.GetForUser(_userManager.GetUserId(User));
            return View(model);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }


        [HttpGet]
        public IActionResult Transfer(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(TransferBankAccountViewModel model, string returnUrl="/BankAccount/List")
        {
            int id = int.Parse(Request.Path.ToUriComponent().Split('/').Last());
            string recipientAccountNumber = model.AccountNumber;
            double AmountTransferred = model.Amount;



            if (ModelState.IsValid)
            {
                BankAccount from = _bankAccountData.Get(id);
                BankAccount recipient = _bankAccountData.GetAll().Where(r => r.AccountNumber.Equals(model.AccountNumber)).FirstOrDefault();

                if (from.AccountBalance > AmountTransferred || recipient!= null)
                {
                    from.AccountBalance -= AmountTransferred;
                    recipient.AccountBalance += AmountTransferred;

                    HistoryEntry historyEntry = new HistoryEntry() {
                        AmountTransferred = model.Amount,
                        Date = DateTime.Now,
                        PerformedBy = from.AccountNumber,
                        TargetAccountNumber = model.AccountNumber
                    };

                    _historyEntryData.Add(historyEntry);

                    _context.Entry(from).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.Entry(recipient).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToLocal(returnUrl);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult History(HistoryBankAccountViewModel model, string returnUrl)
        {
            int id = int.Parse(Request.Path.ToUriComponent().Split('/').Last());
            BankAccount bankAccount = _bankAccountData.Get(id);

            //IEnumerable<HistoryEntry> historyEntries = _historyEntryData.GetAllForAccount(bankAccount.AccountNumber);
            model.HistoryEntries = _historyEntryData.GetAllForAccount(bankAccount.AccountNumber).ToList();

            return View(model);
        }
    }
}
