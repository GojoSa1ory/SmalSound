namespace KPCourseWork.Service;

public interface IFileUploadService
{
    Task<string> UploadFile(string fileType, IFormFile file);
}