using Microsoft.EntityFrameworkCore;
using Simple_CRUD.Models;
using Simple_CRUD.ViewModels;

namespace Simple_CRUD.Services
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<VMProduct> Create(VMProduct order)
        {
            try
            {
                if (order.Id == null || order.Id == 0)
                {
                    Product product = new Product
                    {
                        Name = order.Name,
                        Description = order.Description,
                        Price = order.Price,
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.Products.Add(product);
                }
                else
                {
                    Product existingData = await _context.Products.FirstOrDefaultAsync(x => x.Id == order.Id);
                    
                    if (existingData == null)
                    {
                        throw new InvalidOperationException("Product not found");
                    }

                    existingData.Name = order.Name;
                    existingData.Description = order.Description;
                    existingData.Price = order.Price;
                }

                await _context.SaveChangesAsync();

                return order;
            }
            catch
            {
                
                throw;
            }
        }

        public async Task<List<VMProduct>> GetAll(string? search, decimal? minPrice, decimal? maxPrice, int cursorId = 0)
        {
            var products = await _context.Database
                .SqlQuery<VMProduct>($"""
                    EXEC GetProducts
                       @Keyword = {search},
                       @MinPrice = {minPrice},
                       @MaxPrice = {maxPrice},
                       @CursorId = {cursorId}
                    """)
                .ToListAsync();

            return products;
        }

        public async Task<bool> Delete(int id)
        {
            var data = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                return false;
            }

            try
            {
                _context.Products.Remove(data);

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
