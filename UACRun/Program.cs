using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Helpers;
using Domain.Models;
using Persistence;

namespace UACRun
{
  internal static class Program
  {

    private static readonly SqlLiteCustomerRepository SqlLiteCustomerRepository = new SqlLiteCustomerRepository();
    private static readonly List<App> Apps = SqlLiteCustomerRepository.GetAllApps();
    private static List<App> _searchApps;
    
    private static void Main()
    {
      var toShow = true;
      while (toShow)
      {
        if (_searchApps != null)
        {
          foreach (var app in _searchApps)
          {
            Console.Write($"{app.Id}".PadRight(5));
            Console.Write($"{app.Name}\n");
          }
        }
        else
        {
          foreach (var app in Apps)
          {
            Console.Write($"{app.Id}".PadRight(5));
            Console.Write($"{app.Name}\n");
          }
        }
        
        var input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input))
        {
          var isLong = long.TryParse(input, out var longInput);
          var result = isLong ? (List<App>) Apps.Where(a => a.Id == longInput) : (List<App>) Apps.Where(a => a.Name.Contains(input));

          if (result.Count == 1)
          {
            var first = result.FirstOrDefault();
            if (first != null) SqlLiteCustomerRepository.GetApp(first.Id);
            toShow = false;
          }
          else
          {
            _searchApps = result;
          }
        }
      }
    }
    
    private static async void ResetAppDatabase()
    {
      Console.WriteLine("Resetting app database...");
      //_sqlLiteCustomerRepository.UpdateDatabase();
      await Task.Delay(1 * 1000);
      Console.Clear();
      Main();
    }
  }
}
