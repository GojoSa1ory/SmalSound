using AutoMapper;
using KPCourseWork.Data;
using KPCourseWork.Dto;
using KPCourseWork.Dto.TrackDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace KPCourseWork.Service.TrackService;

public class TrackService : ITrackService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileUploadService _fileService;

    public TrackService(AppDbContext context, IMapper mapper, IFileUploadService fileService)
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
            GenreModel genre = _context.Genre.FirstOrDefault(t => t.Id == track.GenreId);

            if (checkTrack is not null) throw new Exception("Track already exist");
            if (user is null) throw new Exception("User not found");
            if (genre is null) throw new Exception("Genre not found");

            string imagePath = await _fileService.UploadFile("TrackImage", track.TrackImage);
            string trackPath = await _fileService.UploadFile("Track", track.Track);

            TrackModel result = _mapper.Map<TrackModel>(track);
            result.TrackImage = imagePath;
            result.Track = trackPath;
            result.User = user;
            result.Genre = genre;
            result.createdAt = DateTime.Now;
            result.updatedAt = DateTime.Now;


            user.Tracks = new List<TrackModel> { result };
            genre.Tracks = new List<TrackModel> { result};

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
            .Include(t => t.Genre)
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
                .Include(t => t.Genre)
                .Where(t => t.User.Id == userId)
                .ToList();

            if (tracks.IsNullOrEmpty()) throw new Exception("You don't have any upload tracks");

            response.Data = tracks.Select(t => _mapper.Map<GetTrackDto>(t)).ToList();
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
                .Include(t => t.Genre)
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

    public async Task<ServiceResponse<GetTrackDto>> UpdateTrack (UpdateTrackDto track, int trackId, int userId) {
        ServiceResponse<GetTrackDto> response = new ();

        try
        {
            var dbTrack = _context.Tracks.FirstOrDefault(t => t.Id == trackId && t.User.Id == userId);
            var genre = _context.Genre.FirstOrDefault(g => g.Id == track.GenreId);

            if(dbTrack is null) throw new Exception("Track not found");
            if(genre is null) throw new Exception("Genre not found");

             string imagePath = await _fileService.UploadFile("TrackImage", track.TrackImage);

            dbTrack.Name = track.Name != dbTrack.Name ? track.Name : dbTrack.Name;
            dbTrack.Genre = dbTrack.Genre != genre ? genre : dbTrack.Genre;
            dbTrack.TrackImage = dbTrack.TrackImage != imagePath ? imagePath : dbTrack.TrackImage;
            dbTrack.updatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            response.Data = _mapper.Map<GetTrackDto>(dbTrack);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }
    
    public async Task<ServiceResponse<string>> UpdateListeningCount(int trackId) 
    {
      ServiceResponse<string> response = new ();

      try
      {
          var track = await _context.Tracks.FindAsync(trackId);
          
          if(track is null) throw new Exception("Track not found");

          track.ListenCount += 1;

          await _context.SaveChangesAsync();
           
      }
      catch (Exception e)
      {
          response.Success = false;
          response.Message = e.Message;
          throw;
      }

      return response;
    }

    public async Task<ServiceResponse<List<GetGenreDto>>> GetGenres () {
        ServiceResponse<List<GetGenreDto>> response = new();

        try {

            var genres = _context.Genre.Select(g => _mapper.Map<GetGenreDto>(g)).ToList();

            if(genres is null) throw new Exception("No genres");

            response.Data = genres;

        } catch (Exception ex) {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<ServiceResponse<string>> DeleteTrack(int trackId)
    {
        ServiceResponse<string> response = new();

        try
        {
            var track = _context.Tracks.FirstOrDefault(t => t.Id == trackId);

            if (track is null) throw new Exception("Track not found");

            _context.Tracks.Remove(track);

            await _context.SaveChangesAsync();

            response.Data = "done";

        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
        }

        return response;
    }
}
