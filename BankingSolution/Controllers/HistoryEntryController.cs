using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BankingSolution.Services;
using BankingSolution.ViewModels.BankAccountViewModels;
using BankingSolution.Entities;

namespace BankingSolution.Controllers
{
    [Authorize]
    public class HistoryEntryController : Controller
    {
        private SqlBankAccountData _bankAccountData;
        private SqlHistoryEntryData _historyEntryData;

        public IActionResult Index()
        {
            return View();
        }

        public HistoryEntryController(SqlBankAccountData bankAccountData,
            SqlHistoryEntryData historyEntryData)
        {
            _bankAccountData = bankAccountData;
            _historyEntryData = historyEntryData;
        }

        [HttpGet]
        public IActionResult RegisterHistoryEntry(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet]
        public IActionResult History(HistoryBankAccountViewModel model, string returnUrl)
        {
            int id = int.Parse(Request.Path.ToUriComponent().Split('/').Last());
            BankAccount bankAccount = _bankAccountData.Get(id);

            IEnumerable<HistoryEntry> historyEntries = _historyEntryData.GetAllForAccount(bankAccount.AccountNumber);
            model.HistoryEntries = historyEntries;

            return View();
        }
    }
}