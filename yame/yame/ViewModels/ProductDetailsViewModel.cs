using yame.Models;

namespace yame.ViewModels
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Product> RandomProducts { get; set; }
    }
}
