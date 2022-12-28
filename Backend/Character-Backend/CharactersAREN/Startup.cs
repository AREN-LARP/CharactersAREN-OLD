using DAL.DALInterfaces;
using DAL.Repositories;
using Logic.Logic;
using Logic.LogicInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model.Interfaces;
using Model.Models;
using Newtonsoft.Json;

namespace CharactersAREN
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ICharacterRepository, CharacterRepository>();
            services.AddTransient<ICharacterLogic, CharacterLogic>();
            services.AddTransient<ICareerLogic, CareerLogic>();
            services.AddTransient<ICareerRepository, CareerRepository>();
            services.AddTransient<ISkillRepository, SkillRepository>();
            services.AddTransient<ISkillLogic, SkillLogic>();
            services.AddTransient<ISkillCategoryRepository, SkillCategoryRepository>();
            services.AddTransient<ISkillCategoryLogic, SkillCategoryLogic>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IEventLogic, EventLogic>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserLogic, UserLogic>();
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IItemLogic, ItemLogic>();
            services.AddTransient<IItemGroupRepository, ItemGroupRepository>();
            services.AddTransient<IItemGroupLogic, ItemGroupLogic>();
            services.AddTransient<IFactionLootRepository, FactionLootRepository>();
            services.AddTransient<IFactionLootLogic, FactionLootLogic>();
            services.AddTransient<ILootProbabilityRepository, LootProbabilityRepository>();
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            });
            services.AddDbContext<CharacterContext>(op => op.UseSqlServer(Configuration.GetConnectionString("acdb")));
            // Register the service and implementation for the database context
            services.AddScoped<ICharacterContext>(provider => provider.GetService<CharacterContext>());
            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyAllowSpecificOrigins",
                                  builder =>
                                  {
                                      builder.WithOrigins("https://localhost:3000",
                                                          "http://localhost:3000",
                                                          "https://charactersaren-admin.azurewebsites.net",
                                                          "http://charactersaren-admin.azurewebsites.net",
                                                          "https://charactersaren-player.azurewebsites.net",
                                                          "https://charactersaren-player.azurewebsites.net").AllowAnyHeader().AllowAnyMethod();
                                  });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://dev-8yft46n8.eu.auth0.com/";
                options.Audience = "https://characters-aren.azurewebsites.net";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("WriteAccess", policy =>
                                  policy.RequireClaim("permissions", "create:character", "update:character", "create:event", "update:event", "create:skill", "update:skill"));
                options.AddPolicy("ReadAccess", policy =>
                                  policy.RequireClaim("permissions", "read:character", "read:event", "read:skill", "read: personalcharacter"));
                options.AddPolicy("DeleteAccess", policy =>
                                  policy.RequireClaim("permissions", "delete:character", "delete:event", "delete:skill"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyAllowSpecificOrigins");

            // 2. Enable authentication middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
