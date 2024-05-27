using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using WebShopClient.Handlers;
using WebShopClient.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.           
builder.Services.AddDatabaseDeveloperPageExceptionFilter();      

builder.Services.AddHttpClient("API Client", client =>
{
    client.BaseAddress = new Uri("https://localhost:7190/api/");
});

builder.Services.AddScoped<ApiService>();
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
            
builder.Services.AddAuthorizationBuilder()
    .SetDefaultPolicy(new AuthorizationPolicyBuilder("CustomAuth").RequireAuthenticatedUser().Build())
    .AddPolicy("Customer", policy => policy.RequireRole("Customer"));

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