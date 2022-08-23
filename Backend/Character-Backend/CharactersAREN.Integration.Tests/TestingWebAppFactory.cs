using CharactersAREN.Integration.Tests.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Model.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CharactersAREN.Integration.Tests
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<CharacterContext>));

                services.Remove(descriptor);

                services.AddDbContext<CharacterContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<CharacterContext>();

                    db.Database.EnsureCreated();
                }
            });
            builder.ConfigureTestServices(ConfigureServices);

        }

        public HttpClient GetClient()
        {
            var client = WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(async services =>
                {
                    var serviceProvider = services.BuildServiceProvider();

                    using var scope = serviceProvider.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices
                        .GetRequiredService<CharacterContext>();

                    try
                    {
                        await DatabaseSeeder.ResetDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                });
            })
               .CreateClient();

            return client;
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "http://localhost";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = false
                };

                var config = new OpenIdConnectConfiguration()
                {
                    Issuer = MockJwtTokens.Issuer
                };


                config.SigningKeys.Add(MockJwtTokens.SecurityKey);
                options.Configuration = config;
            });
        }
    }
}
