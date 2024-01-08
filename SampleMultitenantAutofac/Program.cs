using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SampleMultitenantAutofac.Configs;
using SampleMultitenantAutofac.Repository;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DBConnection");


//Add services to the container.
//builder.Services.AddDbContext<TestDbContext>(options =>
//    options.UseSqlServer(connectionString)
//        );


//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
//    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
//    {
//        containerBuilder.RegisterModule(new RegisterModule(connectionString));
//    });

builder.Host
    .UseServiceProviderFactory(x=>new AutofacMultitenantServiceProviderFactory(MyMultitenantContainer.Setup))
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new RegisterModule(connectionString));
    });






builder.Services
    .AddAutofacMultitenantRequestServices()
    .AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
