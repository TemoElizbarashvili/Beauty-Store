using Beauty.Shared.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Beauty.DAL.Context
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            StoreDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if(!context.Products.Any())
            {
                context.Products.AddRange(new Product
                {
                    Name = "Dominas Dark Spot Control Cream",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent luctus.",
                    Price = 37.50m,
                    ImgUrl = "https://cdn.shopify.com/s/files/1/0476/1799/9004/products/timely_product_bn_templet_1800_TG_cream_1.jpg?v=1682013933&width=600"
                },
                new Product
                {
                    Name = "Dominas Dark Spot Control Multi Balm",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent luctus.",
                    Price = 26.25m,
                    ImgUrl = "https://cdn.shopify.com/s/files/1/0476/1799/9004/products/timely_product_bn_templet_1800_TG_multi_balm_1.jpg?v=1682013945&width=600"
                },
                new Product
                {
                    Name = "Dark Spot Treatment Ampoule",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent luctus.",
                    Price = 41.25m,
                    ImgUrl = "https://cdn.shopify.com/s/files/1/0476/1799/9004/products/timely_product_bn_templet_1800_TG_ampoule.jpg?v=1682016022&width=600"
                },
                new Product
                {
                    Name = "Dark Spot Control Ultimate SET",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent luctus.",
                    Price = 78.75m,
                    ImgUrl = "https://cdn.shopify.com/s/files/1/0476/1799/9004/files/TG55000SET_001.jpg?v=1682964064&width=600"
                },
                new Product
                {
                    Name = "Cover Supreme Rich Essence Setting Powder",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent luctus.",
                    Price = 32.80m,
                    ImgUrl = "https://cdn.shopify.com/s/files/1/0476/1799/9004/products/IK51333176_003.jpg?v=1671820058&width=600"
                },
                new Product
                {
                    Name = "Ultimate Cover Cushion Compact + 2 Refills",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent luctus.",
                    Price = 32.80m,
                    ImgUrl = "https://cdn.shopify.com/s/files/1/0476/1799/9004/products/OH51599497_001.jpg?v=1665598129&width=600"
                }
                );
                context.SaveChanges();
            }
        }
    }
}
