namespace Models;

public class User
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int State { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime UpdateTime { get; set; }
}