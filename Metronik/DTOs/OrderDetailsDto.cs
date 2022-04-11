using Metronik.Enteties;

namespace Metronik.DTOs;

public class OrderDetailsDto
{

    public String Id { get; set; }
    public String FactoryId { get; set; }

    public String FactoryName { get; set; }

    public String OmsId { get; set; }

    public String FactoryAddress { get; set; }

    public String FactoryCountry { get; set; }

    public String ProductionLineId { get; set; }

    public String ProductCode { get; set; }

    public String ProductDescription { get; set; }

    public String PoNumber { get; set; }

    public String ExpectedStartDate { get; set; }

}