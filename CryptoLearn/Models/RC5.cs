using System;

namespace CryptoLearn.Models
{
    public class Rc5
    {
        const double GoldenRatio = 1.61803398874989484820458683436;
        public const int W = 64;
        public int R { get; set; }
        public ulong Pw => RoundToOdd((Math.E - 1) * Math.Pow(2, W));
        public ulong Qw => RoundToOdd((GoldenRatio - 2) * Math.Pow(2, W));

        private ulong[] S;

        private int t;
        private int b;
        private int u;
        private int c;

        public Rc5(byte[] key)
        {
            Prepare(key);
        }

        private void Prepare(byte[] key)
        {
            ulong x, y;
            int i, j, n;
            
            u = W >> 3;
            b = key.Length;
            c = b % u > 0 ? b / u + 1 : b / u;
            var l = new ulong[c];

            for (i = b - 1; i >= 0; i--)
            {
                l[i / u] = CycleLeft(l[i / u], 8) + key[i];
            }
            
            t = 2 * (R + 1);
            S = new ulong[t];
            S[0] = Pw;
            for (i = 1; i < t; i++)
            {
                S[i] = S[i - 1] + Qw;
            } 
            x = y = 0;
            i = j = 0;
            n = 3 * Math.Max(t, c);

            for (int k = 0; k < n; k++)
            {
                x = S[i] = CycleLeft((S[i] + x + y), 3);
                y = l[j] = CycleLeft((l[j] + x + y), (int)(x + y));
                i = (i + 1) % t;
                j = (j + 1) % c;
            }
        }
        public void Encrypt(byte[] inBuf, byte[] outBuf)
        {
            ulong a = BytesToUlong(inBuf, 0);
            ulong b = BytesToUlong(inBuf, 8);

            a += S[0];
            b += S[1];

            for (int i = 1; i <= R; i++)
            {
                a = CycleLeft((a ^ b), (int)b) + S[2 * i];
                b = CycleLeft((b ^ a), (int)a) + S[2 * i + 1];
            }

            UlongToBytes(a, outBuf, 0);
            UlongToBytes(b, outBuf, 8);
        }
        public void Decrypt(byte[] inBuf, byte[] outBuf)
        {
            ulong a = BytesToUlong(inBuf, 0);
            ulong b = BytesToUlong(inBuf, 8);

            for (int i = R; i > 0; i--)
            {
                b = CycleRight(b - S[2 * i + 1], (int)a) ^ a;
                a = CycleRight(a - S[2 * i], (int)b) ^ b;
            }

            b -= S[1];
            a -= S[0];

            UlongToBytes(a, outBuf, 0);
            UlongToBytes(b, outBuf, 8);
        }
        private static ulong BytesToUlong(byte[] b, int p)
        {
            ulong res = 0;
            for (int i = p + 7; i > p; i--)
            {
                res |= b[i];
                res <<= 8;
            }
            res |= b[p];
            return res;
        }
        private static void UlongToBytes(ulong a, byte[] b, int p)
        {
            for (int i = 0; i < 7; i++)
            {
                b[p + i] = (byte)(a & 0xFF);
                a >>= 8;
            }
            b[p + 7] = (byte)(a & 0xFF);
        }
        private ulong CycleLeft(ulong n, int k)
        {
            var x = n << k;
            var y = n >> (W - k);
            return (x | y);
        }
        private ulong CycleRight(ulong n, int k)
        {
            var x = n >> k;
            var y = n << (W - k);
            return (x | y);
        }
        private ulong RoundToOdd(double x)
        {
            return (ulong) (2 * (int)(x / 2) + 1);
        }
        
    }
}