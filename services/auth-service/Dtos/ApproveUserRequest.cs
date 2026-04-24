using System.ComponentModel.DataAnnotations;
using auth_service.Models;

namespace auth_service.Dtos;

public class ApproveUserRequest
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    [EnumDataType(typeof(UserRole))]
    public UserRole Role { get; set; }
}
