using System.IO;
using System.Linq;
using Application.Interfaces;
using Dapper;
using Domain.Models;

namespace Persistence
{
  class SqlLiteCustomerRepository : SqLiteBaseRepository, ICustomerRepository
  {
    public App GetApp(long id)
    {
      if (!File.Exists(DbFile)) return null;
      using var cnn = SimpleDbConnection();
      cnn.Open();
      var result = cnn.Query<App>(
        $@"SELECT Id, Name, AppUserModelId, Icon
            FROM App
            WHERE Id = @id", new { id }).FirstOrDefault();
      return result;
    }

    public void SaveApp(App app)
    {
      if (!File.Exists(DbFile))
      {
        CreateDatabase();
      }

      using var cnn = SimpleDbConnection();
      cnn.Open();
      app.Id = cnn.Query<long>(
        $@"INSERT INTO App
            ( Name, AppUserModelId, Icon ) VALUES
            ( @Name, @AppUserModelId, @Icon );
            select last_insert_rowid()", app).First();
    }
    
    private static void CreateDatabase()
    {
      using var cnn = SimpleDbConnection();
      cnn.Open();
      cnn.Execute(
        $@"create table App
            (
              Id INTEGER identity primary key AUTOINCREMENT,
              Name TEXT not null,
              AppUserModelId TEXT not null,
              Icon BLOB not null
            )");
    }
  }
}