using Microsoft.EntityFrameworkCore;
using OrderWebApplication.Entity.Models;
using OrderWebApplication.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Services.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Product>> GetAllProducts(ProductSearchViewModel productSearchViewModel)
        {
            try
            {
                List<Product> products = await context.Products.ToListAsync();
                if (productSearchViewModel != null)
                {
                    if (!string.IsNullOrEmpty(productSearchViewModel.productType))
                    {
                        products = products.Where(m => m.ProductType == productSearchViewModel.productType).ToList();
                    }
                    if (!string.IsNullOrEmpty(productSearchViewModel.productName))
                    {
                        products = products.Where(m => m.ProductName.Contains(productSearchViewModel.productName, StringComparison.OrdinalIgnoreCase)).ToList();
                    }
                    if (!string.IsNullOrEmpty(productSearchViewModel.productStock))
                    {
                        products = products.Where(m => m.Avaliable < Convert.ToInt32(productSearchViewModel.productStock)).ToList();
                    }
                    if (!string.IsNullOrEmpty(productSearchViewModel.Unit))
                    {
                        products = products.Where(m => m.Unit == productSearchViewModel.Unit).ToList();
                    }
                }

                return products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Product GetProductById(int id)
        {
            try
            {
                return context.Products.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateProduct(Product product)
        {
            try
            {
                context.Products.Update(product);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddProduct(Product product)
        {
            try
            {
                context.Add(product);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                var product = context.Products.Find(id);
                context.Products.Remove(product);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
