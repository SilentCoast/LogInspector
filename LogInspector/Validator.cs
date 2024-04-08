using System.Net;

namespace LogInspector
{
    //TODO: SOLID(S) violation! split methods into different classes
    public static class Validator
    {
        public static bool IsValidPath(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                string p = Path.GetFullPath(path);
                string? directoryPath = Path.GetDirectoryName(p);

                if (Directory.Exists(directoryPath))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsIPAddressInRange(IPAddress ipAddress, IPAddress startAddress, IPAddress endAddress)
        {
            byte[] ipBytes = ipAddress.GetAddressBytes();
            byte[] startBytes = startAddress.GetAddressBytes();
            byte[] endBytes = endAddress.GetAddressBytes();

            for (int i = 0; i < ipBytes.Length; i++)
            {
                if (ipBytes[i] < startBytes[i] || ipBytes[i] > endBytes[i])
                    return false;
            }

            return true;
        }
        public static bool IsIPAddressInRange(IPAddress ipAddress, IPAddress startAddress)
        {
            byte[] ipBytes = ipAddress.GetAddressBytes();
            byte[] startBytes = startAddress.GetAddressBytes();

            for (int i = 0; i < ipBytes.Length; i++)
            {
                if (ipBytes[i] < startBytes[i])
                    return false;
            }

            return true;
        }
    }
}
