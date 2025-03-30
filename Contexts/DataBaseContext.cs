using Microsoft.EntityFrameworkCore;
using TPC_API.Models;

namespace TPC_API.Contexts
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        { 
        
        }

        public DbSet<User> Users { get; set; }  

        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureaUser(modelBuilder);
            Configureatarefa(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public void ConfigureaUser(ModelBuilder modelBuilder)
        {
            var userConfiguration = modelBuilder.Entity<User>().ToTable("Usuario");

            userConfiguration.HasKey(x => x.Id);
            userConfiguration.Property(x => x.Nome).IsRequired().HasMaxLength(250);
            userConfiguration.Property(x => x.Email).IsRequired().HasMaxLength(150);
            userConfiguration.HasMany(x => x.Tarefas).WithOne(x => x.User).HasForeignKey(x=> x.UsuarioId).OnDelete(DeleteBehavior.Cascade);

        }

        public void Configureatarefa(ModelBuilder modelBuilder)
        {
            var userConfiguration = modelBuilder.Entity<Tarefa>().ToTable("Tarefa");

            userConfiguration.HasKey(x => x.Id);
            userConfiguration.Property(x => x.Titulo).IsRequired().HasMaxLength(50);
            userConfiguration.Property(x => x.Descricao).IsRequired();
            userConfiguration.Property(x => x.Status).IsRequired();
            userConfiguration.Property(x => x.UsuarioId).IsRequired();
      
        }
    }
}
