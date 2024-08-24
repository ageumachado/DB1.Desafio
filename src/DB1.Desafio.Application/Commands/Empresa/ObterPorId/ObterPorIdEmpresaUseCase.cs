using AutoMapper;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB1.Desafio.Application.Commands.Empresa.ObterPorId
{
    public interface IObterPorIdEmpresaUseCase
    {
        Task<ResponseResult<ObterPorIdEmpresaResponse>> ExecutarAsync(Guid id);
    }

    public class ObterPorIdEmpresaUseCase(
        IEmpresaRepository empresaRepository,
        IMapper mapper) : CommandUseCase, IObterPorIdEmpresaUseCase
    {
        public async Task<ResponseResult<ObterPorIdEmpresaResponse>> ExecutarAsync(Guid id)
        {
            var result = mapper.Map<ObterPorIdEmpresaResponse>(await empresaRepository.ObterPorIdAsync(id));
            return result.ToResponseResult();
        }
    }

    public class ObterPorIdEmpresaResponse : BaseEmpresa { }
}
