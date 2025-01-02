using Blockchain.Business.Interfaces.Transactions;
using Blockchain.Business.Mappers;
using Blockchain.Business.Models;
using Blockchain.Data.Entities;
using Blockchain.Data.Interfaces;

namespace Blockchain.Business.Services;

public class WalletService(IUnitOfWork unitOfWork, IMapper<WalletModel, Wallet> mapper)
    : IWalletService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper<WalletModel, Wallet> _mapper = mapper;

    public async Task AddAsync(WalletModel wallet)
    {
        var walletEntity = _mapper.Map(wallet);
        await _unitOfWork.GetRepository<IWalletRepository<Wallet>>().AddAsync(walletEntity);
        await _unitOfWork.CommitAsync();
    }

    public async Task<WalletModel?> GetByNickNameAsync(string nickName)
    {
        var wallet = await _unitOfWork
            .GetRepository<IWalletRepository<Wallet>>()
            .GetByNickNameAsync(nickName);

        return wallet is null ? null : _mapper.Map(wallet);
    }

    public async Task<WalletModel?> GetByIdAsync(string id)
    {
        var wallet = await _unitOfWork.GetRepository<IWalletRepository<Wallet>>().GetByIdAsync(id);

        return wallet is null ? null : _mapper.Map(wallet);
    }

    public async Task UpdateAsync(WalletModel wallet)
    {
        var walletEntity = _mapper.Map(wallet);
        _unitOfWork.GetRepository<IWalletRepository<Wallet>>().Update(walletEntity);
        await _unitOfWork.CommitAsync();
    }
}
