using DB1.Core.Communication;
using DB1.Core.Messages;
using MediatR;

namespace DB1.Core.Mediator
{
    public class MediatorHandler(IMediator mediator) : IMediatorHandler
    {
        public async Task<ResponseResult> EnviarComando<T>(T comando) where T : Command
        {
            return await mediator.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            System.Diagnostics.Debug.WriteLine($"PublicarEvento - IMediator");
            await mediator.Publish(evento);
        }
    }
}
