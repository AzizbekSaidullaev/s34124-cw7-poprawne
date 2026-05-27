using kolosNauka.DTOs;
using kolosNauka.Exceptions;
using kolosNauka.Services;
using Microsoft.AspNetCore.Mvc;

namespace kolosNauka.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PCController(IDbService service) : ControllerBase
{
    [HttpGet]
    public Task<ICollection<PCResponse>> GetAllPCsAsync([FromQuery] string? name, CancellationToken cancellationToken)
        => service.GetAllPCsAsync(name, cancellationToken);

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPCAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await service.GetPCAsync(id, cancellationToken));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPcAsync([FromBody] PCRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await service.AddPcAsync(request, cancellationToken);
            return Created();
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePcAsync([FromRoute] int id, [FromBody] PCRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            await service.UpdatePcAsync(id, request, cancellationToken);
            return NoContent();
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePcAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            await service.DeletePcAsync(id, cancellationToken);
            return  NoContent();
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}