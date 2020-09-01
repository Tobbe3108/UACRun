using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.VisualBasic;

namespace RunnerService
{
  internal static class Program
  {
    private static void Main()
    {
      using var server = new UdpClient(9000);
      var ip = new IPEndPoint(IPAddress.Any, 0);
 
      while (true)
      {
        var bytes = server.Receive(ref ip);
        var data = Encoding.Default.GetString(bytes);
        var args = data.Split("|;|");
        ProcessStartInfo info;
        if (args.First().Contains(".msi"))
        {
          //Open msi with msiexec
          info = new ProcessStartInfo
          {
            FileName = "msiexec.exe",
            Arguments = $"/i \"{data}\" ALLUSERS=1"
          };
        }
        else
        {
          //Add path to process filename
          info = new ProcessStartInfo(args.First())
          {
            UseShellExecute = true
          };
          
          //Add args if anny
          if (args.Length > 1)
          {
            info.Arguments = string.Join(" ", args.Skip(1).ToArray());
          }
        }
        try
        {
          Process.Start(info);
        }
        catch
        {
          // ignored
        }
      }
    }
  }
}