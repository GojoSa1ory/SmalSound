using System.ComponentModel.DataAnnotations.Schema;

namespace KPCourseWork.Models;

[Table("role")]
public class RoleModel
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    public List<UserModel>? User { get; set; }
}