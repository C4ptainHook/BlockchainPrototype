using System.Collections;
using Blockchain.Business.Interfaces.Mining;
using Blockchain.Business.Mappers;
using Blockchain.Business.Models;
using Blockchain.Data.Entities;
using Blockchain.Data.Interfaces;

namespace Blockchain.Business.Services;

public class BlockService : IBlockService<BlockModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper<BlockModel, Block> _mapper;

    public BlockService(IUnitOfWork unitOfWork, IMapper<BlockModel, Block> mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BlockModel?> GetLastBlockAsync()
    {
        var blockchain = await _unitOfWork.GetRepository<IBlockRepository<Block>>().GetAllAsync();
        var lastBlock = blockchain.LastOrDefault();
        return lastBlock is null ? null : _mapper.Map(lastBlock);
    }

    public async Task<BlockModel> AddBlockAsync(BlockModel newBlock)
    {
        var newblockEntity = _mapper.Map(newBlock);
        await _unitOfWork.GetRepository<IBlockRepository<Block>>().AddAsync(newblockEntity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map(newblockEntity);
    }

    public async Task<IEnumerable<BlockModel>> GetFullChainAsync()
    {
        var blockEntities = await _unitOfWork
            .GetRepository<IBlockRepository<Block>>()
            .GetAllAsync();
        return _mapper.Map(blockEntities);
    }

    public async Task<int> GetChainLength()
    {
        var blockchain = await _unitOfWork.GetRepository<IBlockRepository<Block>>().GetAllAsync();
        return blockchain is null ? 0 : blockchain.Count();
    }
}