using System.ComponentModel.DataAnnotations;
namespace PayPlus.Models
{
    public class Partner
    {
        public required int Id { get; set; }
        
        [Display(Name = "Name")]
        public required string Name { get; set; }
        
        [Display(Name = "VAT Number")]
        public required int VAT_number { get; set; }
        
        [Display(Name = "Address")]
        public required string Address { get; set; }
        
        [Display(Name = "Postal Code")]
        public required int Postal_code { get; set; }
        
        [Display(Name = "City")]
        public required string City { get; set; }
        
        [Display(Name = "Country")]
        public required string Country { get; set; }
        
        [Display(Name = "E-mail")]
        public required string Email { get; set; }
        
        [Display(Name = "Phone")]
        public required string Phone { get; set; }
    }
}