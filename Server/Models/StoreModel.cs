namespace Models;

public class Store
{
    public Guid StoreID { get; set; }
    public Guid UserId { get; set; }
    public string StoreName { get; set; }
    public int RegisterTime { get; set; }
    public int StartTime { get; set; }
    public int CloseTime { get; set; }
    public int RestDay{ get; set; }
    public int State { get; set; }
}