using System.ComponentModel.DataAnnotations;

namespace KPCourseWork.Dtos;

public class UpdateUserDto
{
    public string? Name {get; set;}
    public string? Password {get;set;}
    [EmailAddress(ErrorMessage = "Email style is invalid")]
    public string? Email { get; set; }
    public IFormFile? ProfilePicture {get; set;}
}