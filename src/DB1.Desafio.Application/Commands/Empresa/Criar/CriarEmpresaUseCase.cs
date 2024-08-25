using AutoMapper;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;

namespace DB1.Desafio.Application.Commands.Empresa.Criar
{
    public interface ICriarEmpresaUseCase
    {
        Task<ResponseResult<CriarEmpresaResponse>> ExecutarAsync(CriarEmpresaRequest message);
    }

    public class CriarEmpresaUseCase(
        IEmpresaRepository empresaRepository,
        IMapper mapper) : CommandUseCase, ICriarEmpresaUseCase
    {
        public async Task<ResponseResult<CriarEmpresaResponse>> ExecutarAsync(CriarEmpresaRequest message)
        {
            if (message is null) return ResponseResultError("Requisição não recebida");

            message.Cnpj = message.Cnpj?.ApenasNumeros();
            message.DataFundacao = message.DataFundacao.Date;

            if (message.EhInvalido()) return message.ToResponseResultError();

            if (await empresaRepository
                .ExisteAsync(p => p.Cnpj.Numero == message.Cnpj!))
                return ResponseResultError("Cnpj já informado anteriormente");

            var entidade = mapper.Map<Domain.Entities.Empresa>(message);

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
