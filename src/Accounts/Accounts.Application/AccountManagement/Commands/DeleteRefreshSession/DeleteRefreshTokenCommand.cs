using Core.Abstractions;

namespace Accounts.Application.AccountManagement.Commands.DeleteRefreshSession;

public record DeleteRefreshTokenCommand(Guid RefreshToken) : ICommand;
