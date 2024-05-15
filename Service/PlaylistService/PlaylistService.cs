using AutoMapper;
using KPCourseWork.Data;
using KPCourseWork.Dto.PlaylistDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KPCourseWork.Service.PlaylistService;

public class PlaylistService: IPlaylistService
{

    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileUploadService _fileService;
    
    public PlaylistService(AppDbContext context, IMapper mapper, IFileUploadService fileService)
    {
        _context = context;
        _mapper = mapper;
        _fileService = fileService;
    }
    
    public async Task<ServiceResponse<GetPlaylistDto>> CreatePlaylist(SetPlaylistDto newPlaylist, int userId)
    {
        ServiceResponse<GetPlaylistDto> response = new();
            
        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            var check = _context.Playlists.FirstOrDefault(p => p.User.Id == userId && p.Name == newPlaylist.Name);

            if (user is null) throw new Exception("User not found");
            if (check is not null) throw new Exception("Playlist with the same name is already exist");

            PlaylistModel result = _mapper.Map<PlaylistModel>(newPlaylist);
            result.User = user;

            _context.Add(result);
            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetPlaylistDto>(result);
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
        }

        return response;
    }

    public async Task<ServiceResponse<List<GetPlaylistDto>>> GetPlaylists()
    {
        ServiceResponse<List<GetPlaylistDto>> response = new();

        try
        {
            var playlist = _context.Playlists.Select(p => _mapper.Map<GetPlaylistDto>(p)).ToList();

            if (playlist.IsNullOrEmpty()) throw new Exception("Your don't have any playlists");

            response.Data = playlist;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
        }
        
        return response;
    }

    public async Task<ServiceResponse<GetPlaylistDto>> GetPlaylist(int id, int userId)
    {
        ServiceResponse<GetPlaylistDto> response = new();

        try
        {
            var playlist = _context.Playlists
                .Include(p => p.Tracks)
                .FirstOrDefault(p => p.Id == id && p.User.Id == userId);

            if (playlist is null) throw new Exception("Playlist not found");

            response.Data = _mapper.Map<GetPlaylistDto>(playlist);
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
        }
        
        return response;
    }

    public async Task<ServiceResponse<GetPlaylistDto>> AddTrack(int id, int userId, int trackId)
    {
        var response = new ServiceResponse<GetPlaylistDto>();

        try
        {
            var playlist = _context.Playlists.FirstOrDefault(p => p.Id == id && p.User.Id == userId);
            var track = _context.Tracks.FirstOrDefault(t => t.Id == trackId);
            
            if (playlist is null) throw new Exception("Playlist not found");
            if (track is null) throw new Exception("Track not found");

            playlist.Tracks = new List<TrackModel>() { track };

            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetPlaylistDto>(playlist);
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
        }

        return response;
    }

    public async Task<ServiceResponse<GetPlaylistDto>> RemoveTrack(int id, int userId, int trackId)
    {
        var response = new ServiceResponse<GetPlaylistDto>();

        try
        {
            var playlist = _context.Playlists.FirstOrDefault(p => p.Id == id && p.User.Id == userId);
            var track = _context.Tracks.FirstOrDefault(t => t.Id == trackId);
            
            if (playlist is null) throw new Exception("Playlist not found");
            if (track is null) throw new Exception("Track not found");

            playlist.Tracks.Remove(track);

            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetPlaylistDto>(playlist);
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
        }

        return response;
    }

    public async Task<ServiceResponse<GetPlaylistDto>> UpdatePlaylist(UpdatePlaylistDto newPlaylist, int id, int userId)
    {
        ServiceResponse<GetPlaylistDto> response = new();

        try
        {
            var playlist = _context.Playlists.FirstOrDefault(p => p.Id == id && p.User.Id == userId);
            
            if (playlist is null) throw new Exception("Playlist not found");

            playlist.Name = newPlaylist.Name != playlist.Name && newPlaylist.Name is not null && newPlaylist.Name != "" ? newPlaylist.Name : playlist.Name;

            if (newPlaylist.Image is not null)
            {
                string imagePath = await _fileService.UploadFile("Playlists", newPlaylist.Image);
                playlist.Image = imagePath;
            }
            
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
        }
        
        return response;
    }

    public async Task<ServiceResponse<string>> RemovePlaylist(int id, int userId)
    {
        ServiceResponse<string> response = new();

        try
        {
            var playlist = _context.Playlists.FirstOrDefault(p => p.Id == id && p.User.Id == userId);

            if (playlist is null) throw new Exception("Playlist not found");

            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();
            
            response.Data = "Done";
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
        }
        
        return response;
    }
}