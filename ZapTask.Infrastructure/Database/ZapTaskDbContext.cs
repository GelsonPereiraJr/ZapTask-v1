
using System.Collections.Generic;
using System.Reflection.Emit;
using ZapTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;


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
                entity.Property(e => e.Status).HasDefaultValue("Pendente");
                entity.Property(e => e.Tentativas).HasDefaultValue(0);
            });
        }
    }
}