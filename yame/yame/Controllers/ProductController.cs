using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using yame.Data;
using yame.Models;

public class ProductController : Controller
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    // Hiển thị danh sách sản phẩm
    public async Task<IActionResult> Index(string brand)
    {

        var products = await _context.Product.ToListAsync();
        // Lọc theo Brand nếu có
        if (!string.IsNullOrEmpty(brand))
        {
            products = (List<Product>)products.Where(p => p.PageName == brand);
        }
        return View(products);
    }

    // Xem chi tiết sản phẩm
    public async Task<IActionResult> Details(int id)
    {
        var product = await _context.Product.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        // Lấy 4 sản phẩm ngẫu nhiên, loại trừ sản phẩm hiện tại
        var randomProducts = await _context.Product
            .Where(p => p.Id != id)
            .OrderBy(r => Guid.NewGuid()) // Sắp xếp ngẫu nhiên
            .Take(4) // Lấy 4 sản phẩm
            .ToListAsync();

        product.RandomProducts = randomProducts;
        return View(product);
    }
    // Hiển thị form tạo mới sản phẩm
    //get
    public IActionResult Create()
    {
        return View();
    }

    // Xử lý tạo sản phẩm mới
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(NonBranded));
        }
        return View(product);
    }

    // Hiển thị form chỉnh sửa sản phẩm
    //get
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _context.Product.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    // Xử lý chỉnh sửa sản phẩm
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(NonBranded));
        }
        return View(product);
    }



    // Hiển thị form xác nhận xóa sản phẩm
    //Get
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Product.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    // Xử lý xóa sản phẩm
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Product.FindAsync(id);
        if (product != null)
        {
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
        }


        return RedirectToAction(nameof(NonBranded));
    }

    private bool ProductExists(int id)
    {
        return _context.Product.Any(e => e.Id == id);
    }


    //page
    public async Task<IActionResult> NonBranded()
    {
        // Lọc sản phẩm có thương hiệu "Non Brand"
        var nonBrandedProducts = await _context.Product
            .Where(p => p.PageName == "NoneBranded")
            .ToListAsync();

        return View(nonBrandedProducts);  // Truyền danh sách sản phẩm vào View
    }
    public async Task<IActionResult> AoThunCoHinh()
    {
        // Lọc sản phẩm có thương hiệu "Non Brand"
        var nonBrandedProducts1 = await _context.Product
            .Where(p => p.PageName == "AoThunCoHinh")
            .ToListAsync();

        return View(nonBrandedProducts1);  // Truyền danh sách sản phẩm vào View
    }
    public async Task<IActionResult> AoThunCoMau()
    {
        // Lọc sản phẩm có thương hiệu "Non Brand"
        var nonBrandedProducts2 = await _context.Product
            .Where(p => p.PageName == "AoThunCoMau")
            .ToListAsync();

        return View(nonBrandedProducts2);  // Truyền danh sách sản phẩm vào View
    }
    public async Task<IActionResult> AoThunTron()
    {
        // Lọc sản phẩm có thương hiệu "Non Brand"
        var nonBrandedProducts3 = await _context.Product
            .Where(p => p.PageName == "AoThunTron")
            .ToListAsync();

        return View(nonBrandedProducts3);  // Truyền danh sách sản phẩm vào View
    }
    public async Task<IActionResult> Moi()
    {
        // Lọc sản phẩm có thương hiệu "Non Brand"
        var nonBrandedProducts4 = await _context.Product
            .Where(p => p.PageName == "M?i")
            .ToListAsync();

        return View(nonBrandedProducts4);  // Truyền danh sách sản phẩm vào View
    }
    public async Task<IActionResult> NoStyle()
    {
        // Lọc sản phẩm có thương hiệu "Non Brand"
        var nonBrandedProducts5 = await _context.Product
            .Where(p => p.PageName == "NoStyle")
            .ToListAsync();

        return View(nonBrandedProducts5);  // Truyền danh sách sản phẩm vào View
    }
    public async Task<IActionResult> Phukiengiatot()
    {
        // Lọc sản phẩm có thương hiệu "Non Brand"
        var nonBrandedProducts6 = await _context.Product
            .Where(p => p.PageName == "Phukiengiatot")
            .ToListAsync();

        return View(nonBrandedProducts6);  // Truyền danh sách sản phẩm vào View
    }
    public async Task<IActionResult> SaleKetThucVongDoi()
    {
        // Lọc sản phẩm có thương hiệu "Non Brand"
        var nonBrandedProducts7 = await _context.Product
            .Where(p => p.PageName == "SaleKetThucVongDoi")
            .ToListAsync();

        return View(nonBrandedProducts7);  // Truyền danh sách sản phẩm vào View
    }
    public async Task<IActionResult> SeventySeven()
    {
        // Lọc sản phẩm có thương hiệu "Non Brand"
        var nonBrandedProducts8 = await _context.Product
            .Where(p => p.PageName == "SeventySeven")
            .ToListAsync();

        return View(nonBrandedProducts8);  // Truyền danh sách sản phẩm vào View
    }
    public async Task<IActionResult> ThoiTrangGiatot()
    {
        // Lọc sản phẩm có thương hiệu "Non Brand"
        var nonBrandedProducts9 = await _context.Product
            .Where(p => p.PageName == "ThoiTrangGiatot")
            .ToListAsync();

        return View(nonBrandedProducts9);  // Truyền danh sách sản phẩm vào View
    }
    // Hành động để hiển thị trang và xử lý tìm kiếm
    public async Task<IActionResult> SearchResults(string searchString, decimal? minPrice, decimal? maxPrice)
    {
        var products = from p in _context.Product
                       select p;

        // Tìm kiếm theo tên sản phẩm
        if (!string.IsNullOrEmpty(searchString))
        {
            products = products.Where(p => p.Name.Contains(searchString));
        }

        // Lọc theo giá (nếu có giá tối thiểu và tối đa)
        if (minPrice.HasValue)
        {
            products = products.Where(p => p.Price >= minPrice);
        }

        if (maxPrice.HasValue)
        {
            products = products.Where(p => p.Price <= maxPrice);
        }

        // Trả về kết quả tìm kiếm (hiển thị trên trang khác)
        return View(await products.ToListAsync());
    }
}
