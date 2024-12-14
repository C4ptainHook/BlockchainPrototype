using Blockchain.Business.Interfaces.Network;

namespace Blockchain.Business.Services;

public class NodeService : INodeService
{
    private readonly ICollection<string> _nodes = new List<string>();

    public void RegisterNode(string address)
    {
        _nodes.Add(address);
    }
}
