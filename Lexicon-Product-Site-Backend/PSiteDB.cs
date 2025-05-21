using System.Collections.Generic;

namespace Lexicon_Product_Site_Backend
{
    public class PSiteDB : DbContext
    {
        public PSiteDB() { }

        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Product> Products { get; set; }
        public DBSet<Models.ProductImage> ProductImages { get; set; }
    }
}
