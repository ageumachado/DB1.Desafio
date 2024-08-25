using AutoMapper;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;

namespace DB1.Desafio.Application.Commands.Empresa.Editar
{
    public class EditarEmpresaUseCase(
        IEmpresaRepository empresaRepository,
        IMapper mapper) : CommandUseCase, IEditarEmpresaUseCase
    {
        public async Task<ResponseResult<EditarEmpresaResponse>> ExecutarAsync(Guid id, EditarEmpresaRequest request)
        {
            if (request is null) return ResponseResultError("Requisição não recebida");

            request.Cnpj = request.Cnpj?.ApenasNumeros();
            request.DataFundacao = request.DataFundacao.Date;

            if (request.EhInvalido()) return request.ToResponseResultError();

            if (await empresaRepository
                .ExisteAsync(p => p.Id != id && p.Cnpj.Numero == request.Cnpj!))
                return ResponseResultError("Cnpj já informado anteriormente");

            var entidade = await empresaRepository.ObterPorIdAsync(id);

            if (entidade is null) return ResponseResultError("Registro não encontrado");

            mapper.Map(request, entidade);
            
            empresaRepository.Editar(entidade);

            var result = await PersistirDados(empresaRepository.UnitOfWork);
            var response = mapper.Map<EditarEmpresaResponse>(entidade);
            return result.ToResponseResult(response);
        }
    }
}
