namespace AccountService.Dtos;

public class GetAccountResponse
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }

    public GetAccountResponse() { AccountNumber = string.Empty; Name = string.Empty; }
}