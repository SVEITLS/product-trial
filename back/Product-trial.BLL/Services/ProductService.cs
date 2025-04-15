using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product_trial.BLL.Exceptions;
using Product_trial.DAL.Context;
using Product_trial.DAL.Models;

namespace Product_trial.BLL.Services
{
    /// <summary>
    /// Interface for product-related business logic.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieves all products from the database.
        /// </summary>
        /// <returns>A list of all products.</returns>
        Task<List<Product>> GetAllAsync();

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product if found; otherwise, null.</returns>
        Task<Product?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new product in the database.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product.</returns>
        Task<Product> CreateAsync(Product product);

        /// <summary>
        /// Updates an existing product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="updatedProduct">The new product data.</param>
        Task UpdateAsync(int id, Product updatedProduct);

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        Task DeleteAsync(int id);
    }

    /// <summary>
    /// Implementation of IProductService for managing products.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly ProductContext _context;

        /// <summary>
        /// Constructor for ProductService.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductService(ProductContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new ProductMissingException(id);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(int id, Product updatedProduct)
        {
            Product? existing = await _context.Products.FindAsync(id);
            if (existing == null)
                throw new ProductMissingException(id);

            existing.Name = updatedProduct.Name;
            existing.Description = updatedProduct.Description;
            existing.Price = updatedProduct.Price;
            existing.Quantity = updatedProduct.Quantity;
            existing.Category = updatedProduct.Category;
            existing.Code = updatedProduct.Code;
            existing.Image = updatedProduct.Image;
            existing.InternalReference = updatedProduct.InternalReference;
            existing.ShellId = updatedProduct.ShellId;
            existing.InventoryStatus = updatedProduct.InventoryStatus;
            existing.Rating = updatedProduct.Rating;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
