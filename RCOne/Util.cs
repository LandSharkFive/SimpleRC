using System.Security.Cryptography;
using System.Text;

namespace RCOne
{
    internal class Util
    {
        private byte[] KeySeven = new byte[] { 60, 116, 204, 106, 21, 18, 12, 123 };

        private string QuoteOne = "Our revels now are ended. These our actors, as I foretold you, were all spirits and are melted into air, into thin air. "
           + "And like the baseless fabric of this vision, The cloud-capped towers, the gorgeous palaces, The solemn temples, the great globe itself, "
           + "Yea, all which it inherit, shall dissolve and, like this insubstantial pageant faded, Leave not a rack behind. We are such stuff "
           + "as dreams are made on, and our little life is rounded with a sleep.";

        public void TestOne()
        {
            // setup
            byte[] pt = new byte[100];
            GetSequence(pt);

            // encrypt
            int a = HashOne(KeySeven);
            int b = HashTwo(KeySeven);
            byte[] s1 = GetState(GetHash(KeySeven), a);
            byte[] out1 = EncryptSeven(pt, s1, b);

            // decrypt
            byte[] out2 = EncryptSeven(out1, s1, b);
            Console.WriteLine(pt.SequenceEqual(out2));
            Print(out2);
        }

        public void TestTwo()
        {
            // encrypt
            int a = HashOne(KeySeven);
            int b = HashTwo(KeySeven);
            byte[] s1 = GetState(GetHash(KeySeven), a);
            byte[] pt = Encoding.ASCII.GetBytes(QuoteOne);
            byte[] ct = EncryptSeven(pt, s1, b);

            // decrypt
            byte[] temp1 = EncryptSeven(ct, s1, b);
            string output = Encoding.ASCII.GetString(temp1);
            Console.WriteLine(QuoteOne.Equals(output));
            Console.WriteLine(output);
        }

        private void GetSequence(byte[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = Convert.ToByte(i % 256);
            }
        }

        private void Shuffle(byte[] a, int start)
        {
            uint seed = (uint)start;
            for (int i = 0; i < a.Length; i++)
            {
                if ((seed & 1) == 0)
                {
                    seed ^= seed >> 2;
                }
                else
                {
                    seed ^= seed >> 3;
                }
                if ((seed & 1) == 0)
                {
                    seed ^= seed << 4;
                }
                else
                {
                    seed ^= seed << 5;  
                }
                if ((seed & 1) == 0)
                {
                    seed ^= seed >> 6;
                }
                else
                {
                    seed ^= seed >> 7;
                }
                int k = Math.Abs((int)seed % 256);
                byte temp = a[i];
                a[i] = a[k];
                a[k] = temp;
            }
        }

        private byte[] GetState(byte[] hash, int start)
        {
            int j = 0;
            byte[] s = new byte[256];
            GetSequence(s);
            Shuffle(s, start);
            for (int i = 0; i < s.Length; i++)
            {
                j = (j + s[i] + hash[i % hash.Length]) % 256;
                byte temp = s[i];
                s[i] = s[j];
                s[j] = temp;
            }
            return s;
        }

        private byte[] EncryptSeven(byte[] a, byte[] s, int start)
        {
            int k = 0;
            uint seed = (uint)start;
            byte[] b = new byte[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                if ((seed & 1) == 0)
                {
                    seed ^= seed >> 6;
                }
                else
                {
                    seed ^= seed >> 7;
                }
                if ((seed & 1) == 0)
                {
                    seed ^= seed << 8;
                }
                else
                {
                    seed ^= seed << 9;
                }
                if ((seed & 1) == 0)
                {
                    seed ^= seed >> 12;
                }
                else
                {
                    seed ^= seed >> 13;
                }
                k = Math.Abs((int)seed % 256);
                b[i] = Convert.ToByte((a[i] ^ s[k]) % 256);
            }
            return b;
        }


        private byte[] GetHash(byte[] input)
        {
            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(input);
            }
        }

        int HashOne(byte[] a)
        {
            uint hash = 0;
            for (int i = 0; i < a.Length; i++)
            {
                hash = (hash << 1) + (uint)a[i] * 13;
            }
            return Convert.ToInt32(hash % int.MaxValue);
        }

        int HashTwo(byte[] a)
        {
            uint hash = 0;
            for (int i = 0; i < a.Length; i++)
            {
                hash = (hash << 1) + (uint)a[i] * 17;
            }
            return Convert.ToInt32(hash % int.MaxValue);
        }

        public void Print(byte[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine(a[i]);
            }
        }


    }
}
