using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SFDShop.Infrastructure;
using SFDShop.Application;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<SFDShopDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SFDShopDbContext>();
builder.Services.AddControllersWithViews().AddFluentValidation();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequiredLength = 8;
    options.SignIn.RequireConfirmedEmail = false;
    options.User.RequireUniqueEmail = true;

});
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");

        options.ClientId = googleAuthNSection["ClientId"];
        options.ClientSecret = googleAuthNSection["ClientSecret"];
    });

builder.Services.AddAplication(); 
builder.Services.AddInfrastructure(); 

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
    var adminEmail = "kacper_sopata@icloud.com"; // Podaj email administratora
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var newAdminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
        var createAdmin = await userManager.CreateAsync(newAdminUser, "fJwJqaTqZp"); // Ustaw has�o

        if (createAdmin.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdminUser, "Admin");
        }
    }
    else
    {
        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();  // Strona do obs�ugi migracji w trybie developerskim
}
else
{
    app.UseExceptionHandler("/Home/Error");  // Strona obs�ugi b��d�w w trybie produkcyjnym
    app.UseHsts();  // HTTP Strict Transport Security
}
app.UseHttpsRedirection();
app.UseStaticFiles();  // Obs�uga plik�w statycznych (np. CSS, JS)
app.UseRouting();
app.UseAuthentication();  // Uwierzytelnianie (logowanie)
app.UseAuthorization();  // Autoryzacja (sprawdzanie r�l, uprawnie�)

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");  // Domy�lny routing dla kontroler�w

app.MapRazorPages();  // Obs�uga stron Razor

app.Run();
