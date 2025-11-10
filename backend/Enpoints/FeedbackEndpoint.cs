using System.ComponentModel.DataAnnotations;
using backend.Hubs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace backend.Enpoints;

public static class FeedbackEndpoint
{
    public static void MapFeedbackEndpoints(this IEndpointRouteBuilder app)
    {
        var endpoints = app.MapGroup("api/feedbacks")
            .WithTags("Feedbacks");

        // CREATE
        endpoints.MapPost("/", async (
            AppDbContext db,
            FeedbackService service,                // injeta o serviço que calcula estatísticas
            IHubContext<FeedbackHub> hubContext,     // injeta o Hub do SignalR
            [FromBody] FeedBackDto model) =>
        {
            var exists = await db.Feedbacks
                .AnyAsync(f => f.BrowserId == model.BrowserId && f.AreaId == model.AreaId);

            if (exists)
            {
                return Results.Conflict("Este browser já deixou feedback nesta área.");
            }

            var feedback = new Feedback
            {
                Name = model.Name,
                BrowserId = model.BrowserId,
                AreaId = model.AreaId,
                Rating = model.Rating,
                Comentario = model.Comentario
            };

            await db.Feedbacks.AddAsync(feedback);
            await db.SaveChangesAsync();

            var total = await service.GetTotalFeedbacksAsync();
            var media = await service.GetSatisfacaoMediaAsync();
            var (percentuais, geral) = await service.GetPercentuaisSatisfacaoAsync();
            var ranking = await service.GetRankingAsync();
            var ultimos3 = await service.GetUltimos3Async();

            await hubContext.Clients.All.SendAsync("ReceiveTotal", total);
            await hubContext.Clients.All.SendAsync("ReceiveMedia", media);
            await hubContext.Clients.All.SendAsync("ReceivePercentuais", percentuais, geral);
            await hubContext.Clients.All.SendAsync("ReceiveRanking", ranking);
            await hubContext.Clients.All.SendAsync("ReceiveUltimos3", ultimos3);

            return Results.Ok(feedback);
        });


        // READ ALL
        endpoints.MapGet("/", async (AppDbContext db) =>
        {
            var list = await db.Feedbacks
                .Include(f => f.Area)
                .Select(f => new
                {
                    f.Id,
                    f.Name,
                    f.Rating,
                    f.Comentario,
                    Area = f.Area.Name,
                    f.BrowserId
                }).ToListAsync();

            return Results.Ok(list);
        });

        // READ BY ID
        endpoints.MapGet("/{id}", async (AppDbContext db, int id) =>
        {
            var feedback = await db.Feedbacks
                .Include(f => f.Area)
                .Select(f => new
                {
                    f.Id,
                    f.Name,
                    f.Rating,
                    f.Comentario,
                    Area = f.Area.Name,
                    f.BrowserId
                })
                .FirstOrDefaultAsync(f => f.Id == id);

            if (feedback is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(feedback);
        });

        // DELETE
        endpoints.MapDelete("/{id}", async (AppDbContext db, int id) =>
        {
            var feedback = await db.Feedbacks.FirstOrDefaultAsync(f => f.Id == id);
            if (feedback is null)
            {
                return Results.NotFound();
            }

            db.Feedbacks.Remove(feedback);
            await db.SaveChangesAsync();

            return Results.Ok($"Feedback {id} removido com sucesso");
        });
                endpoints.MapGet("/ultimos3", async (FeedbackService service) =>
        {
            var last3 = await service.GetUltimos3Async();
            return Results.Ok(last3);
        });

        // ENDPOINT: ranking (área com maior média)
        endpoints.MapGet("/ranking", async (FeedbackService service) =>
        {
            var ranking = await service.GetRankingAsync();
            if (ranking is null) return Results.NotFound("Nenhum feedback encontrado");
            return Results.Ok(ranking);
        });

        // ENDPOINT: total de feedbacks
        endpoints.MapGet("/total", async (FeedbackService service) =>
        {
            var total = await service.GetTotalFeedbacksAsync();
            return Results.Ok(total);
        });

        // ENDPOINT: percentuais de satisfação
        endpoints.MapGet("/percentuais", async (FeedbackService service) =>
        {
            var (percentuais, geral) = await service.GetPercentuaisSatisfacaoAsync();
            return Results.Ok(new { percentuais, geral });
        });

        // ENDPOINT: média geral de satisfação
        endpoints.MapGet("/media", async (FeedbackService service) =>
        {
            var media = await service.GetSatisfacaoMediaAsync();
            return Results.Ok(media);
        });
    }
}
public class FeedBackDto
{
    public string Name { get; set; } = null!;
    public string BrowserId { get; set; } = null!;
    public string Comentario { get; set; } = null!;
 
    [Range(1, 5)]
    public int Rating { get; set; }
    
    public int AreaId{ get; set; }
}