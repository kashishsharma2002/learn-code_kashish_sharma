namespace FinanceTracker.ConsoleClient.Abstractions;

public interface IUserSession
{
    Guid CurrentUserId { get; }
    string CurrentUserName { get; }
    bool IsLoggedIn { get; }

    void SetUser(Guid userId, string userName);
    void ClearUser();
}
