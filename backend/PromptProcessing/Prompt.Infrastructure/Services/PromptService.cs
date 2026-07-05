using Microsoft.EntityFrameworkCore;
using Prompt.Application.DTOs;
using Prompt.Application.Interfaces;
using Prompt.Domain.Entities;
using Prompt.Domain.Enums;
using Prompt.Infrastructure.Persistence;
using Prompt.Application.Interfaces;

namespace Prompt.Infrastructure.Services;

public class PromptService : IPromptService
{
    private readonly PromptDbContext _context;

    public PromptService(
    PromptDbContext context)
    {
        _context = context;
    }

    public async Task<PromptDto> CreateAsync(CreatePromptRequest request)
    {
        var entity = new PromptJob
        {
            Id = Guid.NewGuid(),
            Prompt = request.Prompt,
            Status = PromptStatus.Pending
        };

        if (!await _context.Database.CanConnectAsync())
        {
            throw new InvalidOperationException("Cannot connect to the database. Ensure the database server is running and the connection string is correct.");
        }

        var strategy = _context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            _context.PromptJobs.Add(entity);
            await _context.SaveChangesAsync();
        });

        return Map(entity);
    }

    public async Task<List<PromptDto>> GetAllAsync()
    {
        return await _context.PromptJobs
            .OrderByDescending(x => x.CreatedAtUtc)
            .Select(x => Map(x))
            .ToListAsync();
    }

    public async Task<PromptDto> GetByIdAsync(Guid id)
    {
        var entity = await _context.PromptJobs.FindAsync(id);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Prompt job with id {id} was not found.");
        }

        return Map(entity);
    }

    private static PromptDto Map(PromptJob entity)
    {
        return new PromptDto
        {
            Id = entity.Id,
            Prompt = entity.Prompt,
            Response = entity.Response,
            Status = entity.Status,
            CreatedAtUtc = entity.CreatedAtUtc,
            StartedAtUtc = entity.StartedAtUtc,
            CompletedAtUtc = entity.CompletedAtUtc,
            ErrorMessage = entity.ErrorMessage
        };
    }
}