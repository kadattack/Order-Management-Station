using Metronik.Enteties;

namespace Metronik.DTOs;

public class OrderProductDto
{
    public int Id { get; set; }
    // Product GTIN
    public String Gtin { get; set; }

    // Quantity of ICs / Identifiers
    public ulong Quantity { get; set; }

    // Method of generation of individual serial number
    public String SerialNumberType { get; set; }

    // «serialNumberType = SELF_MADE». Unique serial numbers. This field is to be filled if only serialNumberType = SELF_MADE
    public ICollection<SerialNumberDto> SerialNumbersDto { get; set; }

    public String TemplateId { get; set; }

}