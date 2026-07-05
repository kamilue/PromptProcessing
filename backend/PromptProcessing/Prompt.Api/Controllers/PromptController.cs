using Microsoft.AspNetCore.Mvc;
using Prompt.Application.DTOs;
using Prompt.Application.Interfaces;

namespace Prompt.Api.Controllers;

[ApiController]
[Route("api/prompts")]
public class PromptController : ControllerBase
{
    private readonly IPromptService _service;

    public PromptController(IPromptService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<PromptDto>> Create(CreatePromptRequest request)
    {
        var result = await _service.CreateAsync(request);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<PromptDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PromptDto>> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);

        return Ok(result);
    }
}