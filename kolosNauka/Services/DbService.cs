using kolosNauka.Infastructure;
using Microsoft.EntityFrameworkCore;
using kolosNauka.DTOs;
using kolosNauka.Entities;
using kolosNauka.Exceptions;

namespace kolosNauka.Services;

public class DbService(DatabaseContext ctx) : IDbService
{
    public async Task<ICollection<PCResponse>> GetAllPCsAsync(string? name, CancellationToken cancellationToken)
    {
        return await ctx.PCs
            .Where(e => name == null || e.Name.Contains(name))
            .Select(e => new PCResponse(
                e.Id,
                e.Name,
                e.Weight,
                e.Warranty,
                e.CreatedAt,
                e.Stock
            )).ToListAsync(cancellationToken);
    }

    public async Task<PCResponse> GetPCAsync(int id, CancellationToken cancellationToken)
    {
        return await ctx.PCs
                   .Where(e => e.Id == id)
                   .Select(e => new PCResponse(
                       e.Id,
                       e.Name,
                       e.Weight,
                       e.Warranty,
                       e.CreatedAt,
                       e.Stock
                   )).FirstOrDefaultAsync(cancellationToken)
               ?? throw new NotFoundException($"PC with id {id} not found");
    }

    public async Task AddPcAsync(PCRequest request, CancellationToken cancellationToken)
    {
        if (await ctx.PCs.AnyAsync(e => e.Name == request.Name.Trim().ToLower(), cancellationToken))
        {
            throw new ConflictException($"PC with title {request.Name} already exists");
        }

        var pc = new PC
        {
            Name = request.Name,
            Weight = request.Weight,
            Warranty = request.Warranty,
            CreatedAt = request.CreatedAt,
            Stock = request.Stock
        };
        
        await  ctx.PCs.AddAsync(pc, cancellationToken);
        await ctx.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdatePcAsync(int id, PCRequest request, CancellationToken cancellationToken)
    {
        if (await ctx.PCs.AnyAsync(e => e.Name == request.Name.Trim().ToLower(), cancellationToken))
        {
            throw new ConflictException($"PC with title {request.Name} already exists");
        }

        var pc = await ctx.PCs
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        
        if (pc == null)
        {
            throw new NotFoundException($"PC with id {id} not found");
        }
        
        pc.Name = request.Name;
        pc.Weight = request.Weight;
        pc.Warranty = request.Warranty;
        pc.CreatedAt = request.CreatedAt;
        pc.Stock = request.Stock;
        
        await ctx.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePcAsync(int id, CancellationToken cancellationToken)
    {
        var transaction = await ctx.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var removedRows = await ctx.PCs.Where(e => e.Id == id).ExecuteDeleteAsync(cancellationToken);

            if (removedRows == 0)
            {
                throw new NotFoundException($"PC with id {id} not found");
            }

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
    


}