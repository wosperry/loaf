using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Loaf.Core.Encryptors
{
    public class MD5Encryptor : IEncryptor
    {
        private MD5 _md5;

        public MD5Encryptor()
        {
            _md5 = MD5.Create();
        }
        public string Encrypt(string password, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes($"{password}-{salt}");
            var hash = _md5.ComputeHash(bytes);
            // 这里认为不可能为空
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in bytes)
            {
                //{0:X2} 大写
                stringBuilder.AppendFormat("{0:X2}", b);
            }
            return stringBuilder.ToString();
        }
    }
}
