namespace trabalho_web.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string username);
    }
}
