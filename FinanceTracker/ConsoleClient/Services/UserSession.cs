using FinanceTracker.ConsoleClient.Abstractions;

namespace FinanceTracker.ConsoleClient.Services;

public class UserSession : IUserSession
{
    private Guid _currentUserId = Guid.Empty;
    private string _currentUserName = string.Empty;

    public Guid CurrentUserId => _currentUserId;
    public string CurrentUserName => _currentUserName;
    public bool IsLoggedIn => _currentUserId != Guid.Empty;

    public void SetUser(Guid userId, string userName)
    {
        _currentUserId = userId;
        _currentUserName = userName;
    }

    public void ClearUser()
    {
        _currentUserId = Guid.Empty;
        _currentUserName = string.Empty;
    }
}
