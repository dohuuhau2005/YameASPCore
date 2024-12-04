namespace yame.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        // Các trường ảnh
        public string Image0 { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        // Trường lưu thông tin về Brand (Trang)
        public string PageName { get; set; }
        public ICollection<Product> RandomProducts { get; set; }
    }
}
