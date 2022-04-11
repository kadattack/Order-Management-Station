using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Metronik.Enteties;

namespace Metronik.Entities;

public class AppSerialNumbers
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int AppOrderProductId {get; set;}
    public AppOrderProduct AppOrderProduct { get; set; }
    public string SerialNumber { get; set; }
}