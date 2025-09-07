using Accounts.Application.AccountManagement.Commands.BanUser;
using Accounts.Application.AccountManagement.Commands.DeleteRefreshSession;
using Accounts.Application.AccountManagement.Commands.Login;
using Accounts.Application.AccountManagement.Commands.Refresh;
using Accounts.Application.AccountManagement.Commands.Register;
using Accounts.Contracts.Requests;
using Accounts.Presentation.Requests;
using Core.DTOs.ValueObjects;
using Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Presentation;

public class AccountController : ApplicationController
{
    [HttpPost("registration")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] RegisterUserHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new RegisterUserCommand(
            request.Email,
            request.UserName,
            new FullNameDto(request.FirstName, request.SecondName, request.Patronymic),
            request.Password);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Errors.ToResponse();

        return Ok(result.IsSuccess);
    }

    [HttpPost("authentication")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserRequest request,
        [FromServices] LoginUserHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new LoginUserCommand(request.Email, request.Password);

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Errors.ToResponse();

        HttpContext.Response.Cookies.Append("refreshToken", result.Value.RefreshToken.ToString());

        return Ok(result.Value);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Delete(
        [FromServices] DeleteRefreshTokenHandler handler,
        CancellationToken cancellationToken = default)
    {
        if (!HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
        {
            return Unauthorized();
        }

        var command = new DeleteRefreshTokenCommand(Guid.Parse(refreshToken));

        HttpContext.Response.Cookies.Delete("refreshToken");

        var result = await handler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Errors.ToResponse();

        return Ok(result);
    }

    [HttpPost("refreshing")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh(
        [FromServices] RefreshTokensHandler handler,
        CancellationToken cancellationToken = default)
    {
        if (!HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
        {
            return Unauthorized();
        }

        var result = await handler.Handle(
            new RefreshTokensCommand(Guid.Parse(refreshToken)),
            cancellationToken);

        if (result.IsFailure)
            return result.Errors.ToResponse();

        HttpContext.Response.Cookies.Append("refreshToken", result.Value.RefreshToken.ToString());

        return Ok(result.Value);
    }

}