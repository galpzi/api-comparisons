using BenchmarkDotNet.Running;

namespace ApiComparisons.Grpc.Benchmark
{
    class Program
    {
        static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(GRPCBenchmark).Assembly).Run(args);
    }
}
