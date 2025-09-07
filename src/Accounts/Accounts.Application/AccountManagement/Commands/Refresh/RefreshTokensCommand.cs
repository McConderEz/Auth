using Core.Abstractions;

namespace Accounts.Application.AccountManagement.Commands.Refresh;

public record RefreshTokensCommand(Guid RefreshToken) : ICommand;