using System.ComponentModel.DataAnnotations;

namespace Lexicon_Product_Site_Backend.Models
{
    public class User
    {
        [Key]
        public int? UserID { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}
