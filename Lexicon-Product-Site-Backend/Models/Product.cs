using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lexicon_Product_Site_Backend.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; } = 0;
        public string Category { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(ImageIDs))]
        public List<int> ImageIDs { get; set; } = new List<int>();
        public List<ProductImage> Images { get; set; } = new List<ProductImage>();
        public bool Enabled { get; set; } = false;
    }
}