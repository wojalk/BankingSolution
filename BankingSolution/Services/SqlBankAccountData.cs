using BankingSolution.Data;
using BankingSolution.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.Services
{
    public class SqlBankAccountData
    {
        private ApplicationDbContext _context;

        public SqlBankAccountData(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BankAccount> GetAll()
        {
            return _context.BankAccounts;
        }

        public BankAccount Get(int id)
        {
            return _context.BankAccounts.FirstOrDefault(r => r.Id == id);
        }

        public BankAccount Add(BankAccount bankAccount)
        {
            _context.Add(bankAccount);
            _context.SaveChanges();
            return bankAccount;
        }
    }
}
