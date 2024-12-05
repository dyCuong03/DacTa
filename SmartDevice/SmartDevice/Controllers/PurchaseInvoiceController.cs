using Microsoft.AspNetCore.Mvc;
using SmartDevice.Datas;
using SmartDevice.DTOs;
using SmartDevice.Models;

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

    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPurchaseInvoiceById(string id)
    {
        var salesInvoice = await _context.PurchaseInvoices.FindAsync(id);
        if (salesInvoice == null)
        {
            return NotFound(new { message = "Sale invoice not found." });
        }
        return Ok(salesInvoice);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddPurchaseInvoice([FromBody] PurchaseInvoiceDto purchaseInvoiceDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var newPurchaseInvoice = new PurchaseInvoice
            {
                PurchaseInvoiceId = Guid.NewGuid().ToString(),
                InvoiceDate = purchaseInvoiceDto.InvoiceDate,
                TotalAmount = purchaseInvoiceDto.TotalAmount,
                EmployeeId = purchaseInvoiceDto.EmployeeId
            };

            _context.PurchaseInvoices.Add(newPurchaseInvoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPurchaseInvoiceById), new { id = newPurchaseInvoice.PurchaseInvoiceId }, newPurchaseInvoice);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while adding the purchase invoice.", error = ex.Message });
        }
    }
}