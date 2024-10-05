namespace Infrastructure.Interfaces;

public interface IDapperContext
{
    public Task<T?> FirstOrDefault<T>(IQueryObject queryObject);
    public Task<List<T>> ListOrEmpty<T>(IQueryObject queryObject);
    public Task Command(IQueryObject queryObject);
    public Task<T> CommandWithResponse<T>(IQueryObject queryObject);
}