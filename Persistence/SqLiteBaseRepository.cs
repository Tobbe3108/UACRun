using System;
using System.Data.SQLite;

namespace Persistence
{
  public class SqLiteBaseRepository
  {
    protected static string DbFile => Environment.CurrentDirectory + "\\SimpleDb.sqlite";

    protected static SQLiteConnection SimpleDbConnection()
    {
      return new SQLiteConnection("Data Source=" + DbFile);
    }
  }
}