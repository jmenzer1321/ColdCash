using System.Collections.Generic;
using System.Transactions;

namespace ColdCash.Core.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal CurrentBalance { get; set; }
        public string Currency { get; set; } = "USD";
        public List<Transaction> Transactions { get; set; } = new();
    }
}