using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;
using Metronik.Enteties;

namespace Metronik.DTOs;
public class EmissionRequestDto
{

    public String omsId { get; set; }
    
    [Required]
    public ICollection<OrderProductDto> ProductsDto { get; set; }

    public OrderDetailsDto OrderDetailsDto { get; set; }
}