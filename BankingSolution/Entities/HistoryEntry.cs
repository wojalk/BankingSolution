using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.Entities
{
    public class HistoryEntry
    {
        public int Id { get; set; }

        public double AmountTransferred;

        public DateTime? Date { get; set; }
        public int PerformedByUser { get; set; }
        public string TargetAccountNumber;
    }
}
