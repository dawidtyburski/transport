using Microsoft.AspNetCore.Identity;
using transport;
using transport.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<ApiBehaviorOptions>(options =>
options.SuppressModelStateInvalidFilter = true);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<transportDbContext>();

builder.Services.AddIdentity<CustomUser, IdentityRole>() 
 .AddEntityFrameworkStores<transportDbContext>()
 .AddDefaultTokenProviders();
builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



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
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
DataSeeder.Initialize(app);
app.Run();


