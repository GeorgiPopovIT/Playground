using Grpc.Core;
    
namespace Grpc_Web_Demo.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        
        public GreeterService(){ }
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
