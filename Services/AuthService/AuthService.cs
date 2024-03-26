
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CourseWork.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext context;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;


    public AuthService(AppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        this.context = context;
        this.mapper = mapper;
        this.configuration = configuration;
    }

    public async Task<ServiceResponse<GetUserDto>> Login(SetUserDto user)
    {
        var response = new ServiceResponse<GetUserDto>();

        try
        {

            var dbUser = this.context.Users.FirstOrDefault(u => u.Name == user.Name);

            if (dbUser is null)
                throw new Exception("User not found");

            if (!BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password))
                throw new Exception("Password is invalid");

            string token = CreateToken(dbUser);

            response.Data = this.mapper.Map<GetUserDto>(dbUser);
            response.Data.Token = token;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return (response);
    }

    public async Task<ServiceResponse<GetUserDto>> Register(SetUserDto user)
    {
        var response = new ServiceResponse<GetUserDto>();

        try
        {
            var check = this.context.Users.FirstOrDefault(u => u.Name == user.Name);

            if (check is not null)
                throw new Exception("This user is already exist");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var userModel = this.mapper.Map<UserModel>(user);

            userModel.Password = passwordHash;

            this.context.Users.Add(userModel);
            await this.context.SaveChangesAsync();

            response.Data = this.mapper.Map<GetUserDto>(userModel);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    private string CreateToken(UserModel user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Auth:KEY").Value!));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var jwt = new JwtSecurityToken(
                    issuer: configuration.GetSection("Auth:ISSUER").Value,
                    audience: configuration.GetSection("Auth:AUDIENCE").Value,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: cred
                  );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
