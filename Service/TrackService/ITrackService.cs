using KPCourseWork.Dto;
using KPCourseWork.Dto.TrackDto;

namespace KPCourseWork.Service.TrackService;

public interface ITrackService
{
    Task<ServiceResponse<GetTrackDto>> UploadTrack(SetTrackDto track, int userId);
    Task<ServiceResponse<List<GetTrackDto>>> GetAllTracks();
    Task<ServiceResponse<List<GetTrackDto>>> GetAllUserTracks(int userId);
    Task<ServiceResponse<GetTrackDto>> GetOneTrack(int trackId);
    Task<ServiceResponse<GetTrackDto>> UpdateTrack (UpdateTrackDto track, int trackId, int userId);
    Task<ServiceResponse<List<GetGenreDto>>> GetGenres();
    Task<ServiceResponse<string>> DeleteTrack(int trackId);
}
