﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dolos_Payload_Execution
{
    class EncryptionKey
    {
        public static String EncryptionKeyDirty = "V8A4AXJYCZCZCX9C7WNIJKDC4EB7NKJFRP05KAVG83INLSOE0CJO5UXL58A5WLL1Q6HX7D17AGKQC01L07GKB5K71PAMAZUXRQ6OL0GXLMNYKHJEHZFZEA0VWK5SCP6LWX8V3MAA0T9ZRWWUG6FLU5SR0L2A4YU5PCBXJXK5ZE9VABD9XLMFKV01EQ5RT9GC8DJPU8LSVKWP66QZI2MSLOV3LKS222LZ86X61XOYA3TA9K5I9GJ0WX0TSQDKQSZ386R9IT7UOXSR5V3APK2WTUDLGEOLTEIRD0BD359PIYM8EIY6WD5FZDJO3F2EFWBJPANEU0HTKNN4HEG7QRYQJC2AQWKXC4KWXDTE4TM55L4MZOGY95YX7UAMINX7QTQQC1HEVCD6F2U7HPKB";
        public static String EncryptedKey = Regex.Replace(EncryptionKeyDirty, @"\s+", "");
    }
}
