﻿using System.ComponentModel.DataAnnotations;

namespace SmartDevice.Models;

public class PurchaseInvoice
{
    [Key] public required int PurchaseInvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }
    public required int EmployeeId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
    public ICollection<PurchaseDetails>? PurchaseDetails { get; set; }
}