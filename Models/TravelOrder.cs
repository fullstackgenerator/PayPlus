using System.ComponentModel;

namespace PayPlus.Models;

public enum CarType
{
    PrivateCar,
    CompanyCar
    
}

public class TravelOrder
{
    public required int order_id { get; set; }

    [DisplayName("Datum dokumenta:")]
    public required DateTime order_date { get; set; }

    [DisplayName("Datum začetka potovanja:")]
    public required DateTime start_date { get; set; }

    [DisplayName("Datum zaključka potovanja:")]
    public required DateTime end_date { get; set; }

    [DisplayName("V/na:")]
    public required string location { get; set; }

    [DisplayName("Ime voznika:")]
    public required string driver_full_name { get; set; }

    [DisplayName("Znamka in model avtomobila:")]
    public required string car_brand_and_model { get; set; }

    [DisplayName("Razlog potovanja:")]
    public required string trip_reason { get; set; }

    [DisplayName("Odobravam uporabo:")]
    public required CarType car_type { get; set; }
}