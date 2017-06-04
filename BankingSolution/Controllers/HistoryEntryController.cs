using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BankingSolution.Controllers
{
    [Authorize]
    public class HistoryEntryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public HistoryEntryController()
        {

        }

        [HttpGet]
        public IActionResult RegisterHistoryEntry()
        {
            return null;
        }
    }
}