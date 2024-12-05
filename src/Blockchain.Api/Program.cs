using Blockchain.Api.DTOs;
using Blockchain.Api.Mappers;
using Blockchain.Business.Interfaces.Mining;
using Blockchain.Business.Interfaces.PoW;
using Blockchain.Business.Interfaces.Transactions;
using Blockchain.Business.Interfaces.Utils;
using Blockchain.Business.Mappers;
using Blockchain.Business.Models;
using Blockchain.Business.RandomWrappers;
using Blockchain.Business.Services;
using Blockchain.Business.Utils;
using Blockchain.Data;
using Blockchain.Data.Data;
using Blockchain.Data.Entities;
using Blockchain.Data.Interfaces;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
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
        Env.Load(@"..\Env\api.env");

        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("MONGO_DB_CONNECTION_STRING")))
        {
            throw new InvalidOperationException("MongoDB connection string is not configured.");
        }
        var mongoClient = new MongoClient(
            Environment.GetEnvironmentVariable("MONGO_DB_CONNECTION_STRING")
        );

        builder.Services.AddDbContext<BlockchainContext>(
            options => options.UseMongoDB(mongoClient, "blockchain-mongo"),
            ServiceLifetime.Singleton
        );

        builder.Host.UseSerilog(
            (context, configuration) => configuration.ReadFrom.Configuration(context.Configuration)
        );

        builder.Services.AddTransient<IRandomNumerical<int>, RandomWrapper>();
        builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
        builder.Services.AddSingleton<
            IMapper<TransactionDto, TransactionModel>,
            TransactionApiMapper
        >();
        builder.Services.AddSingleton<
            IMapper<TransactionModel, Transaction>,
            TransactionBusinessMapper
        >();
        builder.Services.AddSingleton<ITransactionService, TransactionService>();
        builder.Services.AddSingleton<
            IProofOfWorkServiceFactory<ProofOfWorkServiceArgs>,
            ProofOfWorkServiceFactory
        >();
        builder.Services.AddSingleton<IMapper<BlockModel, Block>, BlockBusinessMapper>();
        builder.Services.AddSingleton<IBlockchainService<BlockModel>, BlockchainService>();
        builder.Services.AddSingleton<IMinerService, MinerService>();

        builder.Services.AddSingleton<IMapper<WalletModel, Wallet>, WalletBusinessMapper>();
        builder.Services.AddSingleton<IWalletService, WalletService>();

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
