using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Metronik.Entities;

namespace Metronik.Enteties;

public class AppOrderDetails
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public String FactoryId { get; set; }

    public String FactortyName { get; set; }

    public String FactoryAddress { get; set; }

    [Required]
    public String FactortyCountry { get; set; }

    [Required]
    public String ProductionLineId { get; set; }

    [Required]
    public String ProductCode { get; set; }

    [Required]
    public String ProductDescription { get; set; }

    public String PoNumber { get; set; }

    public String ExpectedStartDate { get; set; }

    public int AppOrderId { get; set; }
    public AppOrder AppOrder { get; set; }
}