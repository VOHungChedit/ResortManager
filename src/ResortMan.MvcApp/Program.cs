using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResortMan.Data;
using ResortMan.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<AccomodationTypesService>();

builder.Services.AddScoped<AccomodationPackagesService>();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var sp = scope.ServiceProvider;

    var roleManager = sp.GetRequiredService<RoleManager<IdentityRole>>();

    //var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();

    //var user = await userManager.FindByIdAsync("");
    //await userManager.AddToRoleAsync(user, "");

    await roleManager.CreateAsync(new("Administrator"));
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
