using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using StockExchange.Domain;

namespace StockExchange.Data
{
  public class StockContext : DbContext {

    public StockContext() : base("name=DefaultConnection") {
    }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ActiveStock> ActiveStocks { get; set; }
        public DbSet<UserStock> UserStocks { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("sm");

            //Set default string size in db if nothing is specified
            modelBuilder.Properties<string>()
            .Configure(p => p.HasMaxLength(256));

            modelBuilder.Entity<UserStock>()
            .Property(s => s.CompanyId)
            .IsRequired()
            .HasColumnAnnotation(
            IndexAnnotation.AnnotationName,
            new IndexAnnotation(
            new IndexAttribute("IX_CompantUser", 1) { IsUnique = true }));

            modelBuilder.Entity<UserStock>()
            .Property(s => s.UserName)
            .IsRequired()
            .HasColumnAnnotation(
            IndexAnnotation.AnnotationName,
            new IndexAnnotation(
            new IndexAttribute("IX_CompantUser", 2) { IsUnique = true }));



            modelBuilder.Entity<Company>()
            .Property(s => s.CompanyName)
            .HasMaxLength(500)
            .HasColumnAnnotation(
            IndexAnnotation.AnnotationName,
            new IndexAnnotation(
            new IndexAttribute("IX_Name", 1) { IsUnique = true }));

            modelBuilder.Entity<Company>()
            .Property(s => s.TicketSymbol)
            .HasMaxLength(5)
            .HasColumnAnnotation(
            IndexAnnotation.AnnotationName,
            new IndexAnnotation(
            new IndexAttribute("IX_TicketSymbol", 1) { IsUnique = true }));


            modelBuilder.Entity<ActiveStock>()
            .Property(s => s.CompanyId)
            .IsRequired()
            .HasColumnAnnotation(
            IndexAnnotation.AnnotationName,
            new IndexAnnotation(
            new IndexAttribute("IX_ActiveStock", 1) { IsUnique = true }));

            //Disable cascade delete set by default on related items
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}