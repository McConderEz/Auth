using Core.Abstractions;
using Core.DTOs.ValueObjects;
using SharedKernel.Shared.ValueObjects;

namespace Accounts.Application.AccountManagement.Commands.Register;

public record RegisterUserCommand(
    string Email,
    string UserName,
    FullNameDto FullNameDto,
    string Password): ICommand;