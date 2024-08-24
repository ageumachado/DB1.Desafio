using DB1.Core.Communication;
using DB1.Core.Messages;

namespace DB1.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task<ResponseResult> EnviarComando<T>(T comando) where T : Command;
        Task PublicarEvento<T>(T evento) where T : Event;
    }
}
