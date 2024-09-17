namespace DotnetCoding.Core.Models
{
    public class ProductQueue
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public QueueState State { get; set; }
        public string RequestReason { get; set; } = string.Empty;
        public DateTime RequestedDate { get; set; } = DateTime.MinValue;
        public DateTime? RejectedDate { get; set; } = DateTime.MinValue;
        public DateTime? ApprovedDate { get; set; } = DateTime.MinValue;
        public ProductDetails? Product { get; set; }
    }
}
