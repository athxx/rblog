using System;
using System.Security.Cryptography;
using System.Text;

namespace Core.Util;

public static class Id
{
    private const string Alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyz-";
    private const int Size = 21;

    private static readonly RandomNumberGenerator Rng = new RNGCryptoServiceProvider();

    public static string NanoId()
    {
        byte[] bytes = new byte[Size];
        Rng.GetBytes(bytes);

        StringBuilder sb = new StringBuilder(Size);

        for (int i = 0; i < Size; i++)
        {
            int index = bytes[i] % Alphabet.Length;
            sb.Append(Alphabet[index]);
        }

        return sb.ToString();
    }

    public static string Uuid()
    {
        return Guid.NewGuid().ToString();
    }
}