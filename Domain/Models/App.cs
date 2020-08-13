using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace Domain.Models
{
  public class App
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public string AppUserModelId { get; set; }

    public Icon Icon
    {
      get => BytesToIcon(IconBytes);
      set => IconBytes = IconToBytes(value);
    }
    
    public byte[] IconBytes { get; set; }
    
    private static byte[] IconToBytes(Icon icon)
    {
      using var ms = new MemoryStream();
      icon.Save(ms);
      return ms.ToArray();
    }
    private static Icon BytesToIcon(byte[] bytes)
    {
      using var ms = new MemoryStream(bytes);
      return new Icon(ms);
    }

  }
}