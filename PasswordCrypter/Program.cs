using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PasswordCrypter
{
    class Program
    {
        static byte[] bytes;

        static void Main(string[] args)
        {
            Console.WriteLine("Original String: ");
            bytes = Encoding.ASCII.GetBytes(File.ReadAllText("key.txt"));
            string originalString = Console.ReadLine();
            string cryptedString = Encrypt(originalString);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nEncrypt Result: {0}", cryptedString);
        }
        public static string Encrypt(string originalString)
        {
            if (String.IsNullOrEmpty(originalString))
            {
                throw new ArgumentNullException
                       ("The string which needs to be encrypted can not be null.");
            }
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }
    }
}
