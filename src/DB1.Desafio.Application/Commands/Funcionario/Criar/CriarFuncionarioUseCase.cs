using AutoMapper;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Repositories;

namespace DB1.Desafio.Application.Commands.Funcionario.Criar
{
    public class CriarFuncionarioUseCase(
        IFuncionarioRepository funcionarioRepository,
        ICargoRepository cargoRepository,
        IMapper mapper) : CommandUseCase, ICriarFuncionarioUseCase
    {
        public async Task<ResponseResult<CriarFuncionarioResponse>> ExecutarAsync(CriarFuncionarioRequest request)
        {
            if (request is null) return ResponseResultError("Requisição não recebida");

            request.Cpf = request.Cpf?.ApenasNumeros();
            request.DataContratacao = request.DataContratacao.Date;

            if (request.EhInvalido()) return request.ToResponseResultError();

            if (await funcionarioRepository
                .ExisteAsync(p => p.Cpf.Numero == request.Cpf!))
                return ResponseResultError("Cpf já informado anteriormente");

            var funcionario = mapper.Map<Domain.Entities.Funcionario>(request);

            if (funcionario.Invalid)
                return funcionario.ValidationResult.ToResponseResult();

            funcionario.AdicionarEmpresa(request.EmpresaId);

            if (funcionario.EmpresaId.HasValue && request.CargoId.HasValue)
            {
                var cargo = await cargoRepository.ObterPorIdAsync(request.CargoId.Value);
                if (cargo is not null)
                {
                    funcionarioRepository.VincularCargo(funcionario, cargo, DateTime.UtcNow);
                }
            }

            funcionarioRepository.Adicionar(funcionario);

            var result = await PersistirDados(funcionarioRepository.UnitOfWork);
            var response = mapper.Map<CriarFuncionarioResponse>(funcionario);
            response.CargoId = request.CargoId;
            return result.ToResponseResult(response);
        }
    }
}
