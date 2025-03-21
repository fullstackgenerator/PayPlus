using System.ComponentModel.DataAnnotations;
namespace PayPlus.Models;

public class Service
{
    public int Id { get; set; }
    
    [Display(Name = "Service name")]
    [MaxLength(150)]
    public required string ServiceName { get; set; }

    [Display(Name = "Description")]
    [MaxLength(600)]
    public required string ServiceDescription { get; set; }

    [Display(Name = "Price")]
    public decimal Price { get; set; }
}
