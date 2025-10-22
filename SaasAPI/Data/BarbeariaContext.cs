using Microsoft.EntityFrameworkCore;
using SaasAPI.Models;


namespace BarbeariaAPI.Data
{

    public class BarbeariaContext : DbContext
    {

        public BarbeariaContext(DbContextOptions<BarbeariaContext> options) : base(options) { }

        public DbSet<Barbearia> Barbearia { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Barbeiro> Barbeiro { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agendamento>()
            .Property(a => a.Status)
            .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .EnableSensitiveDataLogging()
                    .LogTo(Console.Write, LogLevel.Information);
            }

        }


    }
}

