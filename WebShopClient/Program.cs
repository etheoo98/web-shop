using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using WebShopClient.Handlers;
using WebShopClient.Services;

namespace WebShopClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.           
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();      
            

            /****************************************** IDENTITY USER - DBCONTEXT ****************************************
            **************************************************************************************************************
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            **************************************************************************************************************
            *************************************************************************************************************/

            builder.Services.AddHttpClient("API Client", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7190/api/");
            });

            builder.Services.AddScoped<ApiServices>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ShoppingCartService>();
            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<DiscountService>();
            builder.Services.AddScoped<OrderService>();

            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            builder.Services.AddSession(options =>
            {
                //options.Cookie.Name = ".ShoppingCart.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(120);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            
            builder.Services.AddAuthentication("CustomAuth")
                .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>("CustomAuth", null);

            builder.Services.AddAuthorizationBuilder()
                .SetDefaultPolicy(new AuthorizationPolicyBuilder("CustomAuth").RequireAuthenticatedUser().Build())
                .AddPolicy("Admin", policy => policy.RequireRole("Admin"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
