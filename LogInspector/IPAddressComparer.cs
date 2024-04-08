using System.Net;

namespace LogInspector
{
    public class IPAddressComparer : IComparer<IPAddress>
    {
        public int Compare(IPAddress x, IPAddress y)
        {
            byte[] bytesX = x.GetAddressBytes();
            byte[] bytesY = y.GetAddressBytes();

            for (int i = 0; i < bytesX.Length; i++)
            {
                int compareResult = bytesX[i].CompareTo(bytesY[i]);
                if (compareResult != 0)
                {
                    return compareResult;
                }
            }

            return 0; // If all bytes are equal, the IP addresses are equal
        }
    }
}
