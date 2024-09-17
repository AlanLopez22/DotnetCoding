namespace DotnetCoding.Core.Models
{
    public class ProductDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public ProductStatus Status { get; set; }
        public ProductState State { get; set; }
        public DateTime? PostedDate { get; set; } = DateTime.MinValue;
        public DateTime? UpdatedDate { get; set; } = DateTime.MinValue;
        public DateTime? DeletedDate { get; set; } = DateTime.MinValue;

        public ICollection<ProductQueue> Queues { get; set; } = new List<ProductQueue>();
    }
}
