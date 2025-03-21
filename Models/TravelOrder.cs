using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PayPlus.Models;

public enum CarType
{
    [Display(Name = "Private vehicle")]
    PrivateCar,

    [Display(Name = "Company vehicle")]
    CompanyCar
    
}

public class TravelOrder
{
    [Key]
    [DisplayName("Order ID:")]
    public required int order_id { get; set; }

    [DisplayName("Document Date:")]
    [DataType(DataType.Date)]
    public required DateTime order_date { get; set; }

    [DisplayName("Start date:")]
    [DataType(DataType.Date)]
    public required DateTime date_start { get; set; }

    [DisplayName("End date:")]
    [DataType(DataType.Date)]
    public required DateTime date_end { get; set; }

    [DisplayName("Start location:")]
    public required string location_start { get; set; }
    
    [DisplayName("Destination:")]
    public required string location_end { get; set; }

    [DisplayName("Driver's full name:")]
    public required string full_name_driver { get; set; }

    [DisplayName("Vehicle Brand and Model:")]
    public required string car_brand_and_model { get; set; }

    [DisplayName("Vehicle type:")]
    public required CarType car_type { get; set; }
    
    [DisplayName("Purpose:")]
    public required string trip_reason { get; set; }
    
    [DisplayName("Confirmed by:")]
    public required string full_name_organizer { get; set; }
}