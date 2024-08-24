using AutoMapper;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Application.Commands.Empresa.Criar;
using DB1.Desafio.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB1.Desafio.Application.Commands.Empresa.Editar
{
    public interface IEditarEmpresaUseCase
    {
        Task<ResponseResult<EditarEmpresaResponse>> ExecutarAsync(EditarEmpresaRequest message); 
    }

    public class EditarEmpresaUseCase(
        IEmpresaRepository empresaRepository,
        IMapper mapper) : CommandUseCase, IEditarEmpresaUseCase
    {
        public async Task<ResponseResult<EditarEmpresaResponse>> ExecutarAsync(EditarEmpresaRequest message)
        {
            if (message is null) return ResponseResultError("Requisição não recebida");
            if (message.EhInvalido()) return message.ToResponseResultError();

            if (await empresaRepository
                .ExisteAsync(p => p.Id != message.Id && p.Cnpj.Numero == message.Cnpj!))
                return ResponseResultError("Cnpj já informado anteriormente");

            message.Cnpj = message.Cnpj?.ApenasNumeros();

            var entidade = mapper.Map<Domain.Entities.Empresa>(message);

            if (entidade.Invalid)
                return entidade.ValidationResult.ToResponseResult();

            empresaRepository.Editar(entidade);

            var result = await PersistirDados(empresaRepository.UnitOfWork);
            var response = mapper.Map<EditarEmpresaResponse>(entidade);
            return result.ToResponseResult(response);
        }
    }

    public class EditarEmpresaRequest : BaseEmpresa { }
    public class EditarEmpresaResponse : BaseEmpresa { }
         
}
