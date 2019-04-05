using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dolos_Encryption_Tool
{
    public partial class Form1 : Form
    {
        public static String randomString;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.OutputLog.Text += "[i] Welcome to the Dolos Payload Encryption Tool, Please insert a payload and click convert to get your payload ready for Dolos";
        }

        public void Convert(String Payload)
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
                cleanPayload = string.Join(
                Environment.NewLine,
                Payload.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(fooline => fooline.Trim())
                );
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
                plaintext = System.Text.Encoding.UTF8.GetBytes(cleanPayload);
            }
            else
            {
                log("[i] Formatting Not Removed Successfully");
                plaintext = System.Text.Encoding.UTF8.GetBytes(Payload);
            }
            log("[i] Converting to B64");
            try
            {
                base64 = System.Convert.ToBase64String(plaintext);
            }
            catch(Exception e)
            {
                log("[!] FATAL: Failed to convert to Base64 Trace Fallows\n"+e);
                fatal = true;
            }
            log("[i] Converted to B64");
            log("[i] Encrypting");
            if (!fatal)
            {
                randomString = RandomString(400);
                log($"[!] Random String Generated");
                richTextBox1.Text = randomString;
                log($"[!] Building Encryption Key");
                log($"[!] Encrypting");
                try
                {
                    encrypted = Crypto.Encrypt(base64);
                    log("[i] Encrypted!");
                    OutputPayload.Text = encrypted;
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
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public void log(String msg)
        {
            this.OutputLog.Invoke(new Action(() => OutputLog.Text += "\n" + msg));
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Convert(InputPayload.Text);
        }
    }
    public class Crypto
    {
        private static String EncryptionKey = Form1.randomString;
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
