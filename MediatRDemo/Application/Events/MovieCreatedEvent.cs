using MediatR;
using MediatRDemo.Application.Dtos;

namespace MediatRDemo.Application.Events;

public record MovieCreatedEvent(MovieDto MovieDto) : INotification;
