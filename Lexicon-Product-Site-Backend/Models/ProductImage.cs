using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lexicon_Product_Site_Backend.Models
{
    public class ProductImage
    {
        [Key]
        public int ImageID { get; set; }
        [ForeignKey(nameof(ProductID))]
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string AltDescription { get; set; }
    }
}
