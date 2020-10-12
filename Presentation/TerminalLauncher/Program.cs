using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
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

      if (args.Length > 1)
      {
        for (var index = 1; index < args.Length; index++)
        {
          args[0] = args[0] += " " + args[index];
        }
      }
      
      var uncPath= LocalToUnc(args.First());
      if (!string.IsNullOrEmpty(uncPath))  args[0] = uncPath;
      var bytes = Encoding.Default.GetBytes(string.Join("|;|", args));
      client.Send(bytes, bytes.Length);
    }
    
    [DllImport("mpr.dll")]
    static extern int WNetGetUniversalNameA(
      string lpLocalPath, int dwInfoLevel, IntPtr lpBuffer, ref int lpBufferSize
    );

    // I think max length for UNC is actually 32,767
    private static string LocalToUnc(string localPath, int maxLen = 2000)
    {
      IntPtr lpBuff;

      // Allocate the memory
      try
      {
        lpBuff = Marshal.AllocHGlobal(maxLen); 
      }
      catch (OutOfMemoryException)
      {
        return null;
      }

      try
      {
        var res = WNetGetUniversalNameA(localPath, 1, lpBuff, ref maxLen);

        return res != 0 ? null : Marshal.PtrToStringAnsi(Marshal.ReadIntPtr(lpBuff));

        // lpbuff is a structure, whose first element is a pointer to the UNC name (just going to be lpBuff + sizeof(int))
      }
      catch (Exception)
      {
        return null;
      }
      finally
      {
        Marshal.FreeHGlobal(lpBuff);
      }
    }
  }
}