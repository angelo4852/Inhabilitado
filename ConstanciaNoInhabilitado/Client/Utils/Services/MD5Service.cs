using System.Text;

namespace ConstanciaNoInhabilitado.Client.Utils.Services
{
    public class MD5Service
    {
        public string ComputeHash(string input)
        {
            // Initialize variables
            uint[] T = new uint[64];
            for (int i = 0; i < 64; i++)
            {
                T[i] = (uint)(Math.Floor(Math.Abs(Math.Sin(i + 1)) * Math.Pow(2, 32)));
            }

            uint[] X = new uint[16];
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            int originalLength = inputBytes.Length;
            int newLength = originalLength + 1;

            // Pad with zeros
            while ((newLength % 64) != 56)
            {
                newLength++;
            }

            byte[] paddedInput = new byte[newLength + 8];
            Array.Copy(inputBytes, paddedInput, originalLength);

            paddedInput[originalLength] = 0x80;

            long messageLengthBits = (long)originalLength * 8;
            byte[] lengthBytes = BitConverter.GetBytes(messageLengthBits);
            Array.Copy(lengthBytes, 0, paddedInput, newLength, 8);

            uint A = 0x67452301;
            uint B = 0xEFCDAB89;
            uint C = 0x98BADCFE;
            uint D = 0x10325476;

            for (int i = 0; i < paddedInput.Length; i += 64)
            {
                for (int j = 0; j < 16; j++)
                {
                    X[j] = BitConverter.ToUInt32(paddedInput, i + j * 4);
                }

                uint AA = A, BB = B, CC = C, DD = D;

                for (int j = 0; j < 64; j++)
                {
                    uint F, g;

                    if (j < 16)
                    {
                        F = (B & C) | (~B & D);
                        g = (uint)j;
                    }
                    else if (j < 32)
                    {
                        F = (D & B) | (~D & C);
                        g = (uint)(5 * j + 1) % 16;
                    }
                    else if (j < 48)
                    {
                        F = B ^ C ^ D;
                        g = (uint)(3 * j + 5) % 16;
                    }
                    else
                    {
                        F = C ^ (B | ~D);
                        g = (uint)(7 * j) % 16;
                    }

                    F = F + A + T[j] + X[g];
                    A = D;
                    D = C;
                    C = B;
                    B = B + LeftRotate(F, (j < 16) ? (uint)new[] { 7, 12, 17, 22 }[j % 4] :
                                                (j < 32) ? (uint)new[] { 5, 9, 14, 20 }[j % 4] :
                                                (j < 48) ? (uint)new[] { 4, 11, 16, 23 }[j % 4] :
                                                           (uint)new[] { 6, 10, 15, 21 }[j % 4]);
                }

                A += AA;
                B += BB;
                C += CC;
                D += DD;
            }

            byte[] hash = new byte[16];
            Array.Copy(BitConverter.GetBytes(A), 0, hash, 0, 4);
            Array.Copy(BitConverter.GetBytes(B), 0, hash, 4, 4);
            Array.Copy(BitConverter.GetBytes(C), 0, hash, 8, 4);
            Array.Copy(BitConverter.GetBytes(D), 0, hash, 12, 4);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        private static uint LeftRotate(uint value, uint count)
        {
            return (value << (int)count) | (value >> (32 - (int)count));
        }
    }
}
