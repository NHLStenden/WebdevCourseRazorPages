namespace NHLStendenCafe.Models;

public class CafeUser
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public string Location { get; set; }
    public DateTime Date { get; set; }
}