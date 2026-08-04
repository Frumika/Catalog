using Backend.Domain.Interfaces;
using Backend.Domain.Models;

namespace Backend.Infrastructure.Services.Payment;

public class StubPaymentService : IPaymentService
{
    public Task Pay(Order order) => Task.CompletedTask;
}