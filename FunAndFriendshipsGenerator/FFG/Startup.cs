using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FFG.Services;
using Data.Persistance;
using Data.Core.Domain;
using Data.Core;
using Business;
using Data.Core.Interfaces;
using Business.Repository;
using FFG.Seeder;
using FFG.UserContext;
using Microsoft.AspNetCore.Http;

namespace FFG
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
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            // Add application services.

            services.AddMvc();
            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<DbSeeder>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();  
            services.AddTransient<IActivityRepository, ActivityRepository>();
            services.AddTransient<IActivityTypeRepository, ActivityTypeRepository>();
            services.AddTransient<IRatingRepository, RatingRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IUserActivityRepository, UserActivityRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddSingleton<IUserContext, UserContext.UserContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DbSeeder dbSeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<DatabaseContext>();

                dbContext.Database.Migrate();

                dbSeeder.SeedAsync().Wait();
            }
        }
    }
}
