using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.Entities
{
    public enum BankAccountType
    {
        Bronze, Gold, Platinum
    }

    public class BankAccount
    {
        public int Id { get; set; }

        public string AccountOwnerId { get; set; }
        public BankAccountType? BankAccountType { get; set; }
        public ICollection<HistoryEntry> HistoryEntries { get; set; }

        public string AccountNumber { get; set; }
        public double AccountBalance { get; set; }
        public double DailyLimit { get; set; }
        public double MonthlyLimit { get; set; }
    }
}
