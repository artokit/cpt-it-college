using Infrastructure.Dapper.Interfaces;
using Infrastructure.Interfaces;

namespace Infrastructure.Dapper;

public class QueryObject : IQueryObject
{
    public QueryObject(string sql, object parameters)
    {
        if (string.IsNullOrEmpty(sql))
        {
            throw new ArgumentException("sql is missing");
        }
        
        Sql = sql;
        Parameters = parameters;
    }
    
    public string Sql { get; }
    public object Parameters { get; }
}