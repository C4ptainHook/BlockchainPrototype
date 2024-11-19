using Blockchain.Business.Interfaces;
using Blockchain.Business.Interfaces.Mining;
using Blockchain.Business.Interfaces.PoW;
using Blockchain.Business.Models;
using Blockchain.Business.Models.Block;
using Blockchain.Business.RandomWrappers;
using Blockchain.Business.Services;
using Blockchain.Business.Utils;
using Serilog;

namespace Blockchain.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Host.UseSerilog(
                (context, configuration) =>
                    configuration.ReadFrom.Configuration(context.Configuration)
            );

            builder.Services.AddTransient<IRandomNumerical<int>, RandomWrapper>();
            builder.Services.AddTransient<
                IProofOfWorkFactory<ProofOfWorkArgs>,
                BasicProofOfWorkFactory
            >();
            builder.Services.AddTransient<IBlockChain<Block>, BlockChain>();
            builder.Services.AddTransient<IMiner, Miner>();

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
}
