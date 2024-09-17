namespace DotnetCoding.Core.Models;

public class ChangeRequest
{
    public Guid Id { get; set; }
    public Guid ProductQueueId { get; set; }
    public string PropertyName { get; set; } = string.Empty;
    public string CurrentValue { get; set; } = string.Empty;
    public string NewValue { get; set; } = string.Empty;
    public ProductQueue? ProductQueue { get; set; }
}
