using AutoMapper;
using KPCourseWork.Data;
using KPCourseWork.Dto.FavoriteDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KPCourseWork.Service.FavoriteService;

public class FavoriteService : IFavoriteService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public FavoriteService(IMapper mapper, AppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResponse<GetFavoriteDto>> GetFavorite(int userId)
    {
        ServiceResponse<GetFavoriteDto> response = new();

        try
        {
            var favorite = await _context.Favorite
                .Include(f => f.User)
                .Include(f => f.Tracks)
                .ThenInclude(t => t.Genre)
                .Include(f => f.Tracks)
                .ThenInclude(t => t.User)
                .FirstOrDefaultAsync(f => f.User.Id == userId);

            if (favorite == null || favorite.Tracks == null || !favorite.Tracks.Any())
            {
                throw new Exception("No tracks");
            }

            response.Data = _mapper.Map<GetFavoriteDto>(favorite);
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }

        return response;
    }


    public async Task<ServiceResponse<GetFavoriteDto>> AddToFavorite(int userId, int trackId)
    {
        ServiceResponse<GetFavoriteDto> response = new();

        try
        {
            var favorites = _context.Favorite
                .Include(f => f.Tracks)
                .FirstOrDefault(f => f.User.Id == userId);

            var track = _context.Tracks
                .Include(t => t.Genre)
                .Include(t => t.User)
                .FirstOrDefault(t => t.Id == trackId);

            if (track is null) throw new Exception("Track not found");

            if (favorites is null)
            {
                FavoriteModel favorite = new FavoriteModel()
                {
                    Tracks = new List<TrackModel>() { track },
                    User = _context.Users.FirstOrDefault(u => u.Id == userId)
                };

                _context.Favorite.Add(favorite);
                response.Data = _mapper.Map<GetFavoriteDto>(favorite);
            }
            else
            {
                var checkTrack = favorites.Tracks.FirstOrDefault(t => t.Id == trackId);

                if (checkTrack is null)
                {
                    favorites.Tracks.Add(track);
                    response.Data = _mapper.Map<GetFavoriteDto>(favorites);
                }

                favorites.Tracks.Remove(checkTrack);
            }

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return response;
    }

    public async Task<ServiceResponse<GetFavoriteDto>> RemoveFromFavorite(int userId, int trackId)
    {
        throw new NotImplementedException();
    }
}