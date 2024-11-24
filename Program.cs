using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using PimUrbanGreen.Data;
using PimUrbanGreen.Repositories;

var builder = WebApplication.CreateBuilder(args);

// banco de dados
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A conexão com o banco de dados não foi encontrada. Verifique se 'DefaultConnection' está configurado corretamente no appsettings.json.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Registro das repository
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<PedidoRepository>();

// Configuração de TempData e Sessão para depois enviar ao bd o usuario
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expiração da sessão em 30 minutos
    options.Cookie.HttpOnly = true; // Proteção contra acesso de JavaScript "garantir segurança na web"
    options.Cookie.IsEssential = true; // cookies essenciais "não sei a importância de deixar essa parte"
});
builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider(); // Configura o TempData para usar o estado da sessão "devido a necessidade de garantir que o usuario seja submetido ao banco"

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Configuração da sessão
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
