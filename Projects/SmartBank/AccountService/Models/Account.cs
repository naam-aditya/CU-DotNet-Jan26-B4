namespace AccountService.Models;

public class Account
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }

    public Account() { AccountNumber = string.Empty; Name = string.Empty; }
}