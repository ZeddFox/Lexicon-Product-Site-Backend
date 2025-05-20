using System.ComponentModel.DataAnnotations;

namespace Lexicon_Product_Site_Backend.Models
{
    public class Product
    {
        [Key]
        public int? ProductID { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public List<ProductImage>? Images { get; set; }
        public bool Enabled { get; set; } = false;
    }
}