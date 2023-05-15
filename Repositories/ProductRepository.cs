using BusinessObjects;
using DataAccess;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(int id)
        {
            ProductDAO.DeleteProduct(id);
        }

        public List<Category> GetCategories()
        {
            return CategoryDAO.GetCategories();
        }

        public Product? GetProductById(int id)
        {
            return ProductDAO.FindProductById(id);
        }

        public List<Product> GetProducts()
        {
            return ProductDAO.GetProducts();
        }

        public void SaveProduct(Product p)
        {
            ProductDAO.SaveProduct(p);
        }

        public void UpdateProduct(Product p)
        {
            ProductDAO.UpdateProduct(p);
        }

        public Category? GetCategoryById(int id)
        {
            return CategoryDAO.GetCategoryById(id);
        }
    }
}
