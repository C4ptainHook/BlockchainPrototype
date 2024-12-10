using Blockchain.Business.Models;
using Blockchain.Data.Entities;
using MongoDB.Bson;
using Riok.Mapperly.Abstractions;

namespace Blockchain.Business.Mappers;

[Mapper]
public partial class BlockBusinessMapper : IMapper<BlockModel, Block>
{
    private readonly IMapper<TransactionModel, Transaction> _mapper;

    public BlockBusinessMapper(IMapper<TransactionModel, Transaction> mapper)
    {
        _mapper = mapper;
    }

    [MapperIgnoreSource(nameof(BlockModel.Id))]
    [MapperIgnoreTarget(nameof(Block.Id))]
    public partial Block Map(BlockModel from);

    public partial BlockModel Map(Block to);

    [UserMapping]
    private ObjectId IdToObjectId(string id) => new ObjectId(id);

    public partial IEnumerable<Block> Map(IEnumerable<BlockModel> from);

    public partial IEnumerable<BlockModel> Map(IEnumerable<Block> from);
}
