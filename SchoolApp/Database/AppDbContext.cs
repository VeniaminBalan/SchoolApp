using Microsoft.EntityFrameworkCore;
using SchoolApp.Features.Assignments.Models;

namespace SchoolApp.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<AssignmentModel> Assignments { get; set;}
    public DbSet<SubjectModel> Subjects { get; set;}
    public DbSet<TestModel> Tests { get; set;}

    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssignmentModel>()
            .HasOne(s=>s.Subject)
            .WithOne()
    }*/
}