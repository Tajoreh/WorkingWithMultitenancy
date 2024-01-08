using Autofac;
using Autofac.Multitenant;
using Microsoft.EntityFrameworkCore;
using SampleMultitenantAutofac.Repository;

namespace SampleMultitenantAutofac.Configs;

public static class MyMultitenantContainer
{
    public static MultitenantContainer Setup(IContainer container)
    {

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: false)
            .Build();


        var womenOptions = new DbContextOptionsBuilder().UseSqlServer(configuration.GetConnectionString("WomenDbConnection"));

        var menOptions = new DbContextOptionsBuilder().UseSqlServer(configuration.GetConnectionString("MenDbConnection"));


        var strategy = new HeaderStrategy(container.Resolve<IHttpContextAccessor>());

        var multitenantContainer = new MultitenantContainer(strategy, container);

        multitenantContainer.ConfigureTenant("women", cb => cb.Register(x => new TestDbContext(womenOptions.Options))
            .InstancePerLifetimeScope());

        multitenantContainer.ConfigureTenant("men", cb => cb.Register(x => new TestDbContext(menOptions.Options))
            .InstancePerLifetimeScope());

        return multitenantContainer;
    }

    
}