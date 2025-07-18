using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Data
{
    public class TaskContext(DbContextOptions<TaskContext> options) : DbContext(options)
    {

        // 2. DbSets - Representam tabelas no banco
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Category> Categories { get; set; }

        // 3. OnModelCreating - Apenas para Seed Data e configurações específicas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Dados iniciais (Seed Data)
            SeedData(modelBuilder);

            // Configurações que não podem ser feitas via Data Annotations
            ConfigureRelationships(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        // 4. Seed Data - Dados iniciais para desenvolvimento
        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Trabalho",
                    Description = "Tarefas relacionadas ao trabalho",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Category
                {
                    Id = 2,
                    Name = "Pessoal",
                    Description = "Tarefas pessoais",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Category
                {
                    Id = 3,
                    Name = "Estudos",
                    Description = "Tarefas de estudo e aprendizado",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );
        }

        // 5. Configurações específicas que só podem ser feitas via Fluent API
        private static void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            // Relacionamento com Delete Behavior específico
            modelBuilder.Entity<Activity>()
                .HasOne(t => t.Category)
                .WithMany(c => c.TasksList)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Índices para performance (opcional)
            modelBuilder.Entity<Activity>()
                .HasIndex(t => t.Status);

            modelBuilder.Entity<Activity>()
                .HasIndex(t => t.Priority);

            modelBuilder.Entity<Activity>()
                .HasIndex(t => t.DueDate);
        }

        // 6. SaveChanges Override - Intercepta operações de salvamento
        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        // 7. SaveChangesAsync Override - Versão assíncrona
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        // 8. UpdateTimestamps - Atualiza automaticamente Created/Updated
        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Task || e.Entity is Category)
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                }
                entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
            }
        }
    }
}