using System.ComponentModel.DataAnnotations;

namespace KPCourseWork.Dtos;

public class SetAuthUserDto
{
    public string Name { get; set; }
    [MinLength(8, ErrorMessage = "Password length must be more or equal then 8")]
    public string Password { get; set; }
}