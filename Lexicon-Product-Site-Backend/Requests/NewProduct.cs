using Lexicon_Product_Site_Backend.Models;

namespace Lexicon_Product_Site_Backend.Requests
{
    public class NewProduct
    {
        public string? Name { get; set; }
        public decimal Price { get; set; } = 999999m;
        public int Amount { get; set; } = 0;
        public string? Category { get; set; }
        public string? Description { get; set; }
    }
}