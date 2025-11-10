using backend.DTOs;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class FeedbackService
{
    private readonly AppDbContext _db;

    public FeedbackService(AppDbContext db)
    {
        _db = db;
    }

    // 1) Últimos 3 feedbacks
    public async Task<List<FeedbackDto>> GetUltimos3Async()
    {
        return await _db.Feedbacks
            .Include(f => f.Area)
            .OrderByDescending(f => f.Id)
            .Take(3)
            .Select(f => new FeedbackDto
            {
                Id = f.Id,
                Name = f.Name,
                Rating = f.Rating,
                Comentario = f.Comentario,
                Area = f.Area.Name,
                BrowserId = f.BrowserId
            })
            .ToListAsync();
    }

    // 2) Ranking: área com maior média de rating
    public async Task<List<RankingDto>> GetRankingAsync()
    {
        const int minimo = 5; // número mínimo de avaliações para confiança
        var globalAvg = await _db.Feedbacks.AnyAsync()
            ? await _db.Feedbacks.AverageAsync(f => f.Rating)
            : 0;

        var ranking = await _db.Areas
            .Include(a => a.Feedbacks)
            .Select(a => new RankingDto
            {
                AreaName = a.Name,
                QtdFeedbacks = a.Feedbacks.Count,
                AvgRating = a.Feedbacks.Count == 0 ? 0 :
                    ((a.Feedbacks.Average(f => f.Rating) * a.Feedbacks.Count) + (globalAvg * minimo))
                    / (a.Feedbacks.Count + minimo)
            })
            .OrderByDescending(r => r.AvgRating)
            .ToListAsync();

        return ranking;
    }



    // 3) Total de feedbacks
    public async Task<int> GetTotalFeedbacksAsync()
    {
        return await _db.Feedbacks.CountAsync();
    }

    // 4) Percentual de satisfação por rating (1–5)
    public async Task<(List<RatingPercentDto> Detalhes, double PercentualGeral)> GetPercentuaisSatisfacaoAsync()
    {
        var total = await _db.Feedbacks.CountAsync();
        var result = new List<RatingPercentDto>(capacity: 5);

        for (int r = 1; r <= 5; r++)
        {
            var count = await _db.Feedbacks.CountAsync(f => f.Rating == r);
            var pct = total == 0 ? 0 : (count * 100.0 / total);
            result.Add(new RatingPercentDto { Rating = r, Percentual = pct });
        }

        // cálculo da satisfação geral (média dos ratings em percentual)
        double geral = 0;
        if (total > 0)
        {
            var media = await _db.Feedbacks.AverageAsync(f => f.Rating);
            geral = (media / 5.0) * 100.0;
        }

        return (result, geral);
    }


    // Opcional: média geral (satisfação média)
    public async Task<double> GetSatisfacaoMediaAsync()
    {
        var total = await _db.Feedbacks.CountAsync();
        return total == 0 ? 0 : await _db.Feedbacks.AverageAsync(f => f.Rating);
    }
}
