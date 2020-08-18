using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TerminalLauncher
{
  internal static class Program
  {
    private static void Main(string[] args)
    {
      if (args.Length <= 1) Console.WriteLine("Only specify one path!");
      var arg = args.First();
    }
  }
}