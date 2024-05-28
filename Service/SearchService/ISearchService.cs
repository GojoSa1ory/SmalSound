using KPCourseWork.Dto;

namespace KPCourseWork.Service;

public interface ISearchService
{
    Task<ServiceResponse<SearchResponseDto>> Search(string request);
    Task<ServiceResponse<SearchResponseDto>> SortTrack(string request);
    Task<ServiceResponse<SearchResponseDto>> FilterTrack(int genreId);
}