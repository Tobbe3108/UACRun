using System.Diagnostics;
using Application.Interfaces;

namespace RunnerService
{
  public class Messenger : IMessenger
  {
    public void Message(string message)
    {
      try
      {
        Process.Start(message);
      }
      catch
      {
        // ignored
      }
    }
  }
}