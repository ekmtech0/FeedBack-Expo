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
            var exists = await db.Areas
                .AnyAsync(a => a.Id == model.AreaId);

            if (!exists)
            {
                return Results.Conflict("Esta área não existe");
            }

            var expositor = new Expositante
            {
                Name = model.Name,
                AreaId = model.AreaId
            };

            await db.Expositante.AddAsync(expositor);
            await db.SaveChangesAsync();

            return Results.Ok(expositor);
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
        endpoints.MapPut("/{id}", async (AppDbContext db, int id, ExpositanteDto model) =>
        {
            var expositante = await db.Expositante.FirstOrDefaultAsync(e => e.Id == id);
            if (expositante is null)
            {
                return Results.NotFound();
            }

            expositante.Name = model.Name;
            expositante.AreaId = model.AreaId;

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
    public string Name { get; set; } = null!;
    public int AreaId { get; set; }
}

