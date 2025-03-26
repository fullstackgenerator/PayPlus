namespace PayPlus.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class Invoice
{
    [Key]
    [Display(Name = "Invoice ID:")]
    public int Id { get; set; }

    [Display(Name = "Partner")]
    public int PartnerId { get; set; }

    [Display(Name = "Partner")]
    public Partner? Partner { get; set; }

    [Display(Name = "Services")]
    public List<Service> Services { get; set; } = new List<Service>();

    [Display(Name = "Invoice Date")]
    public DateTime Date { get; set; } = DateTime.Now;

    [Display(Name = "Total Price")]
    public decimal TotalPrice { get; set; }
}
