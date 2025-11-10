using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Enpoints;

public static class ExpositanteEndpoint
{
    public static void MapExpositanteEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("api/expositantes")
            .WithTags("Expositantes");

        // CREATE
        endpoints.MapPost("/", async (AppDbContext db, ExpositanteDto model) =>
        {
            var exists = await db.Expositante
                .AnyAsync(e => e.AreaId == model.AreaId);
            if (exists)
            {
                return Results.Conflict("Este expositante já está cadastrado nesta área.");
            }
            var area = await db.Expositante.AnyAsync(c => c.AreaId == model.AreaId);
            if(!area)
                return Results.Conflict("Esta area não existe");

            await db.Expositante.AddAsync(new Expositante
            {
                Name = model.Name,
                AreaId = model.AreaId
            });
            await db.SaveChangesAsync();

            return Results.Ok(model);
        });

        // READ ALL
        endpoints.MapGet("/", async (AppDbContext db) =>
        {
            var list = await db.Expositante
                .Include(e => e.Area)
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                    Area = e.Area.Name
                }).ToListAsync();

            return Results.Ok(list);
        });

        // READ BY ID
        endpoints.MapGet("/{id}", async (AppDbContext db, int id) =>
        {
            var expositante = await db.Expositante
                .Include(e => e.Area)
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                    Area = e.Area.Name
                })
                .FirstOrDefaultAsync(e => e.Id == id);

            if (expositante is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(expositante);
        });

        // UPDATE
        endpoints.MapPut("/{id}", async (AppDbContext db, int id, Expositante input) =>
        {
            var expositante = await db.Expositante.FirstOrDefaultAsync(e => e.Id == id);
            if (expositante is null)
            {
                return Results.NotFound();
            }

            expositante.Name = input.Name;
            expositante.AreaId = input.AreaId;

            await db.SaveChangesAsync();

            return Results.Ok(expositante);
        });

        // DELETE
        endpoints.MapDelete("/{id}", async (AppDbContext db, int id) =>
        {
            var expositante = await db.Expositante.FirstOrDefaultAsync(e => e.Id == id);
            if (expositante is null)
            {
                return Results.NotFound();
            }

            db.Expositante.Remove(expositante);
            await db.SaveChangesAsync();

            return Results.Ok($"Expositante {id} removido com sucesso");
        });
    }
}
public class ExpositanteDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int AreaId { get; set; }
    public string AreaName { get; set; } = null!;
}

