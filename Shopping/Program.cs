using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Data.Entities;
using Shopping.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Formas de inyeccion 

/* \
 * 
 * Ciclo de vida
 1 . Transcient  - Ejecutarlo solo una vez 
 2. Scoope - la inyecta cada vez que la necesita y destruye cuando deja de utilizarse (mas comun)
 3. Singleton - se inyecta una vez y no lo destruye, lo deja en memoria
 
 */

builder.Services.AddDbContext<DataContext>( o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


//TODO : Make stringest password
builder.Services.AddIdentity<User, IdentityRole>(cfg =>
{
    // Condiciones de los usuarios
    cfg.User.RequireUniqueEmail = true;
    cfg.Password.RequireDigit = false;
    cfg.Password.RequiredUniqueChars = 0;
    cfg.Password.RequireLowercase = false;
    cfg.Password.RequireUppercase = false;
    cfg.Password.RequireNonAlphanumeric = false;
    cfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    cfg.Lockout.MaxFailedAccessAttempts = 3;
    cfg.Lockout.AllowedForNewUsers = true;
    //cfg.Password.RequiredLength = 6;

}).AddEntityFrameworkStores<DataContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/NotAuthorized"; // Problema de login
    options.AccessDeniedPath = "/Account/NotAuthorized"; // Problema de Access denied
});




builder.Services.AddTransient<SeedDb>();
builder.Services.AddScoped<IUserHelper, UserHelper>(); // Mandar la interface permite preparar el proyecto para pruebas unitarias
builder.Services.AddScoped<IComboHelper, ComboHelper>();
builder.Services.AddScoped<IBlobHelper, BlobHelper>();


var app = builder.Build();


SeedData();

void SeedData()
{
    IServiceScopeFactory? scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (IServiceScope? scope = scopedFactory.CreateScope())
    {
        SeedDb? service = scope.ServiceProvider.GetService<SeedDb>();
        service.SeedAsync().Wait();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
