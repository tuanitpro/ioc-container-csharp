namespace IoCContainer
{
    public class ProductService : EfService<Product>
    {
        private readonly ILogger _logger;

        public ProductService(ILogger logger)
        {
            _logger = logger;
        }

        public override string Add(Product item)
        {
            return "Add Product " + _logger.Write("successful!");
        }
    }
}