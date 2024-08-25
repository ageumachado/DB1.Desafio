using AutoMapper;
using DB1.Core.Communication;
using DB1.Core.DomainObjects;
using DB1.Core.Extensions;
using DB1.Desafio.Domain.Entities;
using DB1.Desafio.Domain.Repositories;

namespace DB1.Desafio.Application.Commands.Funcionario.Editar
{
    public interface IEditarFuncionarioUseCase
    {
        Task<ResponseResult<EditarFuncionarioResponse>> ExecutarAsync(Guid id, EditarFuncionarioRequest request);
    }

    public class EditarFuncionarioUseCase(
        IFuncionarioRepository funcionarioRepository,
        ICargoRepository cargoRepository,
        IMapper mapper) : CommandUseCase, IEditarFuncionarioUseCase
    {
        public async Task<ResponseResult<EditarFuncionarioResponse>> ExecutarAsync(Guid id, EditarFuncionarioRequest request)
        {
            if (request is null) return ResponseResultError("Requisição não recebida");

            request.Cpf = request.Cpf?.ApenasNumeros();
            request.DataContratacao = request.DataContratacao.Date;

            if (request.EhInvalido()) return request.ToResponseResultError();

            if (await funcionarioRepository
                .ExisteAsync(p => p.Id != id && p.Cpf.Numero == request.Cpf!))
                return ResponseResultError("Cpf já informado anteriormente");

            var funcionario = await funcionarioRepository.ObterPorIdAsync(id);

            if (funcionario is null) return ResponseResultError("Registro não encontrado");

            mapper.Map(request, funcionario);

            if (funcionario.EmpresaId is null)
                funcionario.AdicionarEmpresa(request.EmpresaId);

            if (funcionario.EmpresaId.HasValue && request.CargoId.HasValue)
            {
                var jaExisteCargoVinculado = await funcionarioRepository
                    .ExisteCargoAsync(funcionario, request.CargoId.Value);
                if (!jaExisteCargoVinculado)
                {
                    var cargo = await cargoRepository.ObterPorIdAsync(request.CargoId.Value);
                    if (cargo is not null)
                    {
                        funcionarioRepository.VincularCargo(funcionario, cargo, DateTime.UtcNow);
                    }
                }
            }

            funcionarioRepository.Editar(funcionario);

            var result = await PersistirDados(funcionarioRepository.UnitOfWork);
            var response = mapper.Map<EditarFuncionarioResponse>(funcionario);
            response.CargoId = request.CargoId;
            return result.ToResponseResult(response);
        }
    }

    public class EditarFuncionarioRequest : BaseFuncionario { }
    public class EditarFuncionarioResponse : BaseFuncionario { }
}
