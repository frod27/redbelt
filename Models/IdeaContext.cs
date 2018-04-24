using Microsoft.EntityFrameworkCore;
using redbelt.Models;

namespace redbelt.Models
{
    public class IdeaContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public IdeaContext(DbContextOptions<IdeaContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Idea> ideas { get; set; }
        public DbSet<Participant> participants { get; set; }
    }
}