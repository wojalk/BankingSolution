using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSolution.ViewModels.BankAccountViewModels
{
    public class TransferBankAccountViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string AccountNumber { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }
    }
}
