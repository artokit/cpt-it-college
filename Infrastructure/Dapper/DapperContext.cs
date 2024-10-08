using System.Data;
using Dapper;
using Infrastructure.Dapper.Interfaces;
using Infrastructure.Dapper.Interfaces.Settings;
using Infrastructure.Interfaces;
using Npgsql;

namespace Infrastructure.Dapper;

public class DapperContext : IDapperContext
{
    private IDapperSettings dapperSettings;
    
    public DapperContext(IDapperSettings dapperSettings)
    {
        this.dapperSettings = dapperSettings;
    }
    
    public async Task<T?> FirstOrDefault<T>(IQueryObject queryObject)
    {
        return await Execute(query => query.QueryFirstOrDefaultAsync<T>(queryObject.Sql, queryObject.Parameters));
    }

    public async Task<List<T>> ListOrEmpty<T>(IQueryObject queryObject)
    {
        return (await Execute(query => query.QueryAsync<T>(queryObject.Sql, queryObject.Parameters))).ToList();
    }

    public async Task Command(IQueryObject queryObject)
    {
        await Execute(query => query.ExecuteAsync(queryObject.Sql, queryObject.Parameters));
    }

    public async Task<T> CommandWithResponse<T>(IQueryObject queryObject)
    {
        return await Execute(query => query.QueryFirstAsync<T>(queryObject.Sql, queryObject.Parameters));
    }
    
    public async Task<List<TResult>> QueryWithJoin<T1, T2, TResult>(
        IQueryObject queryObject,
        Func<T1, T2, TResult> map,
        string splitOn = "Id")
    {
        return (await Execute(async query =>
        {
            var result = await query.QueryAsync(
                queryObject.Sql,
                map,
                queryObject.Parameters,
                splitOn: splitOn
            );

            return result.ToList();
        }));
    }
    
    private async Task<T> Execute<T>(Func<IDbConnection, Task<T>> query)
    {
        await using var connection = new NpgsqlConnection(dapperSettings.ConnectionString);
        var res = await query(connection);
        await connection.CloseAsync();
        return res;
    }
}
