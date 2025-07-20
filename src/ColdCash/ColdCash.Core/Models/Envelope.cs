namespace ColdCash.Core.Models
{
    public class Envelope
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal AllocatedAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public bool AutoCarryOver { get; set; } = true;
    }
}