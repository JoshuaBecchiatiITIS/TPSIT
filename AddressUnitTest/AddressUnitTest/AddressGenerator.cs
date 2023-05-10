using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressUnitTest
{
    internal class AddressGenerator : IAddress
    {
        private readonly byte[] _ipv4Bytes;
        private readonly int _bitNumber;

        public AddressGenerator(int bitNumber, byte[] ipv4Bytes)
        {
            if (bitNumber < 0 || bitNumber > 32)
                throw new ArgumentOutOfRangeException(nameof(bitNumber), "I bit devono essere tra 0 e 32.");

            if (ipv4Bytes == null || ipv4Bytes.Length != 4)
                throw new ArgumentException("I byte dell'indirizzo IPv4 devono essere un array di byte di lunghezza 4.", nameof(ipv4Bytes));

            _bitNumber = bitNumber;
            _ipv4Bytes = ipv4Bytes;
        }

        public string generateIPv4()
        {
            // Convert the 32-bit IPv4 address to a byte array
            byte[] fullAddressBytes = BitConverter.GetBytes(_ipv4Bytes[0] << 24 | _ipv4Bytes[1] << 16 | _ipv4Bytes[2] << 8 | _ipv4Bytes[3]);

            // Apply the subnet mask to the IPv4 address
            int numBytes = _bitNumber / 8;
            int remainingBits = _bitNumber % 8;
            byte[] subnetMaskBytes = new byte[4];
            for (int i = 0; i < numBytes; i++)
            {
                subnetMaskBytes[i] = 255;
            }
            if (remainingBits > 0)
            {
                subnetMaskBytes[numBytes] = (byte)(255 << (8 - remainingBits));
            }

            byte[] subnettedAddressBytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                subnettedAddressBytes[i] = (byte)(fullAddressBytes[i] & subnetMaskBytes[i]);
            }

            // Convert the subnetted address bytes to a string representation
            string[] addressStrings = new string[4];
            for (int i = 0; i < 4; i++)
            {
                addressStrings[i] = subnettedAddressBytes[i].ToString();
            }
            return string.Join(".", addressStrings);
        }

        public string generateSubnet()
        {
            // Generate a random subnet mask using the specified number of bits
            var subnetBytes = new byte[4];
            var numBytes = _bitNumber / 8;
            var remainingBits = _bitNumber % 8;

            for (int i = 0; i < numBytes; i++)
            {
                subnetBytes[i] = 255;
            }

            if (remainingBits > 0)
            {
                subnetBytes[numBytes] = (byte)(255 << (8 - remainingBits));
            }

            // Convert the subnet bytes to a string representation
            var subnetString = string.Join(".", subnetBytes);
            var subnetMask = new System.Net.IPAddress(subnetBytes).GetAddressBytes().Length * 8 - remainingBits;
            return $"{subnetString}/{subnetMask}";
        }
    }
}
