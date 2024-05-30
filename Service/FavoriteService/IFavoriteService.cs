using KPCourseWork.Dto.FavoriteDto;

namespace KPCourseWork.Service.FavoriteService;

public interface IFavoriteService
{
    Task<ServiceResponse<GetFavoriteDto>> GetFavorite(int userId);
    Task<ServiceResponse<GetFavoriteDto>> AddToFavorite(int userId, int trackId);
    Task<ServiceResponse<GetFavoriteDto>> RemoveFromFavorite(int userId, int trackId);
}