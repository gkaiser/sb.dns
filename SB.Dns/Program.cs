using System.Net;
using System.Net.Sockets;

namespace SB.Dns
{
  public static class Program
  {
    internal static Random Random = new Random();

    public static void Main(string[] args)
    {
      // get dns servers (resolv.conf?)

      // promt for hostname

      // get host by name
      Program.GetHostByName("www.google.com", 256);

      Console.WriteLine();
      Console.Write("Press any key to quit... ");
      Console.ReadKey();
    }

    private static void GetHostByName(string host, int queryType)
    {
      if (System.Diagnostics.Debugger.IsAttached)
      {
        Console.WriteLine("Overriding host to \"www.google.com\"...");
        host = "www.google.com";
      }

      var dnsIp = "8.8.8.8";
      var buff = new byte[0];

      using (var ms = new MemoryStream())
      {
        using (var bw = new BinaryWriter(ms))
        {
          buff = new byte[2];
          Program.Random.NextBytes(buff);
          Array.Reverse(buff);
          bw.Write(buff);

          bw.Write(((ushort)0x0100).ToBigEndian());
          bw.Write(((ushort)1).ToBigEndian());
          bw.Write(((ushort)0).ToBigEndian());
          bw.Write(((ushort)0).ToBigEndian());
          bw.Write(((ushort)0).ToBigEndian());

          foreach (var t in host.Split('.'))
          {
            var cb = System.Text.Encoding.UTF8.GetBytes(t);
            var bl = cb.Length;

            bw.Write((byte)bl);
            bw.Write(cb);
          }

          bw.Write((byte)0);
          bw.Write(((ushort)2).ToBigEndian());
          bw.Write(((ushort)1).ToBigEndian());
          bw.Close();
        }

        buff = ms.ToArray();

        if (System.Diagnostics.Debugger.IsAttached)
          buff.PrintHexArray();

        ms.Close();
      }

      var uc = new UdpClient(dnsIp, 53);
      uc.Send(buff);

      var ie = new IPEndPoint(IPAddress.Any, 0);

      var respBuff = uc.Receive(ref ie);
      respBuff.PrintHexArray();

      return;
    }


  }
} 