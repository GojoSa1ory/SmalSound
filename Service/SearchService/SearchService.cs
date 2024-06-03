using AutoMapper;
using KPCourseWork.Data;
using KPCourseWork.Dto;
using KPCourseWork.Dto.PlaylistDto;
using KPCourseWork.Dto.TrackDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KPCourseWork.Service;

public class SearchService: ISearchService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public SearchService(IMapper mapper, AppDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<ServiceResponse<SearchResponseDto>> Search(string request)
    {
        ServiceResponse<SearchResponseDto> response = new();

        try
        {
            var users = _context.Users
                .Where(u => u.Name.ToLower().Contains(request.ToLower()));
            
            var tracks = _context.Tracks
                .Include(t => t.User)
                .Where(t => t.Name.ToLower().Contains(request.ToLower()));

            var playlists = _context.Playlists
                .Where(p => p.Name.ToLower().Contains(request.ToLower()));

            if (users.IsNullOrEmpty() && tracks.IsNullOrEmpty() && playlists.IsNullOrEmpty()) throw new Exception("Nothing not found");

            SearchResponseDto result = new SearchResponseDto()
            {
                Tracks = !tracks.IsNullOrEmpty() ? tracks.Select(t => _mapper.Map<GetTrackDto>(t)).ToList() : null,
                User = !users.IsNullOrEmpty() ? users.Select(u => _mapper.Map<GetUserDto>(u)).ToList() : null,
                Playlists = !playlists.IsNullOrEmpty() ? playlists.Select(p => _mapper.Map<GetPlaylistDto>(p)).ToList() : null
            };

            response.Data = result;

        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Success = false;
        }

        return response;
    }

    public async Task<ServiceResponse<SearchResponseDto>> SortTrack(string request)
    {
        ServiceResponse<SearchResponseDto> response = new();

        try
        {
            switch (request)
            {
                case "dateTime":
                    var dateTime = _context.Tracks
                        .Include(t => t.User)
                        .Include(t => t.Genre)
                        .OrderBy(t => t.createdAt)
                        .ToList();

                    SearchResponseDto dateTimeResult = new()
                    {
                        Tracks = dateTime.Select(t => _mapper.Map<GetTrackDto>(t)).ToList(),
                        User = null
                    };
                    
                    response.Data = dateTimeResult;
                    
                    break;
                case "listeners":
                    
                    var listening = _context.Tracks
                        .Include(t => t.User)
                        .Include(t => t.Genre)
                        .OrderByDescending(t => t.ListenCount)
                        .ToList();
                    
                    SearchResponseDto listeningResult = new()
                    {
                        Tracks = listening.Select(t => _mapper.Map<GetTrackDto>(t)).ToList(),
                        User = null
                    };

                    response.Data = listeningResult;
                    
                    break;
                default:
                    throw new Exception("Invalid method");
                    break;
            }

        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
        }

        return response;
    }

    public async Task<ServiceResponse<SearchResponseDto>> FilterTrack(int genreId)
    {
        ServiceResponse<SearchResponseDto> response = new();

        try
        {
            var tracks = _context.Tracks
                .Include(t => t.Genre)
                .Include(t => t.User)
                .Where(t => t.Genre.Id == genreId);

            if (tracks.IsNullOrEmpty()) throw new Exception("Tracks not found");

            var result = new SearchResponseDto()
            {
                Tracks = tracks.Select(t => _mapper.Map<GetTrackDto>(t)).ToList(),
                User = null
            };
            
            response.Data = result;
            
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
        }
        
        return response;
    }
}