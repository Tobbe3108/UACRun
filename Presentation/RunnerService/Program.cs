using System.Threading.Tasks;
using Application.Interfaces;
using PipeMethodCalls;

namespace RunnerService
{
  internal static class Program
  {
    static async Task Main(string[] args)
    {
      while (true)
      {
        await RunServerAsync();
      }
    }

    private static async Task RunServerAsync()
    {
      var pipeServer = new PipeServer<IMessenger>("messengerPipe", () => new Messenger());

      try
      {
        await pipeServer.WaitForConnectionAsync().ConfigureAwait(false);
        await Task.Delay(500);
        pipeServer.Dispose();
      }
      catch
      {
        // ignored
      }
    }
  }
}