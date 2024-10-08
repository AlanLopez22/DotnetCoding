﻿namespace DotnetCoding.Core.Responses
{
    public class ProductApprovalResponse
    {
        public Guid QueueId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string RequestReason { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
    }
}
