﻿namespace MediatRDemo.Application.Dtos;

public class MovieDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required int ReleaseYear { get; set; }
}
