namespace Infrastructure.Minio.Interfaces;

public interface IMinioService
{
    public Task UploadFile(string objectName, Stream fileStream);
    public Task<Stream?> DownloadFile(string objectName);
    public Task DeleteFile(string objectName);
}
