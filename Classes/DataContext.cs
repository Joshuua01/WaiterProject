using Microsoft.EntityFrameworkCore;
using WaiterProject.Classes;

namespace WaiterProject
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WaiterProject;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>()
                .HasMaxLength(50);

            modelBuilder.Entity<MenuItemType>()
                .HasIndex(m => m.Name)
                .IsUnique();

            modelBuilder.Entity<MenuItem>()
                .HasIndex(m => m.Name)
                .IsUnique();

            modelBuilder.Entity<MenuItem>()
                .HasOne<MenuItemType>(m => m.MenuItemType)
                .WithMany(m => m.MenuItems)
                .HasForeignKey(mi => mi.MenuItemTypeId);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<MenuItemType> MenuItemTypes { get; set; }
    }
}