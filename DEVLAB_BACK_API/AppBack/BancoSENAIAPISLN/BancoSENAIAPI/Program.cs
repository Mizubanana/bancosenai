using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Banco SENAI - Sistema Financeiro",
        Version = "v1",
        Description = "API Gestão Financeira e Integração Clientes."
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// REMOVIDO: app.UseHttpsRedirection(); -> Evita o redirecionamento automático para HTTPS

app.UseCors("PermitirTudo");

app.UseAuthorization();

app.MapControllers();

app.Run();