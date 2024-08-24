using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB1.Desafio.Application.Commands.Empresa.Excluir
{
    public interface IExcluirEmpresaUseCase
    {
        Task<ResponseResult> ExecutarAsync(Guid id);
    }

    public class ExcluirEmpresaUseCase(
        IEmpresaRepository empresaRepository) : CommandUseCase, IExcluirEmpresaUseCase
    {
        public async Task<ResponseResult> ExecutarAsync(Guid id)
        {
            if (id == Guid.Empty) return ResponseResultError("Informe o ID corretamente");

            var entidade = await empresaRepository.ObterPorIdAsync(id);

            if (entidade is null) return ResponseResultError("Registro não encontrado");

            empresaRepository.Remover(entidade);

            var result = await PersistirDados(empresaRepository.UnitOfWork);
            return result.ToResponseResult();
        }
    }
}
