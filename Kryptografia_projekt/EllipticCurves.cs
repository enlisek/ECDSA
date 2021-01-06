using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kryptografia_projekt
{
    static class EllipticCurves
    {
        public static long p= 751;
        static long a = 751-1;
        static long b = 188;
        public static long[] G = new long[] { 0, 376 };
        public static long q = 727;
        public static long d = 85;
        public static long[] pk = Multiplication(G,d);

        public static long mod(long a, long m)
        {
            while(a >= m)
            {
                a -= m;
            }
            while(a < 0)
            {
                a += m;
            }
            return a;
        }

        public static long modInverse(long a, long m)
        {
            long m0 = m;
            long y = 0, x = 1;

            if (m == 1)
                return 0;

            while (a > 1)
            {
                long q = a / m;
                long t = m;

                m = a % m;
                a = t;
                t = y;

                y = x - q * y;
                x = t;
            }

            if (x < 0)
                x += m0;

            return x;
        }

        public static long[] Addition(long[] number1, long[] number2)
        {
            long s;
            long x;
            long y;

            if (number1[0] == number2[0] && number1[1] == number2[1])
            {
                s = mod((3 * number1[0] * number1[0] + a) * modInverse(2 * number1[1], p),p);
            }
            else if(number1[0]==0 && number1[1]==0)
            {
                return number2;
            }
            else if(number2[0] == 0 && number2[1] == 0)
            {
                return number2;
            }
            else if(number1[0] == number2[0] && mod(p-number1[1],p) == number2[1])
            {
                return new long[] { 0, 0 };
            }
            else
            {
                s = mod((number1[1] - number2[1]) * modInverse(mod(number1[0] - number2[0],p), p),p);
            }

            x = mod(s * s - number1[0] - number2[0],p);
            y = mod(s * (number1[0] - x) - number1[1],p);
            return new long[] { x, y };
        }

        public static long[] Multiplication(long[] number1, long k)
        {
            long[] result = new long[] { };
            long[] temp = number1;
            for (int i = 0; i < k - 1; i++)
            {
                result = Addition(number1, temp);
                temp = result; 
            }
            return result;
        }
    }
}
