using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Backend.Areas.Identity.Data;
using Backend.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure the connection string for the database
var connectionString = builder.Configuration.GetConnectionString("BackendContextConnection")
    ?? throw new InvalidOperationException("Connection string 'BackendContextConnection' not found.");

// Add DbContext for Backend context
builder.Services.AddDbContext<BackendContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<BackendUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BackendContext>();


// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseSession(); // Add session middleware

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
