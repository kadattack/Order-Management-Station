namespace Metronik.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public string omsId { get; set; }
    public string orderId { get; set; }
    public int expectedCompletionTime { get; set; }
    public EmissionRequestDto EmissionRequestDto { get; set; }

}