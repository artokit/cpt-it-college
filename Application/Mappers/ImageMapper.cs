using Domain;
using Infrastructure.Models;

namespace Application.Mappers;

public static class ImageMapper
{
    public static Image MapToDomain(this DbImage dbImage)
    {
        return new Image
        {
            Id = dbImage.Id,
            ImageUrl = $"http://localhost:9000/bucket/{dbImage.ImageName}",
            CreatedAt = dbImage.CreatedAt
        };
    }

    public static List<Image> MapToDomain(this List<DbImage> dbImages)
    {
        return dbImages.Select(i => i.MapToDomain()).ToList();
    }
}
