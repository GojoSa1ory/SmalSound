namespace KPCourseWork.Dto.FavoriteDto;

public class GetFavoriteDto
{
    public int Id { get; set; }
    public List<TrackModel> Tracks { get; set; }
    public List<UserModel> User { get; set; }
}