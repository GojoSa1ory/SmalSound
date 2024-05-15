using System.ComponentModel.DataAnnotations;

namespace KPCourseWork.Dtos;

public class UpdateUserDto
{
    public string? Name {get; set;}
    [MinLength(8, ErrorMessage = 
        "Password length must be more or equal 8")]
    public string? Password {get;set;}
    [EmailAddress(ErrorMessage = "Email style is invalid")]
    public string? Email { get; set; }
    public IFormFile? ProfilePicture {get; set;}
}