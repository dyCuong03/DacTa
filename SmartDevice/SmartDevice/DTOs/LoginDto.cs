using SmartDevice.Models.Interfaces;

namespace SmartDevice.DTOs;

public class LoginDto :IUser
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}