public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Role { get; set; } = "User";

    // Construtor para garantir que todas as propriedades obrigatórias sejam fornecidas
    public User(string username, string email, string passwordHash, DateTime dateOfBirth)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username), "Username is required");
        Email = email ?? throw new ArgumentNullException(nameof(email), "Email is required");
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash), "PasswordHash is required");
        DateOfBirth = dateOfBirth;
    }
}
