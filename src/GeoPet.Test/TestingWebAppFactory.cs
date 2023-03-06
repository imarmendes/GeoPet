using GeoPet.Interfaces.Repository;
using GeoPet.Interfaces.Services;
using GeoPet.Repository;
using GeoPet.Service;
using GeoPet.Test;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GeoPet.Test;

public class TestingWebAppFactory<TProgram> : WebApplicationFactory<Program> where TProgram : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
          {
              var dbContextDescriptor = services.SingleOrDefault(
                  d => d.ServiceType ==
                      typeof(DbContextOptions<GeoPetContext>));

              if (dbContextDescriptor != null) services.Remove(dbContextDescriptor);


              services.AddDbContext<GeoPetContext>(options =>
              {
                  options.UseInMemoryDatabase("InMemoryDB");
              });

              services.AddScoped<IOwnerRepository, OwnerRepository>();
              services.AddScoped<IPetRepository, PetRepository>();
              services.AddScoped<IOwnerService, OwnerService>();
              services.AddScoped<IPetService, PetService>();
              services.AddScoped<ISecurityServices, SecurityServices>();
              services.AddScoped<IAuthenticationService, AuthenticationService>();
              services.AddScoped<IJwtService, JwtService>();
              services.AddScoped<IQRCodeService, QRCodeService>();
              services.AddScoped<INominatinService, NominatinService>();

              var sp = services.BuildServiceProvider();
              using var scope = sp.CreateScope();
              using var db = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

              db.Database.EnsureCreated();
              db.Database.EnsureDeleted();
              db.Database.EnsureCreated();

          });

    }
}