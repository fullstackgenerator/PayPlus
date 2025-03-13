using System.ComponentModel.DataAnnotations;
namespace PayPlus.Models;

public class Service
{
    public int Id { get; set; }

    [Display(Name = "Datum dogodka")]
    public DateTime EventDte { get; set; }
    
    [Display(Name = "Kraj dogodka")]
    public string EventLocation { get; set; }
    
    [Display(Name = "Ime storitve")]
    public string ServiceName { get; set; }

    [Display(Name = "Opis")]
    public string ServiceDescription { get; set; }

    [Display(Name = "Cena")]
    public decimal Price { get; set; }
}
