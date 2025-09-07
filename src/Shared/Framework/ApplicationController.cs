using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Framework;

[ApiController]
[Route("api/[controller]")]
public abstract class ApplicationController: ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);

        return base.Ok(envelope);
    }
    
    
}