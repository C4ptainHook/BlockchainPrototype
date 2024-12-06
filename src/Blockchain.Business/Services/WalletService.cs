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
        await _unitOfWork
            .GetRepository<IWalletRepository<Wallet>>($"{nameof(Wallet)}Repository")
            .AddAsync(walletEntity);
        await _unitOfWork.CommitAsync();
    }

    public async Task<string> GetIdByNickNameAsync(string nickName)
    {
        var wallet = await _unitOfWork
            .GetRepository<IWalletRepository<Wallet>>($"{nameof(Wallet)}Repository")
            .GetByNickNameAsync(nickName);

        return wallet.Id.ToString();
    }
}
