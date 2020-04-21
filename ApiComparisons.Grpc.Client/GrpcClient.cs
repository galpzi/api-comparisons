using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using System;

namespace ApiComparisons.Grpc.Client
{
    public abstract class GrpcClient
    {
        protected readonly AppSettings settings;
        protected readonly GrpcChannel channel;

        protected GrpcClient(IOptions<AppSettings> options)
        {
            // Necessary to call insecure gRPC services. Use 'http' in the server address
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            this.settings = options.Value;
            this.channel = GrpcChannel.ForAddress(this.settings.ServerUri);
        }
    }
}
