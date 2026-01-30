
using ZapTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ZapTask.Domain.Enums;


namespace ZapTask.Infrastructure.Database
{
    public class ZapTaskDbContext : DbContext
    {
        public ZapTaskDbContext(DbContextOptions<ZapTaskDbContext> options) : base(options){ }

        public DbSet<Tarefa> Tarefas {get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tarefa>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired();
                entity.Property(e => e.Prazo).IsRequired();
                entity.Property(e => e.WhatsAppId).IsRequired();
                entity.Property(t => t.Status).HasConversion<int>() .HasDefaultValue(StatusTarefa.Pendente);
                entity.Property(e => e.Tentativas).HasDefaultValue(0);
                modelBuilder.Entity<Tarefa>().Property(t => t.ProximaTentativaEm);

            });
        }
    }
}