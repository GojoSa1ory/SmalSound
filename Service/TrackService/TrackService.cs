using AutoMapper;
using KPCourseWork.Data;
using KPCourseWork.Dto.TrackDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KPCourseWork.Service.TrackService;

public class TrackService: ITrackService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileUploadService _fileService;

    public TrackService (AppDbContext context, IMapper mapper, IFileUploadService fileService)
    {
        _context = context;
        _mapper = mapper;
        _fileService = fileService;
    }
    
    public async Task<ServiceResponse<GetTrackDto>> UploadTrack(SetTrackDto track, int userId)
    {
        ServiceResponse<GetTrackDto> response = new();

        try
        {
            TrackModel checkTrack = _context.Tracks.FirstOrDefault(t => t.Name == track.Name);
            UserModel user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (checkTrack is not null) throw new Exception("Track already exist");

            string imagePath = await _fileService.UploadFile("TrackImage", track.TrackImage);
            string trackPath = await _fileService.UploadFile("Track", track.Track);

            TrackModel result = _mapper.Map<TrackModel>(track);
            result.TrackImage = imagePath;
            result.Track = trackPath;
            result.User = user;
            user.Tracks = new List<TrackModel>{result};

            _context.Tracks.Add(result);
            await _context.SaveChangesAsync();
            
            response.Data = _mapper.Map<GetTrackDto>(result);
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Success = false;
        }

        return response;
    }

    public async Task<ServiceResponse<List<GetTrackDto>>> GetAllTracks()
    {
        ServiceResponse<List<GetTrackDto>> response = new ServiceResponse<List<GetTrackDto>>();

        response.Data = _context.Tracks
            .Include(t => t.User)
            .Select(t => _mapper.Map<GetTrackDto>(t))
            .ToList();
        
        return response;
    }

    public async Task<ServiceResponse<List<GetTrackDto>>> GetAllUserTracks(int userId)
    {
        ServiceResponse<List<GetTrackDto>> response = new();

        try
        {
            var tracks = _context.Tracks
                .Include(t => t.User)    
                .Select(t => _mapper.Map<GetTrackDto>(t.User.Id == userId))
                .ToList();

            if (tracks.IsNullOrEmpty()) throw new Exception("You don't have any upload tracks");

            response.Data = tracks;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
        }
        
        return response;
    }

    public async Task<ServiceResponse<GetTrackDto>> GetOneTrack(int trackId)
    {
        ServiceResponse<GetTrackDto> response = new();

        try
        {
            var track = _context.Tracks
                .Include(t => t.User)
                .FirstOrDefault(t => t.Id == trackId);

            if (track is null) throw new Exception("Track not Found");

            response.Data = _mapper.Map<GetTrackDto>(track);
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
        }
        
        return response;
    }
}