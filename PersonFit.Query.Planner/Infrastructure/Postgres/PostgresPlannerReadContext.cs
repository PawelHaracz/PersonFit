namespace PersonFit.Query.Planner.Infrastructure.Postgres;
using System.Data;
using Dapper;
using Npgsql;
using Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonFit.Infrastructure.Postgres.Options;

internal class PostgresPlannerReadContext : IReadDbContext, IDisposable 
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly DbSetting _setting;
    private readonly IDbConnection _connection;
    public PostgresPlannerReadContext(IOptions<DbSetting> options, ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
        if (options.Value is not null)
        {
            _setting = options.Value;
        }
        else
        {
            throw new ArgumentNullException(nameof(options));
        }

        _connection = new NpgsqlConnection(_setting.ToString());
    }
       
    public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
    {
        return (await _connection.QueryAsync<T>(sql, param, transaction)).AsList();
    }
    public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
    {
        return await _connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
    }
    public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
    {
        return await _connection.QuerySingleAsync<T>(sql, param, transaction);
    }
    public void Dispose()
    {
        _loggerFactory.Dispose();
        _connection.Dispose();
    }
}