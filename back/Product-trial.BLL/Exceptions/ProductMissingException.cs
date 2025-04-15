namespace Product_trial.BLL.Exceptions
{
    public class ProductMissingException : Exception
    {
        public ProductMissingException() : base("Product is missing")
        {
        }

        public ProductMissingException(int productId)
            : base($"The product with ID '{productId}' was not found")
        {
        }

        public ProductMissingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
