using AutoMapper;
using AutoMapper.QueryableExtensions;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;

namespace DB1.Desafio.Application.Commands.Funcionario.ObterTodos
{
    public class ObterTodosFuncionarioUseCase(
        IFuncionarioRepository funcionarioRepository,
        IMapper mapper) : CommandUseCase, IObterTodosFuncionarioUseCase
    {
        public async Task<ResponseResult<IEnumerable<ObterTodosFuncionarioResponse>>> ExecutarAsync()
        {
            var response = await funcionarioRepository
                .ObterListaQueryableAsync(p =>
                    p.ProjectTo<ObterTodosFuncionarioResponse>(mapper.ConfigurationProvider));
            return response.ToResponseResult();
        }
    }
}
