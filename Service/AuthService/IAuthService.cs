namespace KPCourseWork.Services;

public interface IAuthService {
    Task<ServiceResponse<AuthDto>> Register (SetUserDto user);
    Task<ServiceResponse<AuthDto>> Login (SetAuthUserDto user);
    Task<ServiceResponse<RoleModel>> CreateRole(RoleModel role);
}
