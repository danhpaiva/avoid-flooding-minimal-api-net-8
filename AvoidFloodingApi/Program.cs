using AvoidFloodingApi.Context;
using AvoidFloodingApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. ("ConfigureServices")
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
                                            options
                                            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

app.MapGet("/", () => "Lista de Localizações").ExcludeFromDescription();

app.MapPost("/localizacoes", async (Localizacao localizacao, AppDbContext db)
    =>
        {
            db.Localizacoes.Add(localizacao);
            await db.SaveChangesAsync();
            return Results.Created($"/localizacoes/{localizacao.LocalizacaoId}", localizacao);
        });

app.MapGet("localizacoes", async(AppDbContext db) => await db.Localizacoes.ToListAsync());

app.MapGet("/localizacoes/{id:int}", async (int id, AppDbContext db)
    =>
{
    return await db.Localizacoes.FindAsync(id)
        is Localizacao localizacao
        ? Results.Ok(localizacao)
        : Results.NotFound();
});

app.MapPut("/localizacoes/{id:int}", async (int id, Localizacao localizacao, AppDbContext db) =>
{
    if (localizacao.LocalizacaoId != id)
        return Results.BadRequest();

    var localizacaoDb = await db.Localizacoes.FindAsync(id);

    if (localizacaoDb is null) return Results.NotFound();

    localizacaoDb.Nome = localizacao.Nome;
    localizacaoDb.Latitude = localizacao.Latitude;
    localizacaoDb.Longitude = localizacao.Longitude;
    localizacaoDb.ProbabilidadeAlagamento = localizacao.ProbabilidadeAlagamento;
    localizacaoDb.DataAtualizacao = localizacao.DataAtualizacao;

    await db.SaveChangesAsync();

    return Results.Ok(localizacaoDb);
});

app.MapDelete("/localizacoes/{id:int}", async (int id, AppDbContext db) =>
{
    var localizacao = await db.Localizacoes.FindAsync(id);

    if (localizacao is null) return Results.NotFound();

    db.Localizacoes.Remove(localizacao);
    await db.SaveChangesAsync();

    return Results.Ok();
});

// Configure the HTTP request pipeline. ("Configure")
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();