using System.ComponentModel;

namespace PayPlus.Models;

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
    public required string start_location { get; set; }
    
    [DisplayName("Ime voznika:")]
    public required string full_name_driver { get; set; }
    
    [DisplayName("Ime sovoznika:")]
    public string full_name_passenger { get; set; }
    
    [DisplayName("Ime drugega sovoznika:")]
    public string full_name_passenger_two { get; set; }

    [DisplayName("Ime tretjega sovoznika:")]
    public string full_name_passenger_three { get; set; }
    
    [DisplayName("Ime četrtega sovoznika:")]
    public string full_name_passenger_four { get; set; }
    
}