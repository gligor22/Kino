using Cineplex.Domain;
using Cineplex.Domain.Domain;
using Cineplex.Domain.Identity;
using Cineplex.Repository;
using Cineplex.Repository.Implementation;
using Cineplex.Repository.Interface;
using Cineplex.Service.Implementation;
using Cineplex.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIneplex.Web
{
    public class Startup
    {
        private EmailSettings emailSettings;

        public Startup(IConfiguration configuration)
        {
            emailSettings = new EmailSettings();
            Configuration = configuration;
            Configuration.GetSection("EmailSettings").Bind(emailSettings);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<CineplexApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
                
                

            services.AddScoped<IRepository<Acthor>,Repository<Acthor>>();
            services.AddScoped<IActhorsService, AchtorsService>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IRepository<Movie>, Repository<Movie>>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IRepository<ShoppingCart>, Repository<ShoppingCart>>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IRepository<EmailMessage>, Repository<EmailMessage>>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IBackgroundEmailSender, BackgroundEmailSender>();
            services.AddScoped<IRepository<Cineplex.Domain.Domain.Order>, Repository<Cineplex.Domain.Domain.Order>>();
            services.AddScoped<IRepository<TicketinOrder>, Repository<TicketinOrder>>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderService, Cineplex.Service.Implementation.OrderService>();
            services.AddScoped<UserRepoInterface, UserRepository>();
            services.AddScoped< IRepository < TicketsInShoppingCart > , Repository<TicketsInShoppingCart>>();

            //services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

            services.AddControllersWithViews();
            services.AddRazorPages();
           
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe")["SecretKey"]);


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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
                endpoints.MapRazorPages();
            });
        }
    }
}
