using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yame.Data;
using yame.Models;

namespace yame.Controllers
{
    public class CartController : Controller
    {

        private readonly AppDbContext _context;
        private readonly UserManager<Users> _userManager;

        public CartController(AppDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Action Index để hiển thị giỏ hàng
        public async Task<IActionResult> Index()
        {
            // Lấy userId từ Identity
            var userId = _userManager.GetUserId(User);

            // Lấy giỏ hàng của người dùng từ database
            var cart = await _context.Cart.Include(c => c.Items)
                                           .ThenInclude(i => i.Product)
                                           .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return View("EmptyCart"); // Trả về view giỏ hàng trống nếu không có giỏ hàng
            }

            return View(cart); // Trả về giỏ hàng nếu có
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            // Lấy userId từ Identity
            var userId = _userManager.GetUserId(User);

            // Kiểm tra giỏ hàng của người dùng
            var cart = await _context.Cart.Include(c => c.Items).FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId, CreatedDate = DateTime.Now };
                _context.Cart.Add(cart);
                await _context.SaveChangesAsync();
            }

            // Giả sử sản phẩm có sẵn trong cơ sở dữ liệu
            var product = await _context.Product.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            // Kiểm tra xem sản phẩm đã có trong giỏ chưa
            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (cartItem != null)
            {
                // Nếu sản phẩm đã có trong giỏ, cập nhật số lượng
                cartItem.Quantity += quantity;
            }
            else
            {
                // Nếu chưa có, thêm mới sản phẩm vào giỏ
                cart.Items.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    UnitPrice = product.Price,
                    Quantity = quantity
                });
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            // Quay lại trang giỏ hàng
            return RedirectToAction("Index", "Cart");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            // Lấy userId từ Identity
            var userId = _userManager.GetUserId(User);

            // Lấy giỏ hàng của người dùng
            var cart = await _context.Cart.Include(c => c.Items)
                                           .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return NotFound(); // Không tìm thấy giỏ hàng
            }

            // Tìm sản phẩm trong giỏ hàng
            var cartItem = cart.Items.FirstOrDefault(i => i.Id == cartItemId);
            if (cartItem == null)
            {
                return NotFound(); // Không tìm thấy sản phẩm
            }

            // Xóa sản phẩm khỏi giỏ hàng
            cart.Items.Remove(cartItem);

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            // Quay lại trang giỏ hàng
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ClearCart()
        {  // Lấy userId từ Identity
            var userId = _userManager.GetUserId(User);

            // Lấy giỏ hàng của người dùng
            var cart = await _context.Cart.Include(c => c.Items)
                                           .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                // Xóa tất cả các sản phẩm trong giỏ hàng
                _context.CartItem.RemoveRange(cart.Items);

                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();
            }

            // Chuyển hướng về trang giỏ hàng
            return RedirectToAction("Index");
        }

    }


}
