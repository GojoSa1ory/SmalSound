using KPCourseWork.Dto.TrackDto;

namespace KPCourseWork.Service.TrackService;

public interface ITrackService
{
    Task<ServiceResponse<GetTrackDto>> UploadTrack(SetTrackDto track, int userId);
    Task<ServiceResponse<List<GetTrackDto>>> GetAllTracks();
    Task<ServiceResponse<List<GetTrackDto>>> GetAllUserTracks(int userId);
    Task<ServiceResponse<GetTrackDto>> GetOneTrack(int trackId);
    Task<ServiceResponse<List<GetTrackDto>>> SearchTrack(string request);
    Task<ServiceResponse<List<GetTrackDto>>> SortTrack(string sortMethod);
    Task<ServiceResponse<string>> DeleteTrack(int trackId);
}