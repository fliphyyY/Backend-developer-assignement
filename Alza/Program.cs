using Application.CollectionGateways;
using Application.ProductContext;
using Domain.ICollectionGateway;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:Database") ??
                       throw new InvalidOperationException();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductContext, ProductContext>();
builder.Services.AddScoped<IProductCollectionGateway, ProductCollectionGateway>();

var app = builder.Build();

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlServer(connectionString);

if (app.Environment.IsDevelopment())
{
    using (var context = new AppDbContext(optionsBuilder.Options))
    {
        if (!context.Database.CanConnect())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }

    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
