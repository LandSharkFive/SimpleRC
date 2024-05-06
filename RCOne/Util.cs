using System.Text;

namespace RCOne
{
    internal class Util
    {
        private byte[] KeySeven = new byte[] { 60, 116, 204, 106, 21, 18, 128, 37, 5, 208, 166, 120, 107, 38, 116, 118 };

        private string QuoteOne = "Our revels now are ended. These our actors, as I foretold you, were all spirits and are melted into air, into thin air. "
           + "And like the baseless fabric of this vision, The cloud-capped towers, the gorgeous palaces, The solemn temples, the great globe itself, "
           + "Yea, all which it inherit, shall dissolve and, like this insubstantial pageant faded, Leave not a rack behind. We are such stuff "
           + "as dreams are made on, and our little life is rounded with a sleep.";

        public void TestOne()
        {
            // plain text
            byte[] pt = new byte[100];
            GetSequence(pt);

            // encrypt
            byte[] s1 = GetState(KeySeven);
            byte[] out1 = EncryptSeven(pt, s1);

            // decrypt
            byte[] out2 = EncryptSeven(out1, s1);
            Console.WriteLine(pt.SequenceEqual(out2));
            Console.WriteLine();
        }

        public void TestTwo()
        {
            // encrypt
            byte[] s1 = GetState(KeySeven);
            byte[] pt = Encoding.ASCII.GetBytes(QuoteOne);
            byte[] ct = EncryptSeven(pt, s1);

            // decrypt
            byte[] temp1 = EncryptSeven(ct, s1);
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

        private byte[] GetState(byte[] key)
        {
            int j = 0;
            int t = 0;
            byte[] s = new byte[256];
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = (byte)i;
            }
            for (int i = 0; i < s.Length; i++)
            {
                j = (j + s[i] + key[i % key.Length]) % 256;
                t = s[i];
                s[i] = s[j];
                s[j] = (byte)t;
            }
            return s;
        }

        private byte[] EncryptSeven(byte[] a, byte[] s)
        {
            int i = 0;
            int j = 0;
            int k = 0;
            byte[] b = new byte[a.Length];
            for (int m = 0; m < a.Length; m++)
            {
                i = (i + 1) % s.Length;
                j = (j + s[i]) % s.Length;
                k = (s[i] + s[j]) % s.Length;
                b[m] = Convert.ToByte((a[m] ^ s[k]) % 256);
            }
            return b;
        }


        public void Print(byte[] a)
        {
            for (int i = 0;i < a.Length;i++) 
            {
                Console.WriteLine(a[i]);
            }
        }

    }
}
