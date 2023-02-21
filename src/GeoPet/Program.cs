using GeoPet.Interfaces.Repository;
using GeoPet.Interfaces.Services;
using GeoPet.Mapper;
using GeoPet.Repository;
using GeoPet.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<GeoPetContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlserver"));
});

// builder.Services.AddScoped<IRepositoryBase<>, RepositoryBase<>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IUserService, PetParentService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<ISecurityServices, SecurityServices>();


builder.Services.AddHttpClient<ICepService, CepService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Mapper));

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
