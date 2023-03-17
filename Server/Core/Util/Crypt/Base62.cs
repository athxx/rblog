namespace Core.Util.Crypt;

public class Base62
{
    private const string Characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    private static readonly char[] CharArray = Characters.ToCharArray();
    private static readonly int Base = CharArray.Length;

    public static string Encode(long number)
    {
        if (number < 0)
            throw new ArgumentOutOfRangeException(nameof(number), "Number cannot be negative.");

        if (number == 0)
            return "0";

        var encoded = string.Empty;

        while (number > 0)
        {
            int remainder = (int)(number % Base);
            number /= Base;
            encoded = CharArray[remainder] + encoded;
        }

        return encoded;
    }

    public static long Decode(string encoded)
    {
        if (string.IsNullOrEmpty(encoded))
            throw new ArgumentNullException(nameof(encoded), "Encoded value cannot be null or empty.");

        long decoded = 0;

        for (int i = 0; i < encoded.Length; i++)
        {
            int charValue = Characters.IndexOf(encoded[i]);

            if (charValue == -1)
                throw new ArgumentException("Invalid character in encoded value.", nameof(encoded));

            decoded = (decoded * Base) + charValue;
        }

        return decoded;
    }
}