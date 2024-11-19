using Blockchain.Business.CryptoChain;
using Blockchain.Business.Mining;
using Blockchain.Business.ProofOfWork;
using Blockchain.Business.ProofOfWork.Factories;
using Blockchain.Business.RandomWrappers;
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
            builder.Services.AddTransient<IBlockChain, BlockChain>();
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
