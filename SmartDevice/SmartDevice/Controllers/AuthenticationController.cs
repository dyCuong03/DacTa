using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartDevice.Datas;
using SmartDevice.DTOs;
using SmartDevice.Models;

namespace SmartDevice.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationDbContext _context;

    public AuthenticationController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInContext, ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInContext;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var existingUser = await _userManager.FindByNameAsync(registerDto.Username);
        if (existingUser != null) return BadRequest("Username already exists");
        var newUser = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = registerDto.Username,
        };
        await _userManager.CreateAsync(newUser, registerDto.Password);
        return Ok(newUser.Id);
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username);
        if (user == null) return BadRequest("Invalid username");
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (result.Succeeded)
        {
            return Ok();
        }
        return BadRequest("Invalid username or password");
    }
}