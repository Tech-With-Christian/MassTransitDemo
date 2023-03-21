using Domain.Entitites.Products;
using MediatR;

namespace Application.Features.Products.Notifications
{
    public record ProductCreatedNotification(Product product) : INotification;
}
