namespace Lexicon_Product_Site_Backend
{
    public class UserDB : DbContext
    {
        public UserDB(DbContextOptions<UserDB> options) : base(options) { }

        public DbSet<Models.User> Users { get; set; }
    }
}
