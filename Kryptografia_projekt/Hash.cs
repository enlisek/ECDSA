using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kryptografia_projekt
{
    static class Hash
    {
        static long Get4Bytes(byte[] array)
        {

            string result = "";
            for (int i = 0; i < array.Length; i++)
            {
                if (i < 4)
                    result += array[i].ToString();
            }

            long resultToNumber = Convert.ToInt64(result);
            return resultToNumber;
        }

        public static long GetHash(string pathToFile)
        {
            string path = pathToFile;
            long result = 0;

            if (path==null || path=="")
            {
                Console.WriteLine("Error.");
                Console.ReadKey();
                Program.Start();
                return result;
            }
            var file = new FileInfo(path);

            using (SHA256 mySHA256 = SHA256.Create())
            {
                try
                {
                    FileStream fileStream = file.Open(FileMode.Open);
                    fileStream.Position = 0;
                    byte[] hashValue = mySHA256.ComputeHash(fileStream);
                    fileStream.Close();
                    result = Get4Bytes(hashValue);
                }
                catch (IOException e)
                {
                    Console.WriteLine($"I/O Exception: {e.Message}");
                    Console.ReadKey();
                    Program.Start();
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine($"Access Exception: {e.Message}");
                    Console.ReadKey();
                    Program.Start();
                }
                return result;
            }
        }
    }
}
