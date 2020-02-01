namespace SULS.Data
{
    using Microsoft.EntityFrameworkCore;
    using SULS.Models;

    public class SULSContext : DbContext
    {
        public SULSContext(DbContextOptions options)
             : base(options)
        {
        }

        public SULSContext()
        {
        }

        public DbSet<Problem> Problems { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Submission> Submissions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-JH4M4M9\MSSQLSERVER01;Database=SULS;Integrated Security=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}