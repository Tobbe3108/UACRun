using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TerminalLauncher
{
  internal static class Program
  {
    private static void Main(string[] args)
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
      
      var process = Process.GetProcessesByName("RunnerService");
      var path = @$"{Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)}\RunnerService.exe";
      if (process.Length == 0)
      {
        var info = new ProcessStartInfo(path) {Verb = "runas", UseShellExecute = true};
        var runnerService = Process.Start(info);
        while (true)
        {
          try
          {
            var time = runnerService.StartTime;
            Thread.Sleep(100);
            break;
          }
          catch
          {
            // ignored
          }
        }
      }
      
      Thread.Sleep(100);
      using var client = new UdpClient();
      client.Connect(string.Empty, 9000);
      var bytes = Encoding.Default.GetBytes(args.First());
      client.Send(bytes, bytes.Length);
    }
  }
}