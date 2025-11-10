namespace backend.DTOs;

public class FeedbackDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Rating { get; set; }
    public string Comentario { get; set; } = null!;
    public string Area { get; set; } = null!;
    public string BrowserId { get; set; } = null!;
}

public class RatingPercentDto
{
    public int Rating { get; set; }
    public double Percentual { get; set; }
}

public class RankingDto
{
    public string AreaName { get; set; } = null!;
    public double AvgRating { get; set; }

    public int QtdFeedbacks{ get; set; }
}
