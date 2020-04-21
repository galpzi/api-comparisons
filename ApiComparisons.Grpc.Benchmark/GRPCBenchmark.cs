using ApiComparisons.Grpc.Client;
using ApiComparisons.Shared.GRPC.Models;
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
        [Params(100, 500, 1000)]
        public int Iterations;
        private DummyGrpcClient client;

        [GlobalSetup]
        public void Setup()
        {
            var settings = new AppSettings { ServerUri = string.Empty };
            this.client = new DummyGrpcClient(Options.Create(settings));
        }

        [Benchmark(Baseline = true)]
        public async Task<PersonResponse> GetPeopleOnceAsync()
        {
            return await this.client.GetPeopleAsync(Guid.Empty);
        }

        [Benchmark]
        public async Task GetPeopleAsync()
        {
            for (int i = 0; i < Iterations; i++)
                await this.client.GetPeopleAsync(Guid.Empty);
        }

        //[Benchmark]
        public async Task GetTransactionsAsync()
        {
            for (int i = 0; i < Iterations; i++)
                await this.client.GetTransactionsAsync(Guid.Empty);
        }

        //[Benchmark]
        public async Task GetStoresAsync()
        {
            for (int i = 0; i < Iterations; i++)
                await this.client.GetStoresAsync(new Shared.DAL.Product());
        }

        //[Benchmark]
        public async Task GetProductsAsync()
        {
            for (int i = 0; i < Iterations; i++)
                await this.client.GetProductsAsync(new Shared.DAL.Purchase());
        }

        //[Benchmark]
        public async Task GetPurchasesAsync()
        {
            for (int i = 0; i < Iterations; i++)
                await this.client.GetPurchasesAsync(new Shared.DAL.Transaction());
        }
    }
}
