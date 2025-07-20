using System;

namespace ColdCash.Core.Models
{
    public enum TransactionType { Income, Expense, Transfer, EnvelopeTransfer }

    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public int? EnvelopeFromId { get; set; }
        public Envelope? EnvelopeFrom { get; set; }
        public int? EnvelopeToId { get; set; }
        public Envelope? EnvelopeTo { get; set; }
        public string? Notes { get; set; }
    }
}