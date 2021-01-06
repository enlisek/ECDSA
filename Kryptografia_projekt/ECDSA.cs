using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kryptografia_projekt
{
    static class ECDSA
    {
        static Random rnd = new Random();
        public static long[] Signature(long h)
        {
            int ek = rnd.Next((int) EllipticCurves.q);
            long[] R = EllipticCurves.Multiplication(EllipticCurves.G, ek);
            long r = R[0];
            long s = EllipticCurves.mod((h + EllipticCurves.d * r) * EllipticCurves.modInverse(ek, EllipticCurves.q), EllipticCurves.q);
            return new long[] { r, s };
        }

        public static bool SignatureVerification(long h, long[] signature)
        {
            long r = signature[0];
            long s = signature[1];
            long w = EllipticCurves.modInverse(s, EllipticCurves.q);
            long u1 = EllipticCurves.mod(w * h, EllipticCurves.q);
            long u2 = EllipticCurves.mod(w * r, EllipticCurves.q);
            long[] P = EllipticCurves.Addition(EllipticCurves.Multiplication(EllipticCurves.G, u1), EllipticCurves.Multiplication(EllipticCurves.pk, u2));
            return EllipticCurves.mod(P[0], EllipticCurves.q) == EllipticCurves.mod(r, EllipticCurves.q);

        }
    }
}
