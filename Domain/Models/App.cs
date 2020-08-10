using System;
using System.Drawing;

namespace Domain.Models
{
  public class App
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public string AppUserModelId { get; set; }
    public byte[] Icon { get; set; }
  }
}