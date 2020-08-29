using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces
{
  public interface IMessenger
  {
    void Message(string message);
  }
}