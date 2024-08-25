using AutoMapper;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;

namespace DB1.Desafio.Application.Commands.Empresa.Criar
{
    public interface ICriarEmpresaUseCase
    {
        Task<ResponseResult<CriarEmpresaResponse>> ExecutarAsync(CriarEmpresaRequest request);
    }

    public class CriarEmpresaUseCase(
        IEmpresaRepository empresaRepository,
        IMapper mapper) : CommandUseCase, ICriarEmpresaUseCase
    {
        public async Task<ResponseResult<CriarEmpresaResponse>> ExecutarAsync(CriarEmpresaRequest request)
        {
            if (request is null) return ResponseResultError("Requisição não recebida");

            request.Cnpj = request.Cnpj?.ApenasNumeros();
            request.DataFundacao = request.DataFundacao.Date;

            if (request.EhInvalido()) return request.ToResponseResultError();

            if (await empresaRepository
                .ExisteAsync(p => p.Cnpj.Numero == request.Cnpj!))
                return ResponseResultError("Cnpj já informado anteriormente");

            var entidade = mapper.Map<Domain.Entities.Empresa>(request);

            if (entidade.Invalid)
                return entidade.ValidationResult.ToResponseResult();

            empresaRepository.Adicionar(entidade);

            var result = await PersistirDados(empresaRepository.UnitOfWork);
            var response = mapper.Map<CriarEmpresaResponse>(entidade);
            return result.ToResponseResult(response);
        }
    }

    public class CriarEmpresaRequest : BaseEmpresa { }
    public class CriarEmpresaResponse : BaseEmpresa { }
}
