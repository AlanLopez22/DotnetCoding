namespace DotnetCoding.Core.Models
{
    public class ProductQueue
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public QueueState State { get; set; }
        public string RequestReason { get; set; } = string.Empty;
        public DateTime RequestedDate { get; set; }
        public DateTime? RejectedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public ProductDetails? Product { get; set; }
    }
}
