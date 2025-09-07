using Core.Abstractions;

namespace Accounts.Application.AccountManagement.Commands.Login;

public record LoginUserCommand(string Email, string Password): ICommand;