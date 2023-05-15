using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            using var context = new MyDBContext();
            return context.Categories.ToList();
        }

        public static Category? GetCategoryById(int id)
        {
            using var context = new MyDBContext();
            return context.Categories.SingleOrDefault(c => c.CategoryId == id);
        }
    }
}
