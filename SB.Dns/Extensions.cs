using System;
using System.Linq;
using System.Text;

namespace SB.Dns
{
  internal static class Extensions
  {
    internal static byte[] ToBigEndian(this ushort value)
    {
      var v = BitConverter.GetBytes(value);

      if (BitConverter.IsLittleEndian)
        Array.Reverse(v);

      return v;
    }

    internal static void PrintHexArray(this byte[] buff)
    {
      var hexStr = BitConverter.ToString(buff);
      var hexBytes = hexStr.Split('-');
      var ct = 0;

      for (int i = 0; i < bytes.Length; i++)
      {
        Console.Write(bytes[i] + " ");
        ct++;

        if (ct == 8)
        {
          Console.Write(" ");
          continue;
        }
        if (ct == 16)
        {
          Console.WriteLine();
          ct = 0;
        }
      }

      Console.WriteLine();
      Console.WriteLine(new String('=', 50));
    }

  }
}
