using Backend.Application.Common.Base;

namespace Backend.Application.Services.Orders.Requests;

public class MakeOrderRequest
{
    public List<int> ProductIds { get; set; } = new();
    public int PickupPointId { get; set; }
}