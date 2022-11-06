namespace Loaf.Core.Encryptors
{
    public interface IEncryptor
    {
        string Encrypt(string password, string salt);
    } 
}
