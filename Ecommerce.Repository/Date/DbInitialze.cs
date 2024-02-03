using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Date
{
    public static class DbInitialze
    {
        public static async Task Seed(StoreContext _context)
        {
            if (!_context.ProductBrands.Any())
            {
                var brandData = File.ReadAllText("../Ecommerce.Repository/DataSeeding/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                if (brands is not null && brands.Count > 0)
                {
                    foreach (var item in brands)
                    {
                        await _context.ProductBrands.AddAsync(item);

                    }
                    _context.Database.OpenConnection();
                    await _context.Database.ExecuteSqlRawAsync("Set Identity_Insert dbo.productbrands on; ");
                    await _context.SaveChangesAsync();
                   await _context.Database.ExecuteSqlRawAsync("set identity_insert dbo.productbrands off;");
                    _context.Database.CloseConnection();
                }
            }

            if (!_context.ProductTypes.Any())
            {
                var PTypeData = File.ReadAllText("../Ecommerce.Repository/DataSeeding/types.json");
                var Type = JsonSerializer.Deserialize<List<ProductType>>(PTypeData);
                if (Type is not null && Type.Count > 0)
                {
                    foreach (var item in Type)
                    {
                       await _context.ProductTypes.AddAsync(item);
                    }
                    _context.Database.OpenConnection();
                    await _context.Database.ExecuteSqlRawAsync("set identity_insert dbo.productTypes on;");
                    await _context.SaveChangesAsync();
                    await _context.Database.ExecuteSqlRawAsync("set identity_insert dbo.productTypes off;");
                    _context.Database.CloseConnection();
                }

            }

            if (!_context.Products.Any())
            {
                var productData = File.ReadAllText("../Ecommerce.Repository/DataSeeding/products.json");
                var products=JsonSerializer.Deserialize<List<Product>>(productData);
                if (products is not null && products.Count > 0)
                {
                    foreach (var item in products)
                    {
                        await _context.Products.AddAsync(item);
                    }
                }
                _context.Database.OpenConnection();
                await _context.Database.ExecuteSqlRawAsync("set identity_insert dbo.products on;");
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("set identity_insert dbo.products off;");
                _context.Database.CloseConnection();

            }

            if (!_context.DeliveryOrderMethods.Any())
            {
                var DeliveryMethodData = File.ReadAllText("../Ecommerce.Repository/DataSeeding/delivery.json");
                var Data =JsonSerializer.Deserialize<List<DeliveryOrderMethod>>(DeliveryMethodData);
                foreach (var item in Data)
                {
                   await _context.DeliveryOrderMethods.AddAsync(item);
                }
                _context.Database.OpenConnection();
                await _context.Database.ExecuteSqlRawAsync("set identity_insert dbo.DeliveryOrderMethods off");
                await _context.SaveChangesAsync();
                await _context.Database.ExecuteSqlRawAsync("set identity_insert dbo.DeliveryOrderMethods on");
                _context.Database.CloseConnection();
            }

        }
    }
}
