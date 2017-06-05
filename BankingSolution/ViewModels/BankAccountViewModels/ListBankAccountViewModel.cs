using BankingSolution.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.ViewModels.BankAccountViewModels
{
    public class ListBankAccountViewModel
    {
        public IEnumerable<BankAccount> bankAccounts { get; set; }
    }
}
