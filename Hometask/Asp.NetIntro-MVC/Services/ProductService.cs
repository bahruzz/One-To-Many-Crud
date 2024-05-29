using Asp.NetIntro_MVC.Data;
using Asp.NetIntro_MVC.Models;
using Asp.NetIntro_MVC.Services.Interface;
using Asp.NetIntro_MVC.ViewModels.Products;
using Microsoft.EntityFrameworkCore;

namespace Asp.NetIntro_MVC.Services
{
    public class ProductService : IProductService
    {
       private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();


        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(m => m.Category)
                                         .Include(m => m.ProductImages)
                                         .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllPaginateAsync(int page, int take)
        {
            return await _context.Products.Include(m => m.Category)
                                          .Include(m => m.ProductImages)
                                          .Skip((page - 1) * take)
                                          .Take(take)
                                          .ToListAsync(); ;
        }

        public async Task<IEnumerable<Product>> GetAllWithImagesAsync()
        {
            return await _context.Products.Include(m => m.ProductImages).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> GetByIdWithAllDatasAsync(int id)
        {
            return await _context.Products.Where(m => m.Id == id)
                                          .Include(m => m.Category)
                                          .Include(m=>m.ProductImages)
                                          .FirstOrDefaultAsync();


        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public IEnumerable<ProductVM> GetMappedDatas(IEnumerable<Product> products)
        {
            return products.Select(m => new ProductVM
            {
                Id = m.Id,
                Name = m.Name,
                CategoryName = m.Category.Name,
                Price = m.Price,
                Description = m.Description,
                MainImage = m.ProductImages?.FirstOrDefault(m => m.IsMain)?.Name
            });
        }

        public async Task<ProductImage> GetProductImageByIdAsync(int id)
        {
            return await _context.ProductImages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task EditAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task ImageDeleteAsync(ProductImage image)
        {
            _context.ProductImages.Remove(image);
            await _context.SaveChangesAsync();
        }
    }
}
