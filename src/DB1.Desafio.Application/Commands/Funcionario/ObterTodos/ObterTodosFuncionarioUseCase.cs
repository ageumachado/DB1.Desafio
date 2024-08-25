using AutoMapper;
using AutoMapper.QueryableExtensions;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB1.Desafio.Application.Commands.Funcionario.ObterTodos
{
    public interface IObterTodosFuncionarioUseCase
    {
        Task<ResponseResult<IEnumerable<ObterTodosFuncionarioResponse>>> ExecutarAsync();
    }

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

    public class ObterTodosFuncionarioResponse
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public DateTime DataContratacao { get; set; }
        public string? Status { get; set; }
        public string? EmpresaNome { get; set; }
        public string? CargoNome { get; set; }
    }
}
