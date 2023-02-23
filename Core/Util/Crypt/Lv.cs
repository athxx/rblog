namespace Core.Util.Crypt;

public class LV
{
    public static byte[] Encode(byte[] data)
    {
        List<byte> lvBytes = new List<byte>();
        lvBytes.AddRange(BitConverter.GetBytes(data.Length));
        lvBytes.AddRange(data);
        return lvBytes.ToArray();
    }

    public static byte[] Decode(byte[] lvBytes)
    {
        int length = BitConverter.ToInt32(lvBytes, 0);
        byte[] value = new byte[length];
        Buffer.BlockCopy(lvBytes, 4, value, 0, length);
        return value;
    }
}