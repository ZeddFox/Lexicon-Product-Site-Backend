using System.Collections.Generic;

namespace Lexicon_Product_Site_Backend
{
    public class ProductDB : DbContext
    {
        public ProductDB(DbContextOptions<ProductDB> options) : base(options) { }

        public DbSet<Models.Product> Products { get; set; }
    }
}