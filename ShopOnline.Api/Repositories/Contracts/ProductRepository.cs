using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Models;

namespace ShopOnline.Api.Repositories.Contracts
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDbContext _context;
        public ProductRepository(ShopOnlineDbContext context)
        {
            _context=context;
        }
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await _context.ProductCategories.ToListAsync();
            return categories;
        }

        public Task<ProductCategory> GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public Task<Product> GetItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}
