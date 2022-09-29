using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Transportation.Core.Conventors;
using Transportation.Core.Repositories;
using Transportation.Core.Services;
using Transportation.DataLayer.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

#region DataBase Context

builder.Services.AddDbContext<TransportMainContext>(options => {
    options.UseSqlServer(builder.Configuration["ConnectionStrings:TransportationConnection"]);
});

#endregion

#region Services IoC

builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IContractService, ContractService>();

#endregion

#region Repositories IoC

builder.Services.AddTransient<IAdminRepository, AdminRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IContractRepository, ContractRepository>();

#endregion

#region Render View

builder.Services.AddTransient<IViewRenderService, RenderViewToString>();

#endregion

#region Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(option =>
{
    option.LoginPath = "/Login";
    option.LogoutPath = "/Logout";
    option.ExpireTimeSpan = TimeSpan.FromDays(7);
    option.Cookie.Name = "Auth.Coo";
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();

