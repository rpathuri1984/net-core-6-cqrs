using System.Text.Json.Serialization;

namespace RYB.Model.ViewModel;

public class UserToken
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }

    [JsonIgnore]
    public string PasswordHash { get; set; }

    [JsonIgnore]
    public List<RefreshToken> RefreshTokens { get; set; }
}

