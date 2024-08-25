using AutoMapper;
using AutoMapper.QueryableExtensions;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;

namespace DB1.Desafio.Application.Commands.Empresa.ObterTodos
{
    public interface IObterTodosEmpresaUseCase
    {
        Task<ResponseResult<IEnumerable<ObterTodosEmpresaResponse>>> ExecutarAsync();
    }

    public class ObterTodosEmpresaUseCase(
        IEmpresaRepository empresaRepository,
        IMapper mapper) : CommandUseCase, IObterTodosEmpresaUseCase
    {
        public async Task<ResponseResult<IEnumerable<ObterTodosEmpresaResponse>>> ExecutarAsync()
        {
            var response = await empresaRepository
                .ObterListaQueryableAsync(p =>
                    p.ProjectTo<ObterTodosEmpresaResponse>(mapper.ConfigurationProvider));
            return response.ToResponseResult();
        }
    }

    public class ObterTodosEmpresaResponse
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Cnpj { get; set; }
        public DateTime DataFundacao { get; set; }
        public string Status { get; set; }
    }


}
