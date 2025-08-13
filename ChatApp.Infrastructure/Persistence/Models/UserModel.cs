namespace ChatApp.Infrastructure.Model;

public class UserModel : AbstractModel
{

    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}