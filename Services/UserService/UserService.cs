using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Services;

public class UserService : IUserService
{

    private readonly IMapper mapper;
    private readonly AppDbContext context;

    public UserService(IMapper mapper, AppDbContext context)
    {
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<ServiceResponse<List<GetUserDto>>> GetUsers()
    {
        var serviceResponse = new ServiceResponse<List<GetUserDto>>();
        serviceResponse.Data = await this.context.Users.Select(u => this.mapper.Map<GetUserDto>(u)).ToListAsync();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetUserDto>> GetUserByName(string name)
    {
        var serviceResponse = new ServiceResponse<GetUserDto>();

        try
        {

            var user = await this.context.Users.FirstOrDefaultAsync(user => user.Name == name);

            if (user is null)
                throw new Exception($"User with ID {name} not found");

            serviceResponse.Data = this.mapper.Map<GetUserDto>(user);

        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetUserDto>>> PostUser(SetUserDto user)
    {
        var serviceResponse = new ServiceResponse<List<GetUserDto>>();

        try
        {
            var check = await this.context.Users.FirstOrDefaultAsync(u => u.Name == user.Name);

            if (check is not null)
                throw new Exception("This user is already exist");

            var newUser = this.mapper.Map<UserModel>(user);
            this.context.Users.Add(newUser);
            await this.context.SaveChangesAsync();

            serviceResponse.Data = await this.context.Users
            .Select(u => this.mapper.Map<GetUserDto>(u))
            .ToListAsync();
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetUserDto>> UpdateUser(SetUserDto updatedUser)
    {
        var serviceResponse = new ServiceResponse<GetUserDto>();

        try
        {

            var user = await this.context.Users.FirstOrDefaultAsync(u => u.Name == updatedUser.Name);

            if (user is null)
                throw new Exception($"User with name {updatedUser.Name} not found");

            user.Name = updatedUser.Name;
            user.ProfileImage = updatedUser.ProfileImage;
            user.Password = updatedUser.Password;

            await this.context.SaveChangesAsync();
            serviceResponse.Data = this.mapper.Map<GetUserDto>(user);

        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetUserDto>>> RemoveUser(string name)
    {
        var serviceResponse = new ServiceResponse<List<GetUserDto>>();

        try
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.Name == name);

            if (user is null)
                throw new Exception($"User with name {name} not found");

            this.context.Users.Remove(user);
            await this.context.SaveChangesAsync();

            serviceResponse.Data = await this.context.Users.Select(u => this.mapper.Map<GetUserDto>(u)).ToListAsync();
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }
}
