namespace Core.Util.Crypt;

public class Base64
{
    public static string Encode(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
            return plainText;

        byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string Decode(string base64EncodedData)
    {
        if (string.IsNullOrEmpty(base64EncodedData))
            return base64EncodedData;

        try
        {
            byte[] base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        catch (FormatException)
        {
            throw new ArgumentException("Invalid base64-encoded data.", nameof(base64EncodedData));
        }
    }
}