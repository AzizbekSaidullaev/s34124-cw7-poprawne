using kolosNauka.DTOs;

namespace kolosNauka.Services;

public interface IDbService
{
    Task<ICollection<PCResponse>> GetAllPCsAsync(string? name, CancellationToken cancellationToken);
    Task<PCResponse> GetPCAsync(int id, CancellationToken cancellationToken);
    Task AddPcAsync(PCRequest request, CancellationToken cancellationToken);
    Task UpdatePcAsync(int id, PCRequest request, CancellationToken cancellationToken);
    Task DeletePcAsync(int id, CancellationToken cancellationToken);
}