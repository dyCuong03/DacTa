using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartDevice.Datas;
using SmartDevice.DTOs;
using SmartDevice.Models;

namespace SmartDevice.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class SaleDetailsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SaleDetailsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddSaleDetail([FromBody] SaleDetailsDto saleDetailsDto)
    {
        var orderExists = _context.SalesDetails.SingleOrDefault(f => f.ProductId == saleDetailsDto.ProductId);
        if (orderExists != null) return StatusCode(StatusCodes.Status409Conflict);
        var product = _context.Products.SingleOrDefault(f => f.ProductId == saleDetailsDto.ProductId);
            
        if (product == null || product.Quantity < 0 ) return StatusCode(StatusCodes.Status404NotFound);
            
        if (saleDetailsDto.Amount > product.Quantity) return StatusCode(StatusCodes.Status400BadRequest);

        var newCartDetail = new SalesDetails
        {
            SalesInvoiceId = saleDetailsDto.SalesInvoiceId,
            ProductId = saleDetailsDto.ProductId,
            Amount = saleDetailsDto.Amount,
        };
        await _context.SalesDetails.AddAsync(newCartDetail);
        return await _context.SaveChangesAsync()  > 0 ?  StatusCode(StatusCodes.Status200OK) : StatusCode(StatusCodes.Status500InternalServerError);
    }
    
      [HttpPut]
        public async Task<IActionResult> UpdateSaleDetail([FromBody] SaleDetailsDto saleDetailsDto)
        {
            var quantityProduct = _context.Products.SingleOrDefault(f => f.ProductId == saleDetailsDto.ProductId);
            if (quantityProduct is not { Quantity: > 0 })
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }
            var existCartDetail = _context.SalesDetails.FirstOrDefault(x => x.SalesInvoiceId == saleDetailsDto.SalesInvoiceId && x.ProductId == saleDetailsDto.ProductId);
            if (existCartDetail == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            if (quantityProduct.Quantity < saleDetailsDto.Amount)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "ko du");
            }

            existCartDetail.Amount = saleDetailsDto.Amount;

            return await _context.SaveChangesAsync() > 0 
                ? StatusCode(StatusCodes.Status200OK) 
                : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSaleDetail(string saleInvoiceId)
        {
            var allCartDetails = await _context.SalesDetails
                .Where(f => f.SalesInvoiceId == saleInvoiceId)
                .Select(od => new
                {
                    od.SalesInvoiceId,
                    od.ProductId,
                    od.Product.ProductName,
                    od.Amount,
                    od.Product.SalePrice,
                    od.Product.Image
                }).ToListAsync();
            return Ok(allCartDetails);
        }

        [HttpDelete] 
        public async Task<IActionResult> DeleteAllSaleDetail(string saleInvoiceId)
        {
            var orderDetails = _context.SalesDetails.Where(x => x.SalesInvoiceId == saleInvoiceId).ToList();
            _context.SalesDetails.RemoveRange(orderDetails);

            return await _context.SaveChangesAsync() > 0 
                ? StatusCode(StatusCodes.Status200OK) 
                : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSaleDetails(string saleInvoiceId, string productId)
        {
            var existOrderDetail = _context.SalesDetails.FirstOrDefault(x => x.SalesInvoiceId == saleInvoiceId && x.ProductId == productId);
            if(existOrderDetail == null) return StatusCode(StatusCodes.Status404NotFound);
            _context.SalesDetails.Remove(existOrderDetail);
            return await _context.SaveChangesAsync()  > 0 ?  StatusCode(StatusCodes.Status201Created) : StatusCode(StatusCodes.Status500InternalServerError);
        }
}