using Metronik.Enteties;

namespace Metronik.Entities;

public class AppOrder
{
    public int Id { get; set; }
    public string omsId { get; set; }
    public string orderId { get; set; }
    public int expectedCompletionTime { get; set; }
    public AppOrderDetails AppOrderDetails { get; set; }
    public List<AppOrderProduct> AppOrderProducts { get; set; }
}
