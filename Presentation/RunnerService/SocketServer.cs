using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RunnerService
{
  public class SocketServer
  {
    private readonly UdpClient _server = new UdpClient(9000);
 
    public void Dispose()
    {
      Stop();
 
      (_server as IDisposable).Dispose();
    }
 
    public void Start()
    {

    }
    
    private void Stop()
    {
      _server.Close();
    }
  }
}