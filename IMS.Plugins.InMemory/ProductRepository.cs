﻿using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.InMemory
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>()
            {
                new Product() { ProductId = 1, ProductName = "Bike", Quantity = 10, Price = 150},
                new Product() { ProductId = 2, ProductName = "Car", Quantity = 10, Price = 25000}
            };
        }

        public Task AddProductAsync(Product product)
        {
            if(_products.Any(x => x.ProductName.Equals(product.ProductName,StringComparison.OrdinalIgnoreCase)))
            {
                return Task.CompletedTask;
            }

            var maxId = _products.Max(x => x.ProductId);
            product.ProductId = maxId + 1;

            _products.Add(product);
            return Task.CompletedTask;
        }

        public Task DeleteProductByIdAsync(int productId)
        {
            var product = _products.First(x => x.ProductId == productId);
            if (product != null)
            {
                _products.Remove(product);
            }

            return Task.CompletedTask;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await Task.FromResult(_products.First(x => x.ProductId == productId));
        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return await Task.FromResult(_products);

            return _products.Where(x => x.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public Task UpdateProductAsync(Product product)
        {
            if(_products.Any(x => x.ProductId != product.ProductId && x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.CompletedTask;
            }

            var productToUpdate = _products.FirstOrDefault(x => x.ProductId == product.ProductId);
            if (productToUpdate is not null)
            {
                productToUpdate.ProductName = product.ProductName;
                productToUpdate.Quantity = product.Quantity;
                productToUpdate.Price = product.Price;
            }

            return Task.CompletedTask;
        }
    }
}
