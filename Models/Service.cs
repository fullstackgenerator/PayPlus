using System.ComponentModel.DataAnnotations;
namespace PayPlus.Models;

public class Service
{
    public int Id { get; set; }
    
    [Display(Name = "Ime storitve")]
    [MaxLength(150)]
    public required string ServiceName { get; set; }

    [Display(Name = "Opis")]
    [MaxLength(600)]
    public required string ServiceDescription { get; set; }

    [Display(Name = "Cena")]
    public decimal Price { get; set; }
}
