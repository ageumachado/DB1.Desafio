using AutoMapper;
using DB1.Core.ValueObjects;
using DB1.Desafio.Application.Commands.Cargo.Criar;
using DB1.Desafio.Application.Commands.Cargo.Editar;
using DB1.Desafio.Application.Commands.Cargo.ObterPorId;
using DB1.Desafio.Application.Commands.Cargo.ObterTodos;
using DB1.Desafio.Application.Commands.Empresa.Criar;
using DB1.Desafio.Application.Commands.Empresa.Editar;
using DB1.Desafio.Application.Commands.Empresa.ObterComFiltro;
using DB1.Desafio.Application.Commands.Empresa.ObterPorId;
using DB1.Desafio.Application.Commands.Empresa.ObterTodos;
using DB1.Desafio.Application.Commands.Funcionario.Criar;
using DB1.Desafio.Application.Commands.Funcionario.Editar;
using DB1.Desafio.Application.Commands.Funcionario.ObterComFiltro;
using DB1.Desafio.Application.Commands.Funcionario.ObterPorId;
using DB1.Desafio.Application.Commands.Funcionario.ObterTodos;
using DB1.Desafio.Application.Mappings.Converters;
using DB1.Desafio.Domain.Entities;
using DB1.Desafio.Domain.Enums;

namespace DB1.Desafio.Application.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            Mappings();
        }

        private void Mappings()
        {
            #region Empresa
            CreateMap<CriarEmpresaRequest, Empresa>()
                .ConstructUsing(p =>
                    new Empresa(Guid.Empty, p.Nome, Cnpj.Parse(p.Cnpj!), p.DataFundacao, p.Status));
            CreateMap<Empresa, CriarEmpresaRequest>();
            CreateMap<Empresa, CriarEmpresaResponse>();
                
            CreateMap<EditarEmpresaRequest, Empresa>()
                .ConstructUsing(p =>
                    new Empresa(p.Id, p.Nome, Cnpj.Parse(p.Cnpj!), p.DataFundacao, p.Status));
            CreateMap<Empresa, EditarEmpresaResponse>();

            CreateMap<Empresa, ObterPorIdEmpresaResponse>();

            CreateMap<Empresa, ObterTodosEmpresaResponse>();

            CreateMap<Empresa, ObterComFiltroEmpresaResponse>();
            #endregion

            #region Cargo
            CreateMap<CriarCargoRequest, Cargo>();
            CreateMap<Cargo, CriarCargoResponse>();

            CreateMap<EditarCargoRequest, Cargo>();
            CreateMap<Cargo, EditarCargoResponse>();

            CreateMap<Cargo, ObterPorIdCargoResponse>();

            CreateMap<Cargo, ObterComFiltroCargoResponse>();
            #endregion

            #region Funcionario
            CreateMap<CriarFuncionarioRequest, Funcionario>()
                .ConstructUsing(p =>
                    new Funcionario(Guid.Empty, p.Nome, Cpf.Parse(p.Cpf!), p.DataContratacao))
                .ForMember(p => p.EmpresaId, opt => opt.Ignore());
            CreateMap<Funcionario, CriarFuncionarioResponse>();
            CreateMap<Funcionario, CriarFuncionarioRequest>();

            CreateMap<EditarFuncionarioRequest, Funcionario>()
                .ConstructUsing(p =>
                    new Funcionario(p.Id, p.Nome, Cpf.Parse(p.Cpf!), p.DataContratacao))
                .ForMember(p => p.EmpresaId, opt => opt.Ignore());
            CreateMap<Funcionario, EditarFuncionarioResponse>();

            CreateMap<Funcionario, ObterPorIdFuncionarioResponse>();

            CreateMap<Funcionario, ObterTodosFuncionarioResponse>()
                .ForMember(p => p.CargoNome, static opt => 
                    opt.MapFrom(static x => x.FuncionarioCargos.OrderBy(o => o.DataVinculo).Last().Cargo.Nome));
            CreateMap<Funcionario, ObterComFiltroFuncionarioResponse>()
                .ForMember(p => p.CargoNome, static opt =>
                    opt.MapFrom(static x => x.FuncionarioCargos.OrderBy(o => o.DataVinculo).Last().Cargo.Nome));
            #endregion
        }
    }
}
