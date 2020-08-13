using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Helpers;
using Application.Interfaces;
using Dapper;
using Domain.Models;

namespace Persistence
{
  public class SqlLiteCustomerRepository : SqLiteBaseRepository, ICustomerRepository
  {
    public App GetApp(long id)
    {
      if (!File.Exists(DbFile)) return null;
      using var cnn = SimpleDbConnection();
      cnn.Open();
      var result = cnn.Query<App>(
        $@"SELECT AppUserModelId
            FROM App
            WHERE Id = @id", new { id }).FirstOrDefault();
      return result;
    }
    
    public List<App> GetAllApps()
    {
      if (!File.Exists(DbFile)) return null;
      using var cnn = SimpleDbConnection();
      cnn.Open();
      var sql = $@"SELECT Id, Name, Icon FROM App";
      var apps = cnn.Query<dynamic>(sql)
        .Select(item => new App()
        {
          Id = item.Id,
          Name = item.Name,
          IconBytes = item.Icon
        }).ToList();
      return apps;
    }

    private static async Task SaveApps(IEnumerable<App> apps)
    {
      if (!File.Exists(DbFile))
      {
        CreateDatabase();
      }
      
      await using var cnn = SimpleDbConnection();
        var parameters = apps.Select(u =>
        {
          var tempParams = new DynamicParameters();
          tempParams.Add("@Id", u.Id, DbType.Int32, ParameterDirection.Input);
          tempParams.Add("@Name", u.Name, DbType.String, ParameterDirection.Input);
          tempParams.Add("@AppUserModelId", u.AppUserModelId, DbType.String, ParameterDirection.Input);
          tempParams.Add("@Icon", u.IconBytes, DbType.Binary, ParameterDirection.Input);
          return tempParams;
        });
        
        await cnn.ExecuteAsync(
          $"INSERT INTO App (Name, AppUserModelId, Icon) VALUES (@Name, @AppUserModelId, @Icon)",
          parameters).ConfigureAwait(false);
    }
    
    public async Task UpdateDatabase()
    {
      if (File.Exists(DbFile))
      {
        await using var cnn = SimpleDbConnection();
        cnn.Open();
        cnn.Execute(
          $@"DROP TABLE App");
      }

      CreateDatabase();

      await SaveApps(AppHelper.ReadAllInstalledApps());
    }
    
    private static void CreateDatabase()
    {
      using var cnn = SimpleDbConnection();
      cnn.Open();
      cnn.Execute(
        $@"CREATE TABLE App
            (
              Id INTEGER primary key AUTOINCREMENT,
              Name VARCHAR(255) not null,
              AppUserModelId VARCHAR(255) not null,
              Icon BLOB
            )");
    }
  }
}