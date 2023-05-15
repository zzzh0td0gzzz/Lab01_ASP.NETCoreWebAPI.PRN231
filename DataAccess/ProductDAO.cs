using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            using var context = new MyDBContext();
            return context.Products.ToList();
        }

        public static Product? FindProductById(int id)
        {
            using var context = new MyDBContext();
            return context.Products.SingleOrDefault(p => p.ProductId == id);
        }

        public static void SaveProduct(Product product)
        {
            using var context = new MyDBContext();
            context.Products.Add(product);
            context.SaveChanges();
        }

        public static void UpdateProduct(Product product)
        {
            using var context = new MyDBContext();
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
        }

        public static void DeleteProduct(int id)
        {
            var product = FindProductById(id);
            if (product == null) return;
            using var context = new MyDBContext();
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }
}
