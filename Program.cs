using WebPortal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuration pour HttpClient
builder.Services.AddHttpClient();

// Enregistrement du service Joke
builder.Services.AddScoped<IJokeService, JokeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configuration des routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Route spécifique pour les blagues par type
app.MapControllerRoute(
    name: "jokeByType",
    pattern: "Joke/Type/{type}",
    defaults: new { controller = "Joke", action = "ByType" });

app.Run();