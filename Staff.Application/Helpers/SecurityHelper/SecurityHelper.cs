using System.Security.Cryptography;
using System.Text;

namespace Staff.Application.Helpers.SecurityHelper;

public class SecurityHelper : ISecurityHelper
{
    private static readonly byte[]
        Key = Encoding.UTF8.GetBytes("0123456789ABCDEF0123456789ABCDEF"); // 32 bytes key for AES-256

    private static readonly byte[] Iv = Encoding.UTF8.GetBytes("ABCDEF0123456789"); // 16 bytes IV for AES

    public string GenerateApiToken(string username)
    {
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[32];
        rng.GetBytes(bytes);
        return EncryptValue(Convert.ToBase64String(bytes));
    }

    public string EncryptValue(string value)
    {
        using var aes = Aes.Create();
        aes.Key = Key;
        aes.IV = Iv;
        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        {
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(value);
            }
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    public string DecryptValue(string value)
    {
        using var aes = Aes.Create();
        aes.Key = Key;
        aes.IV = Iv;
        ICryptoTransform decrypted = aes.CreateDecryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream(Convert.FromBase64String(value));
        using var cs = new CryptoStream(ms, decrypted, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }
}