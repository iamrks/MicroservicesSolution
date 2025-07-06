using BuildingBlocks.EventBus.Events;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Dtos;
using OrderService.Models;

namespace OrderService.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class OrdersController(OrderDbContext context) : ControllerBase
{
    private readonly OrderDbContext _context = context;

    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<Order>>> GetUserOrders(string userId)
    {
        var orders = await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.UserId == userId)
            .ToListAsync();

        var ordersRes = new List<OrderResponseDto>();

        foreach (var order in orders)
        {
            ordersRes.Add(new OrderResponseDto
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Price = i.Price,
                    Quantity = i.Quantity
                }).ToList(),
                TotalAmount = order.TotalAmount
            });
        }

        return Ok(ordersRes);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDto orderDto, [FromServices] IPublishEndpoint publisher)
    {
        var order = new Order
        {
            UserId = orderDto.UserId,
            Items = orderDto.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Price = i.Price,
                Quantity = i.Quantity
            }).ToList()
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var evt = new OrderCreatedEvent
        {
            OrderId = order.Id,
            UserId = order.UserId,
            Items = order.Items.Select(i => new OrderItemEventDto
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Price = i.Price,
                Quantity = i.Quantity
            }).ToList(),
            TotalAmount = order.TotalAmount
        };

        await publisher.Publish(evt);

        return Ok($"Order {order.Id} created successfully and event published.");
    }
}
