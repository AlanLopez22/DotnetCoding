namespace DotnetCoding.Core.Models
{
    public class ProductQueue
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public string RequestReason { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; } = DateTime.MinValue;
    }
}
