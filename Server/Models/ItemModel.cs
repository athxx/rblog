namespace Models;

public class Item
{
    public Guid ItemId { get; set; }
    public Guid StoreId { get; set; }
    public string StoreName { get; set; }
    public int RegisterTime { get; set; }
    public int StartTime { get; set; }
    public int CloseTime { get; set; }
    public int RestDay{ get; set; }
    public int State { get; set; }
}