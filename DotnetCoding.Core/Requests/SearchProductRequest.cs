namespace DotnetCoding.Core.Requests
{
    public class SearchProductRequest
    {
        public string ProductName { get; set; } = string.Empty;
        public DateTime? FromPostedDate { get; set; }
        public DateTime? ToPostedDate { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
    }
}
