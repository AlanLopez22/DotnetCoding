namespace DotnetCoding.Core.Models
{
    public class ProductQueue
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string RequestReason { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; } = DateTime.MinValue;
        public DateTime InactivatedDate { get; set; } = DateTime.MinValue;
        public ProductDetails? Product { get; set; }
    }
}
