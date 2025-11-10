using System;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Enpoints;

public static class AreaEndpoint
{
    public static void MapAreaEnpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("api/areas")
            .WithTags("Areas");

        endpoints.MapPost("/{nome}", async (AppDbContext db, string nome) =>
        {
            var exists = await db.Areas.AnyAsync(a => a.Name == nome);
            if (exists)
            {
                return Results.Conflict("Está Area já está criada");
            }
            var area = new Area
            {
                Name = nome,

            };
            await db.Areas.AddAsync(area);
            await db.SaveChangesAsync();

            return Results.Ok(area);
        });
        endpoints.MapGet("/", async (AppDbContext db) =>
        {
            var list = await db.Areas
                .Include(a => a.Expositantes)
                .Include(a => a.Feedbacks)
                .Select(a => new
                {
                    a.Id,
                    a.Name,
                    Feedbacks = a.Feedbacks.Count,
                    Expositantes = a.Expositantes.Count
                }).ToListAsync();

            return Results.Ok(list);
        });
        endpoints.MapPut("/{id}/{name}", async (AppDbContext db, int id, string name) =>
        {
            var area = await db.Areas.FirstOrDefaultAsync(a => a.Id == id);
            if (area is null)
            {
                return Results.NotFound();
            }
            area.Name = name;
            await db.SaveChangesAsync();

            return Results.Ok(area);
        });
        endpoints.MapGet("/{id}", async (AppDbContext db, int id) =>
        {
            var area = await db.Areas
                .Include(a => a.Expositantes)
                .Include(a => a.Feedbacks)
                .Select(a => new
                {
                    a.Id,
                    a.Name,
                    Feedbacks = a.Feedbacks.Count,
                    Expositantes = a.Expositantes.Count
                })
                .FirstOrDefaultAsync(a => a.Id == id);

            if (area is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(area);
        });

        endpoints.MapDelete("/{id}", async (AppDbContext db, int id) =>
        {
            var area = await db.Areas.FirstOrDefaultAsync(a => a.Id == id);
            if (area is null)
            {
                return Results.NotFound();
            }

            db.Areas.Remove(area);
            await db.SaveChangesAsync();

            return Results.Ok($"Área {id} removida com sucesso");
        });

    }
}
