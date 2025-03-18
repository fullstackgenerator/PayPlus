using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PayPlus.Models;

public enum CarType
{
    PrivateCar,
    CompanyCar
    
}

public class TravelOrder
{
    [Key]
    public required int order_id { get; set; }

    [DisplayName("Datum izdaje dokumenta:")]
    public required DateTime order_date { get; set; }

    [DisplayName("Datum začetka potovanja:")]
    public required DateTime date_start { get; set; }

    [DisplayName("Datum zaključka potovanja:")]
    public required DateTime date_end { get; set; }

    [DisplayName("Začetna lokacija:")]
    public required string location_start { get; set; }
    
    [DisplayName("V/na:")]
    public required string location_end { get; set; }

    [DisplayName("Ime voznika:")]
    public required string full_name_driver { get; set; }

    [DisplayName("Znamka in model avtomobila:")]
    public required string car_brand_and_model { get; set; }

    [DisplayName("Vrsta vozila:")]
    public required CarType car_type { get; set; }
    
    [DisplayName("namen potovanja:")]
    public required string trip_reason { get; set; }
    
    [DisplayName("Potovanje odobril/a:")]
    public required string full_name_organizer { get; set; }
}