using System.ComponentModel.DataAnnotations;

namespace PayPlus.Models
{
    public class Offer
    {

        [Key]
        [Display(Name = "Offer ID:")]
        public required int Id { get; set; }

        [Display(Name = "Partner")]
        public int PartnerId { get; set; }

        [Display(Name = "Partner")]
        public Partner? Partner { get; set; }

        [Display(Name = "Services")]
        public List<Service> Services { get; set; } = new List<Service>();

        [Display(Name = "Offer Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }
    }
}