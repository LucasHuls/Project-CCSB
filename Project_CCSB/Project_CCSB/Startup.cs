using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project_CCSB.Models;
using Project_CCSB.Services;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Project_CCSB
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddControllersWithViews();

            services.AddTransient<IVehicleService, VehicleService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IContractService, ContractService>();
            services.AddTransient<IBlockedDatesService, BlockedDatesService>();
            services.AddTransient<IUserService, UserService>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddHttpContextAccessor();

            services.AddSingleton(Configuration
                                .GetSection("EmailConfiguration")
                                .Get<EmailConfiguration>());

            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<CustomViewRendererService>();

            // CRON jobs
            // Add Quartz services
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            // Add our job
            services.AddSingleton<ContractJob>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(ContractJob),
                cronExpression: "0 1 0 31 12 ? *")); // Run every last day of December
            // CRON for every 10 seconds: "0/10 * * * * ?"

            services.AddHostedService<QuartzHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
