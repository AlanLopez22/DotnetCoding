namespace DotnetCoding.Core.Responses
{
    public class ProductApprovalResponse
    {
        public string ProductName { get; set; } = string.Empty;
        public string RequestReason { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
    }
}
