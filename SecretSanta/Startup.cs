using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecretSanta.Areas.Identity;
using SecretSanta.Models;

namespace SecretSanta
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        private IHostEnvironment Environment { get; set; }
        public IServiceCollection MyService { get; set; }
        
        public DbContextOptionsBuilder ContextOptions { get; set; }

        public void PointSecretSantas()
        {
            var users = new List<User>();
            var usedUsers = new List<int>();
            var rand = new Random();
            using (var c = new SecretSantaContext(new DbContextOptions<SecretSantaContext>()))
            {
                Console.WriteLine("Starting the Secret Santa Selection");
                users = c.Users.ToList();

                var couple1 = users.Where(u => (u.UserId == 50 || u.UserId == 53)).ToList();
                Console.WriteLine(couple1.Count);
                var couple2 = users.Where(u => (u.UserId == 57 || u.UserId == 60)).ToList();
                Console.WriteLine(couple2.Count);
                var couple3 = users.Where(u => (u.UserId == 51 || u.UserId == 52)).ToList();
                Console.WriteLine(couple3.Count);

                // Couple 1
                while (new[]{-1,0,50,53}.Any(x => x == couple1[0].RecipientId)
                       || usedUsers.Any(x=> x == couple1[0].RecipientId))
                {
                    if (couple1[0].RecipientId == 0)
                        couple1[0].RecipientId = rand.Next(50, 61);
                }
                usedUsers.Add(couple1[0].RecipientId);
                while (new List<int>(){-1,0,50,53}.Any(x => x == couple1[1].RecipientId)
                       || usedUsers.Any(x=> x == couple1[1].RecipientId))
                {
                    if (couple1[1].RecipientId == 0)
                        couple1[1].RecipientId = rand.Next(50, 61);
                }
                usedUsers.Add(couple1[1].RecipientId);

                
                // Couple 2
                while (new[]{-1,0,57,60}.Any(x => x == couple2[0].RecipientId)
                       || usedUsers.Any(x=> x == couple2[0].RecipientId))
                {
                    if (couple2[0].RecipientId == 0)
                        couple2[0].RecipientId = rand.Next(50, 61);
                }
                usedUsers.Add(couple2[0].RecipientId);

                while (new[]{-1,0,57,60}.Any(x => x == couple2[1].RecipientId) 
                        || usedUsers.Any(x=> x == couple2[1].RecipientId))
                {
                    if (couple2[1].RecipientId == 0)
                        couple2[1].RecipientId = rand.Next(50, 61);
                }
                usedUsers.Add(couple2[1].RecipientId);

                
                // Couple 3
                while (new[]{-1,0,51,52}.Any(x => x == couple3[0].RecipientId)
                        && usedUsers.Any(x=> x == couple3[0].RecipientId))
                {
                    if (couple3[0].RecipientId == 0)
                        couple3[0].RecipientId = rand.Next(50, 61);
                }
                usedUsers.Add(couple3[0].RecipientId);

                while (new[]{-1,0,51,52}.Any(x => x == couple3[1].RecipientId)
                       && usedUsers.Any(x=> x == couple3[1].RecipientId))
                {
                    if (couple3[1].RecipientId == 0)
                        couple3[1].RecipientId = rand.Next(50, 61);
                }
                usedUsers.Add(couple3[1].RecipientId);


                var singleUsersLeft = c.Users.Where(u => u.RecipientId == 0);
                Console.WriteLine("Single users left");
                foreach (var user in singleUsersLeft)
                {
                    while (new[] {-1, 0}.Any(x => x == user.RecipientId)
                           && usedUsers.Any(x => x == user.RecipientId))
                    {
                        if (user.RecipientId == 0)
                            user.RecipientId = rand.Next(50, 61);
                    }
                }

                // 50 != 53
                // 57 != 60
                // 51 != 52
                Console.WriteLine(users.Count);
                Console.WriteLine(users[0].Email);

                Console.WriteLine("End Results");
                foreach (var user in users)
                {
                    Console.WriteLine(user.UserId + " " + user.RecipientId + " " + user.Email);
                }

                c.SaveChanges();
            }
        }
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            //PointSecretSantas();
            // Copied from https://stackoverflow.com/questions/61401282/how-to-read-windows-environment-variables-on-dotnet-core
            Environment = environment;
            Configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // var optionsBuilder = new DbContextOptionsBuilder<SecretSantaContext>();
            // optionsBuilder.UseSqlite(@"Data Source=SecretSantaDB.db");

            // ContextOptions = ContextOptions.UseSqlite(@"Data Source=SecretSantaDB.db");
            
            services.AddDbContext<SecretSantaContext>(options => options.UseSqlite(@"Data Source=SecretSantaDB.db"));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<SecretSantaContext>();
            services.AddRazorPages();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

             services.Configure<ForwardedHeadersOptions>(opt =>
             {
                 opt.KnownProxies.Add(IPAddress.Parse("127.0.0.1"));
             });
            
            services.AddServerSideBlazor();
            services
                .AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>
                >();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                //app.UseExceptionHandler("/Error");

                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });

                app.UseAuthentication();
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Not sure what this does
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}