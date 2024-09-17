namespace DotnetCoding.Services.Constants
{
    internal class ErrorMessages
    {
        public const string ProductNotFound = "The requested product was not found.";
        public const string ProductQueueNotFound = "The requested product queue was not found.";
        public const string CouldNotCreateProduct = "Could not create the product.";
        public const string CouldNotUpdateProduct = "Could not update the product.";
        public const string CouldNotApproveProductQueue = "Could not approve the product queue.";
        public const string CouldNotRejectProductQueue = "Could not reject the product queue.";
        public const string CouldNotAddProductIntoQueue = "Could not add the product into the queue.";
        public const string ProductPriceOverTenThousand = "Can not create a product with a price more than $10,000 USD.";
        public const string ProductQueueHasBeenRejected = "The requested product queue has been already rejected.";
        public const string ProductQueueHasBeenApproved = "The requested product queue has been already approved.";
    }
}
