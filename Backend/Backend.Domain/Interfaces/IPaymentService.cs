using Backend.Domain.Models;


namespace Backend.Domain.Interfaces;

public interface IPaymentService
{
    Task Pay(Order order);
}