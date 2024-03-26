namespace CourseWork.Services;

public interface ITrackUploadService
{
    Task<ServiceResponse<GetTrackDto>> PostTrack(SetTrackDto track);
    Task<ServiceResponse<GetTrackDto>> GetTrack(int id);
}
