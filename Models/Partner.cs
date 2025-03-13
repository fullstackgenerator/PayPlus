using System.ComponentModel.DataAnnotations;
namespace PayPlus.Models
{
    public class Partner
    {
        public required int Id { get; set; }
        
        [Display(Name = "Naziv")]
        public required string Name { get; set; }
        
        [Display(Name = "Davčna številka")]
        public required int VAT_number { get; set; }
        
        [Display(Name = "Naslov")]
        public required string Address { get; set; }
        
        [Display(Name = "Poštna številka")]
        public required int Postal_code { get; set; }
        
        [Display(Name = "Kraj")]
        public required string City { get; set; }
        
        [Display(Name = "Država")]
        public required string Country { get; set; }
    }
}