using CoreAssignmentForRollBased.Data;
using CoreAssignmentForRollBased.Models;
using CoreAssignmentForRollBased.Repository;
using CoreAssignmentForRollBased.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var _Configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EmployeeDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<EmployeeDbContext>();
builder.Services.ConfigureApplicationCookie(Config =>
{
    Config.LoginPath = _Configuration["Application:LoginPath"];
});
builder.Services.AddScoped<IGenericsRepository<Profile>, GenericsRepository<Profile>>();
builder.Services.AddScoped<IGenericsRepository<UserViewModel>, GenericsRepository<UserViewModel>>();
builder.Services.AddScoped<IGenericsRepository<ManagerViewModel>, GenericsRepository<ManagerViewModel>>();
builder.Services.AddScoped<IGenericsRepository<ApplicationUser>, GenericsRepository<ApplicationUser>>();

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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
