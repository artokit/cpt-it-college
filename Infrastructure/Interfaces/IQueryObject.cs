namespace Infrastructure.Interfaces;

public interface IQueryObject
{
    public string Sql { get; }
    public object Parameters { get; }
}