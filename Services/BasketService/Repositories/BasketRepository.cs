using BasketService.Models;
using System.Collections.Concurrent;

namespace BasketService.Repositories;

public interface IBasketRepository
{
    CustomerBasket? GetBasket(string userId);
    void UpdateBasket(CustomerBasket basket);
    void DeleteBasket(string userId);
}

public class BasketRepository : IBasketRepository
{
    private static readonly ConcurrentDictionary<string, CustomerBasket> _baskets = new();

    public CustomerBasket? GetBasket(string userId) =>
        _baskets.TryGetValue(userId, out var basket) ? basket : null;

    public void UpdateBasket(CustomerBasket basket) =>
        _baskets[basket.UserId] = basket;

    public void DeleteBasket(string userId) =>
        _baskets.TryRemove(userId, out _);
}
