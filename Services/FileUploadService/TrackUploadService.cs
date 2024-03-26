namespace CourseWork.Services;

public class TrackUploadService : ITrackUploadService
{
    private readonly IMapper mapper;
    private readonly AppDbContext context;

    public TrackUploadService(IMapper mapper, AppDbContext context)
    {
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<ServiceResponse<GetTrackDto>> PostTrack(SetTrackDto track)
    {

        var response = new ServiceResponse<GetTrackDto>();

        try
        {
            var imageFile = track.Image;
            var trackFile = track.Track;

            if (imageFile == null || trackFile == null)
                throw new Exception("error");

            var uploadImagePath = $"./uploads/images";
            var uploadTrackPath = $"./uploads/tracks";
            string fullImagePath = $"{uploadImagePath}/{imageFile.FileName}";
            string fullTrackPath = $"{uploadTrackPath}/{trackFile.FileName}";

            using (var fileStream = new FileStream(fullImagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            using (var fileStream = new FileStream(fullTrackPath, FileMode.Create))
            {
                await trackFile.CopyToAsync(fileStream);
            }

            var baseUrl = "http://localhost:5098/api/file/TrackUpload?path=";
            var imageRelativePath = $"uploads/images/{imageFile.FileName}";
            var trackRelativePath = $"uploads/tracks/{trackFile.FileName}";
            var imageUri = new Uri(baseUrl + imageRelativePath);
            var trackUri = new Uri(baseUrl + trackRelativePath);

            var result = new TrackModel
            {
                ArtistName = track.ArtistName,
                AuditionCount = track.AuditionCount,
                Image = imageUri.ToString(),
                Track = trackUri.ToString(),
                TrackName = track.TrackName,
                UserId = Convert.ToInt32(track.UserId)
            };

            this.context.Tracks.Add(result);
            await this.context.SaveChangesAsync();

            response.Data = this.mapper.Map<GetTrackDto>(result);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<ServiceResponse<GetTrackDto>> GetTrack(int id)
    {
        var response = new ServiceResponse<GetTrackDto>();

        try
        {

            var track = this.context.Tracks.FirstOrDefault(t => t.ID == id);

            if (track is null)
                throw new Exception("Track not found");

            response.Data = this.mapper.Map<GetTrackDto>(track);

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
