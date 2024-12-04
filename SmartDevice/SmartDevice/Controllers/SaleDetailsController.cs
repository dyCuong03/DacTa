using Microsoft.AspNetCore.Mvc;
using SmartDevice.Datas;

namespace SmartDevice.Controllers;

public class SaleDetailsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SaleDetailsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // [HttpPost]
    // public async Task<IActionResult> AddSaleDetail(OrderDetailDTO orderDetail)
    // {
    //     bool orderExists = _context.OrderDetails.Any(f => f.ProductId == orderDetail.ProductId);
    //     if (orderExists) return StatusCode(StatusCodes.Status500InternalServerError);
    //     bool isAvailable = _context.Products.Any(f => f.ProductId == orderDetail.ProductId && f.Quantity > 0);
    //     if (!isAvailable) return StatusCode(StatusCodes.Status409Conflict);
    //     var newCartDetail = new OrderDetail
    //     {
    //         OrderId = orderDetail.OrderId,
    //         ProductId = orderDetail.ProductId,
    //         Quantity = orderDetail.Quantity,
    //     };
    //     return Ok();
    // }
}