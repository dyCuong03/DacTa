using Microsoft.AspNetCore.Mvc;
using SmartDevice.Datas;

namespace SmartDevice.Controllers;

public class PurchaseDetailsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PurchaseDetailsController(ApplicationDbContext context)
    {
        _context = context;
    }
}