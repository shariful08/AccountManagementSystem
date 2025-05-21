using Microsoft.AspNetCore.Authentication.Cookies;
using MiniAccountManagementSystem.Repositories;
using MiniAccountManagementSystem.Repositories.Implementations;
using MiniAccountManagementSystem.Repositories.Interfaces;
using MiniAccountManagementSystem.Services.Accounts;
using MiniAccountManagementSystem.Services.Users;
using MiniAccountManagementSystem.Services.Vouchers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();

// Dependency Injection
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<IVoucherRepository, VoucherRepository>();

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Users/Login"; // Update this path to correct login page
        options.AccessDeniedPath = "/AccessDenied"; // Optional
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Redirect "/" to "/Users/Login"
app.MapGet("/", context =>
{
    context.Response.Redirect("/Users/Login");
    return Task.CompletedTask;
});

// Configure middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // ✅ Required for login to work
app.UseAuthorization();   // ✅ Required for [Authorize] to work

app.MapRazorPages();

app.Run();
