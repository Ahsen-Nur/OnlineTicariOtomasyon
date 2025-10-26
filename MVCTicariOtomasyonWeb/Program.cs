using Microsoft.EntityFrameworkCore;
using MVCTicariOtomasyonWeb.Models.sınıflar;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// DbContext → appsettings.json / ConnectionStrings:DefaultConnection
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Geliştirme sırasında hata detayları
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Statik dosyalar (wwwroot)
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Default route → Kategori/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Kategori}/{action=Index}/{id?}");

app.Run();
