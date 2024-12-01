using System.Reflection;
using Blockchain.Data.Attributes;
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

    protected void GetRepositories()
    {
        var types =
            Assembly
                .GetAssembly(typeof(UnitOfWork))
                ?.GetTypes()
                .Where(x => x.GetCustomAttributes<RepositoryAttribute>().Any())
            ?? throw new NullReferenceException("No repositories found");

        foreach (var type in types)
        {
            Repositories.Add(
                type.GetCustomAttribute<RepositoryAttribute>()?.Name
                    ?? throw new ArgumentNullException($"{type.Name} attribute naming violation"),
                type
            );
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
