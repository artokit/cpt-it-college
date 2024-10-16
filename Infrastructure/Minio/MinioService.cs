using Infrastructure.Minio.Interfaces;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.Minio;

public class MinioService : IMinioService
{
    private readonly IMinioSettings minioSettings;
    private readonly IMinioClient minioClient;
    
    public MinioService(IMinioSettings minioSettings)
    {
        this.minioSettings = minioSettings;
        minioClient = new MinioClient()
            .WithEndpoint(this.minioSettings.Endpoint)
            .WithCredentials(this.minioSettings.AccessKey, this.minioSettings.SecretKey)
            .WithSSL(this.minioSettings.Secure)
            .Build();
    }
    
    public async Task UploadFile(string objectName, Stream fileStream)
    {
        await minioClient.PutObjectAsync(
            new PutObjectArgs()
                .WithBucket(minioSettings.BucketName)
                .WithObject(objectName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length)
                .WithContentType("application/octet-stream")
        );
        
        await minioClient.StatObjectAsync(
            new StatObjectArgs().WithBucket(minioSettings.BucketName).WithObject(objectName));
    }

    public async Task DeleteFile(string objectName)
    {
        await minioClient.RemoveObjectAsync(
            new RemoveObjectArgs()
                .WithBucket(minioSettings.BucketName)
                .WithObject(objectName)
        );
    }
}
