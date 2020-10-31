using System;
using System.Text;

namespace TestObfuscator.Helpers
{
    public class DecryptionHelper
    {
        public static string Decrypt_Base64(string dataEnc)
        {
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(dataEnc));
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
}