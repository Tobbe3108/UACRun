using PipeMethodCalls;
using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;

namespace TerminalLauncher
{
  internal static class Program
  {
    static async Task Main(string[] args)
    {
      if (args.Length == 0)
      {
        Console.WriteLine("You must specify a path!");
        return;
      }
      
      if (args.Length > 1)
      {
        Console.WriteLine("Only specify one path!");
        return;
      }

      using var pipeClient = new PipeClient<IMessenger>("messengerPipe");
      try
      {
        await pipeClient.ConnectAsync().ConfigureAwait(false);
        await pipeClient.InvokeAsync(adder => adder.Message(args.First()));
        pipeClient.Dispose();
      }
      catch
      {
        // ignored
      }
    }
  }
}