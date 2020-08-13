using System.Collections.Generic;
using Domain.Models;

namespace Application.Interfaces
{
  public interface ICustomerRepository
  {
    App GetApp(long guid);
    List<App> GetAllApps();
  }
}