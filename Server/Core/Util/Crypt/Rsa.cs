using System.Security.Cryptography;

namespace Core.Util.Crypt;

public class RSA
{
    private RSACryptoServiceProvider rsaProvider;

    public RSA()
    {
        rsaProvider = new RSACryptoServiceProvider();
    }

    public RSA(int keySize)
    {
        rsaProvider = new RSACryptoServiceProvider(keySize);
    }

    public string GetPublicKey()
    {
        return rsaProvider.ToXmlString(false);
    }

    public string GetPrivateKey()
    {
        return rsaProvider.ToXmlString(true);
    }

    public byte[] Encrypt(byte[] data)
    {
        return rsaProvider.Encrypt(data, false);
    }

    public byte[] Decrypt(byte[] data)
    {
        return rsaProvider.Decrypt(data, false);
    }

    public byte[] SignData(byte[] data)
    {
        using (var sha256 = SHA256.Create())
        {
            return rsaProvider.SignData(data, sha256);
        }
    }

    public bool VerifyData(byte[] data, byte[] signature)
    {
        using (var sha256 = SHA256.Create())
        {
            return rsaProvider.VerifyData(data, sha256, signature);
        }
    }
}