using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _dbContext.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _dbContext.Products
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .ToListAsync();
        }

        public async Task<IReadOnlyList<Brand>> GetBrandsAsync()
        {
            return await _dbContext.Brands.ToListAsync();
        }

        public async Task<IReadOnlyList<Category>> GetCategoriesAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }
    }
}