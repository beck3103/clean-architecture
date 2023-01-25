using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _dbContext.Add(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<Product> GetProductCategoryAsync(int? id)
        {
            return await _dbContext.Products.Include(c => c.Category).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> RemoveAsync(Product product)
        {
            _dbContext.Remove(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _dbContext.Remove(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}
