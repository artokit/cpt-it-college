using Application.DTO;
using Application.Exceptions;
using Application.Interfaces.Services;
using Application.Mappers;
using Domain;
using Domain.Enums;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Minio.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class PostService : IPostService
{
    private IPostRepository postRepository;
    private readonly IMinioService minioService;
    
    public PostService(IPostRepository postRepository, IMinioService minioService)
    {
        this.postRepository = postRepository;
        this.minioService = minioService;
    }
    
    public async Task<PostsListResponse> GetPostsForReader()
    {
        return new PostsListResponse {Result = (await postRepository.GetAllPublishedPosts()).MapToDomain()};
    }

    public async Task<PostsListResponse> GetPostsForAuthor(int userId)
    {
        return new PostsListResponse {Result = (await postRepository.GetAllPostsByUserId(userId)).MapToDomain()};
    }

    public async Task<Post> AddPost(int authorId, AddNewPostRequestDto addNewPostRequestDto)
    {
        return (await postRepository.AddPost(
            new DbPost
            {
                Content = addNewPostRequestDto.Content, Title = addNewPostRequestDto.Title, AuthorId = authorId
            })).MapToDomain();
    }

    public async Task UpdatePost(int userId, int postId, EditPostRequestDto editPostRequestDto)
    {
        var post = await postRepository.GetPostById(postId);
        if (post is null)
        {
            throw new PostNotFoundException("Пост не найден");
        }

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
        var res = await postRepository.GetPostById(postId);
        if (changePostStatusDto.Status.ToLower() != PostStatus.Published.ToString().ToLower())
        {
            throw new InvalidUpdatePostStatusException("Неверное значение статуса");
        }

        if (res is null)
        {
            throw new PostNotFoundException("Пост не найден");
        }

        if (res.AuthorId != userId)
        {
            throw new PostUpdateForbiddenException("Вы не можете редактировать данный пост");
        }

        await postRepository.PublishPost(postId);
    }

    public async Task AddImageToPost(int postId, string objectName, Stream image)
    {
        if (await postRepository.GetPostById(postId) == null)
        {
            throw new PostNotFoundException("Пост не найден");
        }
        
        var uniqueObjectName =  $"{Guid.NewGuid()}-{objectName}";
        await minioService.UploadFile(uniqueObjectName, image);
    }
}
