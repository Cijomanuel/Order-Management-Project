using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderWebApplication.Entity.Models;
using OrderWebApplication.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Administrative/AccesDenied");
});
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

//builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IVendorRepository, VendorRepository>();

//builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PurchaseRolePolicy", policy => policy.RequireClaim("PurchaseRoleClaim"));
    options.AddPolicy("ClaimRolePolicy", policy => policy.RequireClaim("ClaimRoleClaim"));
    options.AddPolicy("ProductRolePolicy", policy => policy.RequireClaim("ProductRoleClaim"));
    options.AddPolicy("SalesRolePolicy", policy => policy.RequireClaim("SalesRoleClaim"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
