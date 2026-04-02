using Cooperative.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Cooperative.Data
{
    public class CooperativeDbContext : DbContext
    {
        public CooperativeDbContext(DbContextOptions<CooperativeDbContext> options) : base(options) { }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<Executives> Executives { get; set; }
        public DbSet<Cooperator> Cooperators { get; set; }
    }
}
