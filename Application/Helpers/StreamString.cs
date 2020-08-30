using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
  public class StreamString
  {
    private readonly Stream _ioStream;
    private readonly UnicodeEncoding _streamEncoding;

    public StreamString(Stream ioStream)
    {
      this._ioStream = ioStream;
      _streamEncoding = new UnicodeEncoding();
    }

    public async Task<string> ReadStringAsync()
    {
      var len = 0;
      len = _ioStream.ReadByte() * 256;
      len += _ioStream.ReadByte();
      var inBuffer = new byte[len];
      await _ioStream.ReadAsync(inBuffer, 0, len);

      return _streamEncoding.GetString(inBuffer);
    }

    public async Task<int> WriteStringAsync(string outString)
    {
      var outBuffer = _streamEncoding.GetBytes(outString);
      var len = outBuffer.Length;
      if (len > ushort.MaxValue)
      {
        len = (int)ushort.MaxValue;
      }
      _ioStream.WriteByte((byte)(len / 256));
      _ioStream.WriteByte((byte)(len & 255));
      await _ioStream.WriteAsync(outBuffer, 0, len);
      await _ioStream.FlushAsync();

      return outBuffer.Length + 2;
    }
  }
}