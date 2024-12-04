namespace yame.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Dùng để liên kết với người dùng
        public DateTime CreatedDate { get; set; }

        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();


    }
}
