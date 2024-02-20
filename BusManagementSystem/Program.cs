using BusManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("ReservationDbContextConnectionProduction3") ?? throw new InvalidOperationException("Connection string 'ReservationDbContextConnection' not found.");
builder.Services.AddSession();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

//add database conntection
builder.Services.AddDbContext<ReservationSystemContext>(options =>
    options.UseSqlServer(connectionString));

//add default Identity
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ReservationSystemContext>();

builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IBusRepository, BusRepository>();
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IBusTypeMappingRepository, BusTypeMappingRepository>();
builder.Services.AddScoped<IGenderMappingRepository, GenderRepository>();
builder.Services.AddScoped<ILicenseTypeMappingRepository, LicenseTypeMappingRepository>();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Set password policy here
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});
// Add services to the container.
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.UseDeveloperExceptionPage();
using (var scope = app.Services.CreateScope()) {
    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    await roleManager.CreateAsync(new IdentityRole("Admin"));
    await roleManager.CreateAsync(new IdentityRole("Passenger"));
}

app.Run();
