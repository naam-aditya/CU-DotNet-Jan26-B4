namespace AccountService.Dtos;

public class CreateAccountRequest
{
    public string Name { get; set; }
    public decimal InitialDeposit { get; set; }

    public CreateAccountRequest() { Name = string.Empty; }
}