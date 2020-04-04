using Grpc.Net.Client;
using Microsoft.Extensions.Options;

namespace ApiComparisons.Grpc.Client
{
    public abstract class GrpcClient
    {
        protected readonly AppSettings settings;
        protected readonly GrpcChannel channel;

        protected GrpcClient(IOptions<AppSettings> options)
        {
            this.settings = options.Value;
            this.channel = GrpcChannel.ForAddress(this.settings.ServerUri);
        }
    }
}
