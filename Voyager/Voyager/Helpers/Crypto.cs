using com.pakhee.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voyager
{
    class Crypto
    {
        public static String iv = "3LkWwZFUdky782Qw";
        public static string key = CryptLib.getHashSha256(Voyager.Program.encryptionkey, 32);
        public static string Encrypt(string text)
        {
            CryptLib _crypt = new CryptLib();
            String cypherText = _crypt.encrypt(text, key, iv);
            return cypherText;
        }
        public static string Decrypt(string text)
        {
            CryptLib _crypt = new CryptLib();
            return _crypt.decrypt(text, key, iv);
        }
    }
}
