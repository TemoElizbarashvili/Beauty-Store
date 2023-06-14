using Beauty.Buisiness.Services;
using Beauty.Buisiness.Services.IServices;
using Beauty.DAL.Context;
using Beauty.DAL.Repositories;
using Beauty.DAL.Repositories.IRepository;
using Beauty.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Beauty.Web.Areas.Identity.Data;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IdentityDbContextConnection");builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>().AddDefaultTokenProviders();


//Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dependency Injections
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<ISeedIdentity, SeedIdentity>();

builder.Services.AddRazorPages();


//DB context config
builder.Services.AddDbContext<StoreDbContext>(opts =>
{
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:StoreConnection"], b => b.MigrationsAssembly("Beauty.Web"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapRazorPages();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//Seed data in Db contexts
SeedData.EnsurePopulated(app);
SeedIdentityDatabase();

app.Run();


void SeedIdentityDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<ISeedIdentity>();
        dbInitializer.EnsurePopulated(app);
    }
}