using ApiComparisons.Grpc.Client;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace ApiComparisons.Grpc.Benchmark
{
    [CsvExporter]
    [HtmlExporter]
    [AsciiDocExporter]
    public class GRPCBenchmark
    {
        [Params(100, 200)]
        public int Iterations;

        private readonly DummyGrpcClient client;

        public GRPCBenchmark()
        {
            this.client = new DummyGrpcClient(Options.Create(new AppSettings
            {
                ServerUri = "http://localhost:5000"
            }));
        }

        [Benchmark]
        public async Task GetPeopleAsync()
        {
            for (int i = 0; i < Iterations; i++)
                await this.client.GetPeopleAsync(Guid.Empty);
        }

        [Benchmark]
        public async Task GetTransactionsAsync()
        {
            for (int i = 0; i < Iterations; i++)
                await this.client.GetTransactionsAsync(Guid.Empty);
        }

        [Benchmark]
        public async Task GetStoresAsync()
        {
            for (int i = 0; i < Iterations; i++)
                await this.client.GetStoresAsync(new Shared.DAL.Product());
        }

        [Benchmark]
        public async Task GetProductsAsync()
        {
            for (int i = 0; i < Iterations; i++)
                await this.client.GetProductsAsync(new Shared.DAL.Purchase());
        }

        [Benchmark]
        public async Task GetPurchasesAsync()
        {
            for (int i = 0; i < Iterations; i++)
                await this.client.GetPurchasesAsync(new Shared.DAL.Transaction());
        }
    }
}
