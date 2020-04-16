using BenchmarkDotNet.Running;

namespace ApiComparisons.Grpc.Benchmark
{
    class Program
    {
        static void Main(string[] args) => BenchmarkRunner.Run<GRPCBenchmark>();
    }
}
