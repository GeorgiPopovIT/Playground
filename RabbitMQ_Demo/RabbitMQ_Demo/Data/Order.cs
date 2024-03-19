using System.ComponentModel.DataAnnotations;

namespace RabbitMQ_Demo.Data;

public class Order
{
    [Required]
    public int Id { get; init; }
    [Required]
    public required string ProductName { get; set; }
    [Required]
    public required decimal Price { get; set; }
    [Required]
    public required int Quantity { get; set; }
}
