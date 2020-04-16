using BenchmarkDotNet.Running;

namespace ApiComparisons.GraphQL.Benchmark
{
    class Program
    {
        static void Main(string[] args) => BenchmarkRunner.Run<GraphQLBenchmark>();
    }
}
