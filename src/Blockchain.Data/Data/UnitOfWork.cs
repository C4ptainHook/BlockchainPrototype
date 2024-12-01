using System.Reflection;
using Blockchain.Data.Interfaces;
using Blockchain.Data.Repositories;

namespace Blockchain.Data.Data;

public class UnitOfWork : IUnitOfWork
{
    private BlockchainContext _context;
    public Dictionary<string, Type> Repositories { get; }

    public UnitOfWork(BlockchainContext context)
    {
        _context = context;
        Repositories = new Dictionary<string, Type>();
        GetRepositories();
    }

    private void GetRepositories()
    {
        var types =
            Assembly
                .GetAssembly(typeof(UnitOfWork))
                ?.GetTypes()
                .Where(x =>
                    x.BaseType != null
                    && x.BaseType.IsGenericType
                    && x.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<>)
                ) ?? throw new NullReferenceException("No repositories found");

        foreach (var type in types)
        {
            Repositories.Add(type.Name.Replace("Repository", string.Empty), type);
        }
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
