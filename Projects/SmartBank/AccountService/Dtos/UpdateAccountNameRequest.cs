namespace AccountService.Dtos;

public class UpdateAccountNameRequest
{
    public int AccountId { get; set; }
    public string Name { get; set; }
    public UpdateAccountNameRequest() { Name = string.Empty; }
}