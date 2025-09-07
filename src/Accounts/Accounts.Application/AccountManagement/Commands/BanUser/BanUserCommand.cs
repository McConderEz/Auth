using Core.Abstractions;

namespace Accounts.Application.AccountManagement.Commands.BanUser;

public record BanUserCommand(Guid UserId): ICommand;