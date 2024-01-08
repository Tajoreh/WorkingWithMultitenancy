using Microsoft.EntityFrameworkCore;

namespace SampleMultitenantAutofac.Repository;

public class TestDbContext : DbContext
{
    public DbSet<Person> People { get; set; }

    public TestDbContext(DbContextOptions options):base(options)
    {
        
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(x =>
        {
            x.ToTable("People");
            x.HasKey("Id");
            x.Property(p => p.Id).ValueGeneratedNever();
            x.Property(p => p.Name);
        });
        base.OnModelCreating(modelBuilder);
    }
}

public class Person
{
    public long Id { get; set; }
    public string Name { get; set; }

    protected Person()
    {
    }

    public Person(long id,string name)
    {
        Id = id;
        Name = name;
    }
}