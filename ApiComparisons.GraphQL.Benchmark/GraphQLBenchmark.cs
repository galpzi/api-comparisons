using BenchmarkDotNet.Attributes;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using System;
using System.Threading.Tasks;

namespace ApiComparisons.GraphQL.Benchmark
{
    [CsvExporter]
    [HtmlExporter]
    [AsciiDocExporter]
    public class GraphQLBenchmark
    {
        [Params(100, 200)]
        public int Iterations;

        private readonly GraphQLHttpClient client;

        public GraphQLBenchmark()
        {
            this.client = new GraphQLHttpClient("http://localhost:5000/graphql", new SystemTextJsonSerializer());
        }

        [Benchmark]
        public async Task GetPeopleAsync() => await SendRequestAsync(new GraphQLRequest
        {
            Query = @"
            {
                people {
                id
                name
                created   
                }
            }"
        });

        [Benchmark]
        public async Task GetTransactionsAsync() => await SendRequestAsync(new GraphQLRequest
        {
            Query = @"
            {
                transactions {
                id
                personID
                total
                created
                }
            }"
        });

        [Benchmark]
        public async Task GetStoresAsync() => await SendRequestAsync(new GraphQLRequest
        {
            Query = @"
            {
                stores {
                id
                name
                country
                address    
                created
                }
            }"
        });

        [Benchmark]
        public async Task GetProductsAsync() => await SendRequestAsync(new GraphQLRequest
        {
            Query = @"
            {
              products {
                id
                storeID
                name
                description
                created
              }
            }"
        });

        [Benchmark]
        public async Task GetPurchasesAsync() => await SendRequestAsync(new GraphQLRequest
        {
            Query = @"
            {
              purchases {
                productID
                transactionID
                count
                price
                created
              }
            }"
        });

        [Benchmark]
        public async Task GetPersonTransactionsAsync() => await SendRequestAsync(new GraphQLRequest
        {
            Query = @"
            query person($id: Guid!) {
              person(id: $id) {
                id
                name
                transactions {
                  id
                  total
                  created
                  purchases {
                    count
                    price
                    created
                    product {
                      id
                      name
                      price
                      created
                      description
                      store {
                        id
                        name
                        country
                        address            
                      }
                    }
                  }
                }
              }
            }",
            Variables = new
            {
                id = Guid.Empty.ToString()
            }
        });

        private async Task SendRequestAsync(GraphQLRequest request)
        {
            for (int i = 0; i < Iterations; i++)
                await this.client.SendQueryAsync<dynamic>(request);
        }
    }
}
