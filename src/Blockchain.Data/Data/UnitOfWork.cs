using System.Reflection;
using Blockchain.Data.Attributes;
using Blockchain.Data.Entities;
using Blockchain.Data.Interfaces;
using Blockchain.Data.Repositories;

namespace Blockchain.Data.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly BlockchainContext _context;
    private readonly Dictionary<string, object> _repositories;

    public UnitOfWork(BlockchainContext context)
    {
        _context = context;
        _repositories = new Dictionary<string, object>();
        InitializeRepositories();
    }

    protected void InitializeRepositories()
    {
        var types =
            Assembly
                .GetAssembly(typeof(UnitOfWork))
                ?.GetTypes()
                .Where(x => x.GetCustomAttributes<RepositoryAttribute>().Any())
            ?? throw new NullReferenceException("No repositories found");

        foreach (var type in types)
        {
            var attribute = type.GetCustomAttribute<RepositoryAttribute>();
            var name =
                attribute?.Name
                ?? throw new ArgumentNullException($"{type.Name} attribute naming violation");
            var instance =
                Activator.CreateInstance(type, _context)
                ?? throw new NullReferenceException($"{type.Name} instance creation failed");
            _repositories.Add(name, instance);
        }
    }

    public T GetRepository<T>(string repositoryName)
        where T : class
    {
        if (_repositories.TryGetValue(repositoryName, out var repository))
        {
            return (T)repository;
        }
        throw new KeyNotFoundException($"{repositoryName} repository not found");
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
