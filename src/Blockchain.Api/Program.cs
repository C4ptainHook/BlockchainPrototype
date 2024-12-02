using Blockchain.Business.Interfaces;
using Blockchain.Business.Interfaces.Mining;
using Blockchain.Business.Interfaces.PoW;
using Blockchain.Business.Interfaces.Utils;
using Blockchain.Business.Models;
using Blockchain.Business.RandomWrappers;
using Blockchain.Business.Services;
using Blockchain.Business.Utils;
using Serilog;

namespace Blockchain.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Host.UseSerilog(
            (context, configuration) => configuration.ReadFrom.Configuration(context.Configuration)
        );

        builder.Services.AddTransient<IRandomNumerical<int>, RandomWrapper>();
        builder.Services.AddSingleton<ITransactionService, TransactionService>();
        builder.Services.AddSingleton<
            IProofOfWorkServiceFactory<ProofOfWorkServiceArgs>,
            ProofOfWorkServiceFactory
        >();
        builder.Services.AddSingleton<IBlockchainService<Block>, BlockchainService>();
        builder.Services.AddSingleton<IMinerService, MinerService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHttpsRedirection();
        }

        app.MapControllers();

        app.Run();
    }
}
