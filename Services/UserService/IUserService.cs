namespace CourseWork.Services;

public interface IUserService
{
    Task<ServiceResponse<List<GetUserDto>>> GetUsers();
    Task<ServiceResponse<GetUserDto>> GetUserByName(string name);
    Task<ServiceResponse<List<GetUserDto>>> RemoveUser(string name);
    Task<ServiceResponse<GetUserDto>> UpdateUser(SetUserDto updatedUser);
    Task<ServiceResponse<List<GetUserDto>>> PostUser(SetUserDto newUser);
}
