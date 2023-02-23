namespace Core.Util.Crypt;

public static class Base32
{
    private const string Base32Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

    public static byte[] Decode(string input)
    {
        input = input.TrimEnd('=');
        int byteCount = input.Length * 5 / 8;
        byte[] output = new byte[byteCount];

        byte[] alphabet = GetAlphabet();

        int inputOffset = 0;
        int outputOffset = 0;
        int bits = 0;
        int bitsRemaining = 0;

        while (outputOffset < byteCount && inputOffset < input.Length)
        {
            int b = alphabet[input[inputOffset] - 'A'];
            if (b < 0)
                throw new FormatException("Invalid base32 character");

            if (bitsRemaining > 0)
            {
                bits |= b >> (8 - bitsRemaining);
                bitsRemaining += 5;
            }
            else
            {
                bits = (b & 0x1F) << 3;
                bitsRemaining = 5;
            }

            if (bitsRemaining >= 8)
            {
                output[outputOffset++] = (byte)(bits >> (bitsRemaining - 8));
                bitsRemaining -= 8;
            }

            inputOffset++;
        }

        if (bitsRemaining > 0)
        {
            output[outputOffset++] = (byte)(bits << (8 - bitsRemaining));
        }

        return output;
    }

    private static byte[] GetAlphabet()
    {
        byte[] alphabet = new byte[256];
        for (int i = 0; i < alphabet.Length; i++)
        {
            alphabet[i] = 0xFF;
        }

        for (int i = 0; i < Base32Alphabet.Length; i++)
        {
            alphabet[Base32Alphabet[i]] = (byte)i;
        }

        return alphabet;
    }
}