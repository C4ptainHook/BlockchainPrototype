using Blockchain.Business.Interfaces.Transactions;
using Blockchain.Business.Mappers;
using Blockchain.Business.Models;
using Blockchain.Data.Entities;
using Blockchain.Data.Interfaces;

namespace Blockchain.Business.Services;

public class WalletService : IWalletService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper<WalletModel, Wallet> _mapper;

    public WalletService(IUnitOfWork unitOfWork, IMapper<WalletModel, Wallet> mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddAsync(WalletModel wallet)
    {
        var walletEntity = _mapper.Map(wallet);
        await _unitOfWork
            .GetRepository<IWalletRepository<Wallet>>($"{nameof(Wallet)}Repository")
            .AddAsync(walletEntity);
        await _unitOfWork.CommitAsync();
    }
}
