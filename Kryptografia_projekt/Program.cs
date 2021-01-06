using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Remoting.Services;

namespace Kryptografia_projekt
{
    class Program
    {
        public static void Start()
        {
            Console.Clear();
            Console.WriteLine("What do you want to do? Enter 1 or 2.");
            Console.WriteLine("1. to generate a signature for a file");
            Console.WriteLine("2. to verify a signature for a file");
            string selectedOption = Console.ReadLine();
            if(selectedOption=="1")
            {
                GenerateSignature();
            }
            else if(selectedOption=="2")
            {
                VerifySignature();
            }
            else
            {
                Console.WriteLine("Error. Press any key to try again. ");
                Console.ReadKey();
                Start();
            }
        }

        static void GenerateSignature()
        {
            Console.Clear();
            Console.WriteLine("Enter the file path: ");
            string path = Console.ReadLine();
            long[] signature = ECDSA.Signature(Hash.GetHash(path));
            Console.WriteLine($"The signature: ({signature[0]},{signature[1]})");
            Console.WriteLine("Press any key to go back to start.");
            Console.ReadKey();
            Start();
        }

        static void VerifySignature()
        {
            long r = 0;
            long s = 0;
            Console.Clear();
            Console.WriteLine("Enter the file path: ");
            string path = Console.ReadLine();
            Console.WriteLine("Enter the r value:");
            try
            {
                r = Convert.ToInt64(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Error: {e.Message}.");
                Console.ReadKey();
                VerifySignature();
            }
            Console.WriteLine("Enter the s value:");
            try
            {
                s = Convert.ToInt64(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Error: {e.Message}.");
                Console.ReadKey();
                VerifySignature();
            }

            if(ECDSA.SignatureVerification(Hash.GetHash(path), new long[] { r, s }))
            {
                Console.WriteLine("The signature is valid.");
                Console.ReadKey();
                Start();
            }
            else
            {
                Console.WriteLine("The signature is not valid.");
                Console.ReadKey();
                Start();
            }
        }

        static void Main(string[] args)
        {
            Start();
        }
    }
}
