namespace KPCourseWork.Services;

public interface IAuthService {
    Task<ServiceResponse<AuthDto>> Register (SetUserDto user);
    Task<ServiceResponse<AuthDto>> Login (SetAuthUserDto user);
    Task<ServiceResponse<GetUserDto>> VerifyUser(int userId);
    Task<ServiceResponse<RoleModel>> CreateRole(RoleModel role);
    
}
