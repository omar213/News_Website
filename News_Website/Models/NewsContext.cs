using Microsoft.EntityFrameworkCore;

namespace News_Website.Models
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options)
            : base(options)
        {
        }

        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Contactus> Contacts { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }



    }
}
