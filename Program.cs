using Microsoft.EntityFrameworkCore;
using SGE.Data;
using SGE.Repositories;
using SGE.Services;
using SGE.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IReferenteRepository, ReferenteRepository>();

builder.Services.AddScoped<IPadronImportService, PadronImportService>();
builder.Services.AddScoped<IReferenteService, ReferenteService>();builder.Services.AddScoped<IMovilizadorRepository, MovilizadorRepository>();
builder.Services.AddScoped<IAsignacionVotanteRepository,AsignacionVotanteRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
