using System;
using System.Security.Cryptography;

namespace Core.Util.Crypt;

public class TOTP
{
    private const int DefaultTimeStepSeconds = 30;
    private const int DefaultDigits = 6;

    private readonly byte[] _secret;
    private readonly int _timeStepSeconds;
    private readonly int _digits;

    public TOTP(string secretBase32)
        : this(Base32.Decode(secretBase32))
    {
    }

    public TOTP(byte[] secret, int timeStepSeconds = DefaultTimeStepSeconds, int digits = DefaultDigits)
    {
        _secret = secret;
        _timeStepSeconds = timeStepSeconds;
        _digits = digits;
    }

    public string GenerateCode()
    {
        return GenerateCode(DateTime.UtcNow);
    }

    public string GenerateCode(DateTime timestamp)
    {
        byte[] timestampBytes = BitConverter.GetBytes((long)(timestamp - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds / _timeStepSeconds);
        Array.Reverse(timestampBytes);

        HMACSHA1 hmac = new HMACSHA1(_secret);
        byte[] hash = hmac.ComputeHash(timestampBytes);

        int offset = hash[hash.Length - 1] & 0x0F;
        int binaryCode = ((hash[offset] & 0x7F) << 24) | ((hash[offset + 1] & 0xFF) << 16) | ((hash[offset + 2] & 0xFF) << 8) | (hash[offset + 3] & 0xFF);

        int code = binaryCode % (int)Math.Pow(10, _digits);

        return code.ToString($"D{_digits}");
    }
}