using BenchmarkDotNet.Running;

namespace ApiComparisons.GraphQL.Benchmark
{
    class Program
    {
        static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(GraphQLBenchmark).Assembly).Run(args);
    }
}
