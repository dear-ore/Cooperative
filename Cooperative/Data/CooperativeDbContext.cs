using Cooperative.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Cooperative.Data
{
    public class CooperativeDbContext : DbContext
    {
        public CooperativeDbContext(DbContextOptions<CooperativeDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<Executive> Executives { get; set; }
        public DbSet<Cooperator> Cooperators { get; set; }
        public DbSet<Loan> Loans { get; set;  }
        public DbSet<Food> Food { get; set; }
        public DbSet<Souvenir> Souvenirs { get; set; }
        public DbSet<Repayment> Repayments { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
    }
}
