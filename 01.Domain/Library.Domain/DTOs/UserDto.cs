using Library.Domain.Enums;
namespace Library.Domain.DTOs;
public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public UserRole Role { get; set; }
    public decimal PenaltyAmount { get; set; }
}
