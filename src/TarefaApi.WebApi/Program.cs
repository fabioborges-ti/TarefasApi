using Microsoft.EntityFrameworkCore;
using TarefaApi.Application.Services;
using TarefaApi.Domain.Interfaces;
using TarefaApi.Infrastructure.Context;
using TarefaApi.Infrastructure.Repositories;

namespace TarefaApi.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuração dos serviços
        ConfigureServices(builder);

        var app = builder.Build();

        // Configuração do pipeline de requisição
        ConfigureApp(app);

        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        // Configuração do DbContext
        builder.Services.AddDbContext<TarefaDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Registro dos serviços
        builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
        builder.Services.AddScoped<TarefaService>();

        // Registro dos controllers e Swagger
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Configuração CORS (opcional)
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });
    }

    private static void ConfigureApp(WebApplication app)
    {
        // Middleware de desenvolvimento
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        // Middleware de segurança e roteamento
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors("AllowAll"); // Use a política CORS configurada

        app.UseAuthentication();
        app.UseAuthorization();

        // Mapeamento de endpoints
        app.MapControllers();
    }
}