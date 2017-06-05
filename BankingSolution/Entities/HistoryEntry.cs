using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.Entities
{
    public class HistoryEntry
    {
        public int Id { get; set; }

        public double AmountTransferred { get; set; }

        public DateTime Date { get; set; }
        public string PerformedBy { get; set; }
        public string TargetAccountNumber { get; set; }
    }
}
