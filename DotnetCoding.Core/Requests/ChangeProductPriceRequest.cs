namespace DotnetCoding.Core.Requests
{
    public class ChangeProductPriceRequest
    {
        public Guid ProductId { get; set; }
        public double NewPrice { get; set; }
    }
}
