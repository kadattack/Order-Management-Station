using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;
using System.Threading.Tasks.Dataflow;
using Metronik.Entities;

namespace Metronik.Enteties;

public class AppOrderProduct
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required, MaxLength(14)]
    // Product GTIN
    public String Gtin { get; set; }

    [Required]
    // Quantity of ICs / Identifiers
    public ulong Quantity { get; set; }


    [Required]
    // Method of generation of individual serial number
    public String SerialNumberType { get; set; }

    [Required]
    // «serialNumberType = SELF_MADE». Unique serial numbers. This field is to be filled if only serialNumberType = SELF_MADE
    public ICollection<AppSerialNumbers> SerialNumbers { get; set; }

    [Required]
    public String TemplateId { get; set; }

    public int AppOrderId { get; set; }
    public AppOrder AppOrder { get; set; }

}