using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using KPCourseWork.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KPCourseWork.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }
    
    public async Task<ServiceResponse<AuthDto>> Register(SetUserDto user)
    {
        ServiceResponse<AuthDto> response = new();

        try
        {
            var checkUser = _context.Users.FirstOrDefault(u => u.Email == user.Email || u.Name == user.Name);
            var role = _context.Roles.FirstOrDefault(r => r.Id == 3);
            
            if (checkUser is not null) throw new Exception("This email is already exist");
            
            var newUser = _mapper.Map<UserModel>(user);

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            newUser.Role = role;
            string token = CreateToken(newUser);
            
            _context.Users.Add(newUser);

            await _context.SaveChangesAsync();

            AuthDto authDto = new AuthDto
            {
                Token = token,
                User = _mapper.Map<GetUserDto>(newUser)
            };

            response.Data = authDto;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
        }
        
        return response;
    }

    public async Task<ServiceResponse<AuthDto>> Login(SetAuthUserDto user)
    {
        ServiceResponse<AuthDto> response = new();

        try
        {
            UserModel userDb = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Name == user.Name);

            if (user is null) throw new Exception("User not found");

            if (!BCrypt.Net.BCrypt.Verify(user.Password, userDb.Password)) 
                throw new Exception("Invalid password");

            var token = CreateToken(userDb);
            AuthDto authDto = new AuthDto
            {
                Token = token,
                User = _mapper.Map<GetUserDto>(userDb)
            };

            response.Data = authDto;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
        }
        
        return response;
    }

    public async Task<ServiceResponse<RoleModel>> CreateRole(RoleModel role)
    {
        ServiceResponse<RoleModel> response = new();

        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        
        response.Data = role;
        
        return response;
    }
    
    private string CreateToken(UserModel user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Auth:KEY").Value!));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var jwt = new JwtSecurityToken(
            issuer: _configuration.GetSection("Auth:ISSUER").Value,
            audience: _configuration.GetSection("Auth:AUDIENCE").Value,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
            signingCredentials: cred
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}