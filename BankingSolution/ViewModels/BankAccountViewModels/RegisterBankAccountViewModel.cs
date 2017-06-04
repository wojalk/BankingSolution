using BankingSolution.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.ViewModels.BankAccountViewModel
{
    public class RegisterBankAccountViewModel
    {
        public double DailyLimit { get; set; }
        public double MonthlyLimit { get; set; }
    }
}
