using Lexicon_Product_Site_Backend.Models;

namespace Lexicon_Product_Site_Backend.Requests
{
    public class NewProductImage
    {
        public int ProductID { get; set; }
        public string AltDescription { get; set; } = "image";
        public bool IsThumbnail { get; set; } = false;
    }
}