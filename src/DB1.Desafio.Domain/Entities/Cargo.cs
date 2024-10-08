﻿using DB1.Core.DomainObjects;
using DB1.Desafio.Domain.Enums;
using DB1.Desafio.Domain.Validators;

namespace DB1.Desafio.Domain.Entities
{
    public class Cargo : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public Status Status { get; private set; }

        // EF
        protected Cargo() { }

        public Cargo(string nome)
        {
            Nome = nome;
            Ativar();

            Validate(this, new CargoValidator());
        }

        public ICollection<FuncionarioCargo>? FuncionarioCargos { get; set; }

        public void Ativar() => Status = Status.Ativo;
        public void Inativar() => Status = Status.Inativo;
        public void Remover() => Status = Status.Removido;
    }
}
