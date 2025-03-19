using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PayPlus.Models;

public enum CarType
{
    [Display(Name = "Lastno vozilo")]
    PrivateCar,

    [Display(Name = "Službeno vozilo")]
    CompanyCar
    
}

public class TravelOrder
{
    [Key]
    [DisplayName("Številka naloga:")]
    public required int order_id { get; set; }

    [DisplayName("Datum dokumenta:")]
    [DataType(DataType.Date)]
    public required DateTime order_date { get; set; }

    [DisplayName("Datum začetka:")]
    [DataType(DataType.Date)]
    public required DateTime date_start { get; set; }

    [DisplayName("Datum zaključka:")]
    [DataType(DataType.Date)]
    public required DateTime date_end { get; set; }

    [DisplayName("Začetna lokacija:")]
    public required string location_start { get; set; }
    
    [DisplayName("V/na:")]
    public required string location_end { get; set; }

    [DisplayName("Ime voznika:")]
    public required string full_name_driver { get; set; }

    [DisplayName("Znamka in model vozila:")]
    public required string car_brand_and_model { get; set; }

    [DisplayName("Vrsta vozila:")]
    public required CarType car_type { get; set; }
    
    [DisplayName("Namen:")]
    public required string trip_reason { get; set; }
    
    [DisplayName("Odobril/a:")]
    public required string full_name_organizer { get; set; }
}