using backend.Services;
using Microsoft.AspNetCore.SignalR;

namespace backend.Hubs;

public class FeedbackHub : Hub
{
    private readonly FeedbackService _service;

    public FeedbackHub(FeedbackService service)
    {
        _service = service;
    }

    // Broadcast de estatísticas completas
    public async Task BroadcastStats()
    {
        var total = await _service.GetTotalFeedbacksAsync();
        var media = await _service.GetSatisfacaoMediaAsync();
        var (percentuais, geral) = await _service.GetPercentuaisSatisfacaoAsync();
        var ranking = await _service.GetRankingAsync();
        var ultimos3 = await _service.GetUltimos3Async();

        await Clients.All.SendAsync("ReceiveTotal", total);
        await Clients.All.SendAsync("ReceiveMedia", media);
        await Clients.All.SendAsync("ReceivePercentuais", percentuais, geral);
        await Clients.All.SendAsync("ReceiveRanking", ranking);
        await Clients.All.SendAsync("ReceiveUltimos3", ultimos3);
    }
}
