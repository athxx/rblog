namespace Core.Util.Crypt;

public class TLV
{
    public static byte[] Encode(Dictionary<byte[], byte[]> data)
    {
        List<byte> tlvBytes = new List<byte>();
        foreach (var pair in data)
        {
            tlvBytes.AddRange(pair.Key);
            tlvBytes.AddRange(BitConverter.GetBytes(pair.Value.Length));
            tlvBytes.AddRange(pair.Value);
        }

        return tlvBytes.ToArray();
    }

    public static Dictionary<byte[], byte[]> Decode(byte[] tlvBytes)
    {
        Dictionary<byte[], byte[]> data = new Dictionary<byte[], byte[]>();
        int index = 0;
        while (index < tlvBytes.Length)
        {
            byte[] tag = new byte[2];
            Buffer.BlockCopy(tlvBytes, index, tag, 0, 2);
            index += 2;

            int length = BitConverter.ToInt32(tlvBytes, index);
            index += 4;

            byte[] value = new byte[length];
            Buffer.BlockCopy(tlvBytes, index, value, 0, length);
            index += length;

            data.Add(tag, value);
        }

        return data;
    }
}