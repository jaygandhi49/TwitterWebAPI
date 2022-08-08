using Microsoft.EntityFrameworkCore;
using TwitterWebAPI.Models;

namespace TwitterWebAPI.Data
{
    public class TweetDbContext : DbContext
    {
        public TweetDbContext(DbContextOptions<TweetDbContext> options): base(options)
        {

        }
        public DbSet<UserDetails> Users { get; set; }
        public DbSet<TweetMaster> TweetMasters { get; set; }
        public DbSet<TweetAction> TweetLikes { get; set; }
        public DbSet<TweetComment> TweetComments { get; set; }
    }
}
