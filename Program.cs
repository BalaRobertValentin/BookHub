using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookRental.Data;
using BookRental.Services;
using Microsoft.AspNetCore.Http;
using System;
using BookRental.Models;
using Microsoft.AspNetCore.Identity;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<CartService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationIdentityDbContext") ?? throw new InvalidOperationException("Connection string 'ApplicationIdentityDbContext' not found.")));

// Include roles in the configuration
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

builder.Services.AddDbContext<BookRentalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookRentalContext") ?? throw new InvalidOperationException("Connection string 'BookRentalContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication(); // Use Authentication is necessary for Identity to work
app.UseAuthorization();

app.MapRazorPages();

app.Run();
