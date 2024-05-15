using KPCourseWork.Dto.PlaylistDto;

namespace KPCourseWork.Service.PlaylistService;

public interface IPlaylistService
{
    Task<ServiceResponse<GetPlaylistDto>> CreatePlaylist(SetPlaylistDto newPlaylist, int userId);
    Task<ServiceResponse<List<GetPlaylistDto>>> GetPlaylists();
    Task<ServiceResponse<GetPlaylistDto>> GetPlaylist(int id, int userId);
    
    Task<ServiceResponse<GetPlaylistDto>> AddTrack(int id, int userId, int trackId);
    
    Task<ServiceResponse<GetPlaylistDto>> RemoveTrack(int id, int userId, int trackId);
    Task<ServiceResponse<GetPlaylistDto>> UpdatePlaylist(UpdatePlaylistDto newPlaylist, int id, int userId);
    Task<ServiceResponse<string>> RemovePlaylist(int id, int userId);
}