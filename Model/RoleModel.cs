namespace KPCourseWork.Models;

public class RoleModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<UserModel>? User { get; set; }
}