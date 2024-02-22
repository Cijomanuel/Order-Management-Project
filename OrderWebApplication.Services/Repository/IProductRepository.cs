using OrderWebApplication.Entity.Models;
using OrderWebApplication.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Services.Repository
{
    public interface IProductRepository
    {

        Task<List<Product>> GetAllProducts(ProductSearchViewModel productSearchViewModel);

        Product GetProductById(int id);

        void AddProduct(Product product);
        void UpdateProduct(Product product);

        void DeleteProduct(int id);
    }
}
