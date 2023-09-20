namespace MediatRDemo.Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required int ReleaseYear { get; set; }
}
