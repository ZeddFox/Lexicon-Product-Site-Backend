using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lexicon_Product_Site_Backend
{
    public class PSiteDB : DbContext
    {
        public PSiteDB(DbContextOptions<PSiteDB> options) : base(options) { }

        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.ProductImage> ProductImages { get; set; }
    }
}
