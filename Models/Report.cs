using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PayPlus.Models;

public class Report
{
    [DisplayName("Report Date:")]
    public DateTime ReportDate { get; set; }
    
    [DisplayName("Start Date:")]
    public DateTime StartDate { get; set; }
    
    [DisplayName("End Date:")]
    public DateTime EndDate { get; set; }
    
    [DisplayName("Number of Services")]
    public int ServiceCount { get; set; }
    
    [DisplayName("Number of Partners")]
    public int PartnerCount { get; set; }
    
    [DisplayName("Services:")]
    public List<Service> Services { get; set; } = new List<Service>();
    
    [DisplayName("Partners:")]
    public List<Partner> Partners { get; set; } = new List<Partner>();
    
    [DisplayName("Invoices:")]
    public List<Invoice> Invoices { get; set; } = new List<Invoice>();
    
    [DisplayName("Total Amount")]
    [DisplayFormat(DataFormatString = "{0:C}")]
    public decimal TotalAmount { get; set; }
    
    public int? SelectedPartnerId { get; set; }
}