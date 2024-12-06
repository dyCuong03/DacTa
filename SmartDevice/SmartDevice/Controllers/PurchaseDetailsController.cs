using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartDevice.Datas;
using SmartDevice.DTOs;
using SmartDevice.Models;

namespace SmartDevice.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PurchaseDetailsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PurchaseDetailsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddPurchaseDetail([FromBody] PurchaseDetailsDto purchaseDetailsDto)
    {
        var newCartDetail = new PurchaseDetails
        {
            PurchaseInvoiceId = purchaseDetailsDto.PurchaseInvoiceId,   
            ProductId = purchaseDetailsDto.ProductId,
            Amount = purchaseDetailsDto.Amount,
            SupplierId = purchaseDetailsDto.SupplierId
        };
        await _context.PurchaseDetails.AddAsync(newCartDetail);
        return await _context.SaveChangesAsync() > 0
            ? StatusCode(StatusCodes.Status200OK)
            : StatusCode(StatusCodes.Status500InternalServerError);
    }
    
    
    [HttpPut]
    public async Task<IActionResult> UpdatePurchaseDetails([FromBody] PurchaseDetailsDto purchaseDetailsDto)
    {
        var quantityProduct = _context.Products.SingleOrDefault(f => f.ProductId == purchaseDetailsDto.ProductId);
        if (quantityProduct is not { Quantity: > 0 })
        {
            return StatusCode(StatusCodes.Status409Conflict);
        }
        var existCartDetail = _context.PurchaseDetails.FirstOrDefault(x => x.PurchaseInvoiceId == purchaseDetailsDto.PurchaseInvoiceId && x.ProductId == purchaseDetailsDto.ProductId);
        if (existCartDetail == null)
        {
            return StatusCode(StatusCodes.Status404NotFound);
        }
        if (quantityProduct.Quantity < purchaseDetailsDto.Amount)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "ko du");
        }

        existCartDetail.Amount = purchaseDetailsDto.Amount;

        return await _context.SaveChangesAsync() > 0 
            ? StatusCode(StatusCodes.Status200OK) 
            : StatusCode(StatusCodes.Status500InternalServerError);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAlPurchaseDetail(string purchaseInvoiceId)
    {
        var allCartDetails = await _context.PurchaseDetails
            .Where(f => f.PurchaseInvoiceId == purchaseInvoiceId)
            .Select(od => new
            {
                od.PurchaseInvoiceId,
                od.ProductId,
                od.Product.ProductName,
                od.Amount,
                od.Product.SalePrice,
                od.Product.Image
            }).ToListAsync();
        return Ok(allCartDetails);
    }
    
    [HttpDelete] 
    public async Task<IActionResult> DeleteAllPurchaseDetail(string purchaseInvoiceId)
    {
        var orderDetails = _context.PurchaseDetails.Where(x => x.PurchaseInvoiceId == purchaseInvoiceId).ToList();
        _context.PurchaseDetails.RemoveRange(orderDetails);

        return await _context.SaveChangesAsync() > 0 
            ? StatusCode(StatusCodes.Status200OK) 
            : StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePurchaseDetails(string purchaseInvoiceId, string productId)
    {
        var existOrderDetail = _context.PurchaseDetails.FirstOrDefault(x => x.PurchaseInvoiceId == purchaseInvoiceId && x.ProductId == productId);
        if(existOrderDetail == null) return StatusCode(StatusCodes.Status404NotFound);
        _context.PurchaseDetails.Remove(existOrderDetail);
        return await _context.SaveChangesAsync()  > 0 ?  StatusCode(StatusCodes.Status201Created) : StatusCode(StatusCodes.Status500InternalServerError);
    }
}