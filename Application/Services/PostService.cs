using Application.DTO;
using Application.Exceptions;
using Application.Interfaces.Services;
using Application.Mappers;
using Domain.Enums;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Minio.Interfaces;
using Infrastructure.Models;

namespace Application.Services;

public class PostService : IPostService
{
    private readonly IPostRepository postRepository;
    private readonly IMinioService minioService;
    
    public PostService(IPostRepository postRepository, IMinioService minioService)
    {
        this.postRepository = postRepository;
        this.minioService = minioService;
    }
    
    public async Task<List<PostResponseDto>> GetPostsForReader()
    {
        return (await postRepository.GetAllPublishedPosts()).MapToDomain().MapToDto();
    }

    public async Task<List<PostResponseDto>> GetPostsForAuthor(int userId)
    {
        return (await postRepository.GetAllPostsByUserId(userId)).MapToDomain().MapToDto();
    }

    public async Task<PostResponseDto> AddPost(int authorId, AddNewPostRequestDto addNewPostRequestDto)
    {
        if (await postRepository.GetPostByIdempotencyKey(addNewPostRequestDto.IdempotencyKey) != null)
        {
            throw new IdempotencyKeyConflictException("Пост с данным ключом уже существует");
        }
        
        return (await postRepository.AddPost(
            new DbPost
            {
                Content = addNewPostRequestDto.Content,
                Title = addNewPostRequestDto.Title,
                AuthorId = authorId,
                IdempotencyKey = addNewPostRequestDto.IdempotencyKey
            })).MapToDomain().MapToDto();
    }

    public async Task UpdatePost(int userId, int postId, EditPostRequestDto editPostRequestDto)
    {
        var post = await GetPostByIdOrException(postId);

        if (post.AuthorId != userId)
        {
            throw new PostUpdateForbiddenException("Вы не можете редактировать данный пост");
        }
        
        await postRepository.UpdatePost(new DbPost
        {
            Id = postId, Content = editPostRequestDto.Content, Title = editPostRequestDto.Title
        });
    }

    public async Task PublishPost(int userId, int postId, ChangePostStatusDto changePostStatusDto)
    {
        var res = await GetPostByIdOrException(postId);
        if (changePostStatusDto.Status.ToLower() != PostStatus.Published.ToString().ToLower())
        {
            throw new InvalidUpdatePostStatusException("Неверное значение статуса");
        }

        if (res.AuthorId != userId)
        {
            throw new PostUpdateForbiddenException("Вы не можете редактировать данный пост");
        }

        await postRepository.PublishPost(postId);
    }

    public async Task<PostResponseDto> AddImageToPost(int postId, string objectName, Stream image)
    {
        var post = await GetPostByIdOrException(postId);
        
        var uniqueObjectName =  $"{Guid.NewGuid()}-{objectName}";
        await minioService.UploadFile(uniqueObjectName, image);
        var dbImage = await postRepository.AddImageToPost(postId, uniqueObjectName);
        var res = post.MapToDomain();
        res.Images.Add(dbImage.MapToDomain());
        return res.MapToDto();
    }

    public async Task DeleteImageFromPost(int postId, int imageId, int userId)
    {
        var post = await GetPostByIdOrException(postId);

        if (post.AuthorId != userId)
        {
            throw new PostUpdateForbiddenException("Вы не можете удалить фотографии в данном посте");
        }

        var image = post.Images?.FirstOrDefault(image => image.Id == imageId);
        if (image == null)
        {
            throw new PostImageNotFoundException("Фотография поста не найдена");
        }
        
        await postRepository.DeleteImage(imageId);
        await minioService.DeleteFile(image.ImageName);
    }

    private async Task<DbPost> GetPostByIdOrException(int postId)
    {
        var post = await postRepository.GetPostById(postId);
        if (post == null)
        {
            throw new PostNotFoundException("Пост не найден");
        }

        return post;
    }
}
