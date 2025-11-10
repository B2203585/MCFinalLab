using Webapp.Services;

var builder = WebApplication.CreateBuilder(args);

// Register the service layer as a singleton
// The checker app will verify this service layer (intermediary between data and interface)
builder.Services.AddSingleton<IStaffService, StaffService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Staff}/{action=Index}/{id?}");


app.Run();
