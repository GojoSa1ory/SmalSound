namespace KPCourseWork.Service;

public class FileUploadService: IFileUploadService
{
    public async Task<string> UploadFile(string fileType, IFormFile file)
    {

        string filePath = "";
        
        try
        {
            if (file is null) throw new Exception("File not found");
            
            var uploadImagePath = $"./Uploads/{fileType}";
            string fullImagePath = $"{uploadImagePath}/{file.FileName}";

            using (var fileStream = new FileStream(fullImagePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var baseUrl = "http://localhost:5024/FileUpload/file/get?path=";
            var imageRelativePath = $"Uploads/{fileType}/{file.FileName}";
            var imageUri = new Uri(baseUrl + imageRelativePath);

            filePath = imageUri.ToString();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return filePath;
    }
}