using BankingSolution.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.ViewModels.BankAccountViewModels
{
    public class HistoryBankAccountViewModel
    {
        public IEnumerable<HistoryEntry> HistoryEntries { get; set; }
    }
}
