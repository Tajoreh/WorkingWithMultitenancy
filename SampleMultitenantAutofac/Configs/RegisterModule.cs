using Autofac;
using Microsoft.EntityFrameworkCore;
using SampleMultitenantAutofac.Repository;

namespace SampleMultitenantAutofac.Configs;

public class RegisterModule : Module
{
    private readonly string _connectionString;

    public RegisterModule(string connectionString)
    {
        _connectionString = connectionString;
    }
    protected override void Load(ContainerBuilder builder)
    {

        var womenOptions = new DbContextOptionsBuilder().UseSqlServer(_connectionString);
        builder.Register(x => new TestDbContext(womenOptions.Options));
    }


}