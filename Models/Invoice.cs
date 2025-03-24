namespace PayPlus.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class Invoice
{
    public int Id { get; set; }

    [Display(Name = "Partner")]
    public int PartnerId { get; set; }

    [Display(Name = "Partner")]
    public Partner? Partner { get; set; }

    [Display(Name = "Services")]
    public List<Service> Services { get; set; } = new List<Service>();

    [Display(Name = "Offer Date")]
    public DateTime OfferDate { get; set; } = DateTime.Now;

    [Display(Name = "Total Price")]
    public decimal TotalPrice { get; set; }
}
