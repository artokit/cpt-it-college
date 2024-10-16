using Application.DTO;
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
            ImageName = dbImage.ImageName,
            CreatedAt = dbImage.CreatedAt
        };
    }

    public static ImageResponseDto MapToDto(this Image image)
    {
        return new ImageResponseDto
        {
            Id = image.Id, ImageUrl = $"http://localhost:9000/bucket/{image.ImageName}", CreatedAt = image.CreatedAt
        };
    }
    
    public static List<ImageResponseDto> MapToDto(this List<Image> images)
    {
        return images.Select(image => image.MapToDto()).ToList();
    }

    public static List<Image> MapToDomain(this List<DbImage> dbImages)
    {
        return dbImages.Select(i => i.MapToDomain()).ToList();
    }
}
