using Bussiness.Abstract;
using Bussiness.Concrete;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MyShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IBasketDal, EfBasketDal>();
builder.Services.AddScoped<IBasketService, BasketManager>();
builder.Services.AddScoped<IBasketLineDal, EfBasketLineDal>();
builder.Services.AddScoped<IBasketLineService, BasketLineManager>();
builder.Services.AddScoped<IHashingHelper, HashingHelper>();
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.
    ReferenceHandler = ReferenceHandler.IgnoreCycles
);

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
