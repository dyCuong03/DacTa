using SmartDevice.Models.Interfaces;

namespace SmartDevice.DTOs;

public class RegisterDto : IUser
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}