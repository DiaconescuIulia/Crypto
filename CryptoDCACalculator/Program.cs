using CryptoDCACalculator.Components;
using CryptoDCACalculator.Entities;
using CryptoDCACalculator.Servicies.IServicies;
using CryptoDCACalculator.Servicies.ServiciesImpl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();

builder.Services.AddScoped<ICryptocurrencyService, CryptocurrencyService>();

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();

app.MapControllers();

app.UseRouting();

app.UseAntiforgery();

//app.UseEndpoints();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
