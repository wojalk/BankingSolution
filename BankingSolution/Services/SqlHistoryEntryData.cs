using BankingSolution.Data;
using BankingSolution.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.Services
{
    public class SqlHistoryEntryData
    {
        private ApplicationDbContext _context;

        public SqlHistoryEntryData(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<HistoryEntry> GetAll()
        {
            return _context.HistoryEntries;
        }

        public HistoryEntry Get(int id)
        {
            return _context.HistoryEntries.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<HistoryEntry> GetAllForAccount(int bankAccountId)
        {
            return _context.HistoryEntries.Where(x => x.PerformedByUser == bankAccountId);
        }

        public HistoryEntry Add(HistoryEntry historyEntry)
        {
            _context.Add(historyEntry);
            _context.SaveChanges();
            return historyEntry;
        }
    }
}
