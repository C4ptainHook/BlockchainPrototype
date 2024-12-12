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
        builder.Services.AddHttpContextAccessor();
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
            ServiceLifetime.Scoped
        );

        builder.Host.UseSerilog(
            (context, configuration) => configuration.ReadFrom.Configuration(context.Configuration)
        );

        builder.Services.AddTransient<
            IMapper<TransactionDto, TransactionModel>,
            TransactionApiMapper
        >();
        builder.Services.AddTransient<
            IMapper<TransactionModel, Transaction>,
            TransactionBusinessMapper
        >();
        builder.Services.AddTransient<IMapper<BlockModel, Block>, BlockBusinessMapper>();
        builder.Services.AddTransient<IMapper<WalletDto, WalletModel>, WalletApiMapper>();
        builder.Services.AddTransient<IMapper<WalletModel, Wallet>, WalletBusinessMapper>();
        builder.Services.AddTransient<IRandomNumerical<int>, RandomWrapper>();

        builder.Services.AddScoped<ITransactionService, TransactionService>();
        builder.Services.AddScoped<IBlockService<BlockModel>, BlockService>();
        builder.Services.AddScoped<IWalletService, WalletService>();
        builder.Services.AddScoped<ITransactionHashingService, TransactionHashingService>();
        builder.Services.AddScoped<IMinerService, MinerService>();
        builder.Services.AddScoped<
            IProofOfWorkServiceFactory<ProofOfWorkServiceArgsModel>,
            ProofOfWorkServiceFactory
        >();

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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
