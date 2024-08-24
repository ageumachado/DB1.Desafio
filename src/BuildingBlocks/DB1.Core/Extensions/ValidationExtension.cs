using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB1.Core.Extensions
{
    public static class ValidationExtension
    {
        /// <summary>
        /// Validar CNPJ
        /// </summary>
        public static IRuleBuilderOptions<T, string?> IsCnpj<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            var retorno = ruleBuilder.Must(ValueObjects.Cnpj.IsValid).WithMessage("The Cnpj is invalid");
            return retorno;
        }

        /// <summary>
        /// Validar CPF
        /// </summary>
        public static IRuleBuilderOptions<T, string?> IsCpf<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            var retorno = ruleBuilder.Must(ValueObjects.Cpf.IsValid).WithMessage("The Cpf is invalid");
            return retorno;
        }

        public static IRuleBuilderOptions<T, int> IsInRange<T>(this IRuleBuilder<T, int> rule, int start, int end)
        {
            var comparisonFunc = BuildComparisonFunc(start, end);
            return rule.Must(x => comparisonFunc(x));
        }

        private static Func<int, bool> BuildComparisonFunc(int start, int end)
        {
            return x => x >= start && x <= end;
        }
    }
}
