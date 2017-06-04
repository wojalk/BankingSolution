using BankingSolution.Entities;
using BankingSolution.ViewModels.BankAccountViewModel;
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

namespace BankingSolution.Controllers
{
    public class BankAccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private SqlBankAccountData _data;
        public object RandomString { get; private set; }

        public IActionResult Index()
        {
            return View();
        }

        public BankAccountController(UserManager<ApplicationUser> manager, SqlBankAccountData data)
        {
            _userManager = manager;
            _data = data;
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
        public async Task<IActionResult> Register(RegisterBankAccountViewModel model, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
                ApplicationUser appUser = await GetCurrentUserAsync();
                var bankAccount = new BankAccount {
                    AccountNumber = GenerateRandomString(20),
                    DailyLimit = model.DailyLimit,
                    MonthlyLimit = model.MonthlyLimit,
                    AccountBalance = 0,
                    BankAccountType = BankAccountType.Bronze,
                    AccountOwnerId = appUser.Id
                };

                _data.Add(bankAccount);
                
                return RedirectToLocal(returnUrl);
            }
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
    }
}
