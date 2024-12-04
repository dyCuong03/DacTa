using Microsoft.AspNetCore.Mvc;
using SmartDevice.Datas;

namespace SmartDevice.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PurchaseInvoiceController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PurchaseInvoiceController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPurchaseInvoice(string purchaseInvoiceId) 
        => Ok(_context.PurchaseInvoices.Where(o => o.PurchaseInvoiceId == purchaseInvoiceId));

}