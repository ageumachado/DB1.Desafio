using DB1.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB1.Core.ValueObjects
{
    /// <summary>
    /// Gerenciador de CPF ou CNPJ
    /// </summary>
    public record CpfCnpj
    {
        public string Numero { get; } = string.Empty;
        //private string _cpfCnpj;

        // Obrigatório para funcionar com EF
        protected CpfCnpj() { }

        private CpfCnpj(string cpfCnpj)
        {
            //_cpfCnpj = cpfCnpj;
            Numero = cpfCnpj;
        }

        /// <inheritdoc/>
        public static CpfCnpj Parse(string value)
        {
            if (TryParse(value, out var result))
                return result;

            throw new CpfCnpjInvalidoException(value);
        }

        public static bool TryParse(string value, out CpfCnpj cpfCnpj)
        {
            cpfCnpj = new CpfCnpj(value);
            return IsValid(value);
        }

        //public override string ToString() => _cpfCnpj;
        public override string ToString() => Numero;

        public static implicit operator CpfCnpj(string cpfCnpj) => Parse(cpfCnpj);

        public static implicit operator string(CpfCnpj cpfCnpj) => cpfCnpj.Numero;
        //public static implicit operator string(CpfCnpj cpfCnpj) => cpfCnpj._cpfCnpj;

        /// <summary>
        /// Valida elemento
        /// </summary>
        /// <param name="cpfCnpj">string</param>
        /// <returns>bool</returns>
        public static bool IsValid(string? cpfCnpj)
        {
            if (string.IsNullOrEmpty(cpfCnpj)) return true;

            var cpfValido = ValidarCPF(cpfCnpj);

            if (cpfValido)
                return cpfValido;

            return ValidarCnpj(cpfCnpj);
        }

        private static bool ValidarCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        private static bool ValidarCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
