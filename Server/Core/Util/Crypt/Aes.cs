using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Core.Util.Crypt;

public class AesEncryptor
{
    private readonly byte[] _key;
    private readonly byte[] _iv;

    public AesEncryptor(string key, string iv)
    {
        _key = Encoding.UTF8.GetBytes(key);
        _iv = Encoding.UTF8.GetBytes(iv);
    }

    public string Encrypt(string plaintext)
    {
        byte[] plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

        using Aes aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        using MemoryStream memoryStream = new MemoryStream();
        using CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        cryptoStream.Write(plaintextBytes, 0, plaintextBytes.Length);
        cryptoStream.FlushFinalBlock();

        byte[] ciphertextBytes = memoryStream.ToArray();
        return Convert.ToBase64String(ciphertextBytes);
    }

    public string Decrypt(string ciphertext)
    {
        byte[] ciphertextBytes = Convert.FromBase64String(ciphertext);

        using Aes aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;

        using MemoryStream memoryStream = new MemoryStream(ciphertextBytes);
        using CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);

        byte[] plaintextBytes = new byte[ciphertextBytes.Length];
        int decryptedByteCount = cryptoStream.Read(plaintextBytes, 0, plaintextBytes.Length);

        return Encoding.UTF8.GetString(plaintextBytes, 0, decryptedByteCount);
    }
}