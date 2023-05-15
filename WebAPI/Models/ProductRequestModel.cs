using BusinessObjects;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class ProductRequestModel
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int UnitsInStock { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}
