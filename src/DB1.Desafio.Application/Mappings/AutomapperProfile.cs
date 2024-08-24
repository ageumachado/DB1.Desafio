using AutoMapper;
using DB1.Core.ValueObjects;
using DB1.Desafio.Application.Commands.Empresa.Criar;
using DB1.Desafio.Application.Commands.Empresa.Editar;
using DB1.Desafio.Application.Commands.Empresa.ObterPorId;
using DB1.Desafio.Application.Mappings.Converters;
using DB1.Desafio.Domain.Entities;
using DB1.Desafio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

            
            CreateMap<CriarEmpresaRequest, Empresa>();
            //CreateMap<Empresa, CriarEmpresaRequest>()
            //.ConstructUsing(p =>
            //    new Empresa(p.Nome, Cnpj.Parse(p.Cnpj!), p.DataFundacao, p.Ativo ? StatusEmpresa.Ativo : StatusEmpresa.Inativo));
            //.ForMember(p => p.Status, opt =>
            //opt.ConvertUsing(new BooleanToEnumConverter<StatusEmpresa>(StatusEmpresa.Ativo, StatusEmpresa.Inativo), src => src.Ativo));

            CreateMap<Empresa, CriarEmpresaRequest>()
                .ForMember(dest => dest.Ativo, opt =>
                    opt.ConvertUsing(new EnumToBooleanConverter<StatusEmpresa>(StatusEmpresa.Ativo), src => src.Status));

            CreateMap<Empresa, CriarEmpresaResponse>()
                .ForMember(dest => dest.Ativo, opt =>
                    opt.ConvertUsing(new EnumToBooleanConverter<StatusEmpresa>(StatusEmpresa.Ativo), src => src.Status));

            CreateMap<EditarEmpresaRequest, Empresa>();

            CreateMap<Empresa, EditarEmpresaResponse>()
                .ForMember(dest => dest.Ativo, opt =>
                    opt.ConvertUsing(new EnumToBooleanConverter<StatusEmpresa>(StatusEmpresa.Ativo), src => src.Status));

            CreateMap<Empresa, ObterPorIdEmpresaResponse>()
                .ForMember(dest => dest.Ativo, opt => opt.ConvertUsing(new EnumToBooleanConverter<StatusEmpresa>(StatusEmpresa.Ativo), src => src.Status));

            #endregion
        }
    }
}
