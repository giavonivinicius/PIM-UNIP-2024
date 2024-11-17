using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using PimUrbanGreen.Data;
using PimUrbanGreen.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A conexão com o banco de dados não foi encontrada. Verifique se 'DefaultConnection' está configurado corretamente no appsettings.json.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Registro dos repositórios como serviços
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<PedidoRepository>();

// Configuração de TempData e Sessão
builder.Services.AddDistributedMemoryCache(); // Necessário para armazenar dados em memória para sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expiração da sessão
    options.Cookie.HttpOnly = true; // Proteção contra acesso de JavaScript
    options.Cookie.IsEssential = true; // Necessário para funcionar em cookies essenciais
});
builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider(); // Configura o TempData para usar o estado da sessão

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
