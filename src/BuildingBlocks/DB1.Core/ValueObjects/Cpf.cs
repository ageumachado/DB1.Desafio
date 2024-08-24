using DB1.Core.Exceptions;

namespace DB1.Core.ValueObjects
{
    /// <summary>
    /// Gerenciador de CPF ou CNPJ
    /// </summary>
    public record Cpf
    {
        public string Numero { get; } = string.Empty;
        //private string _cpfCnpj;

        // Obrigatório para funcionar com EF
        protected Cpf() { }

        private Cpf(string cpfCnpj)
        {
            //_cpfCnpj = cpfCnpj;
            Numero = cpfCnpj;
        }

        /// <inheritdoc/>
        public static Cpf Parse(string value)
        {
            if (TryParse(value, out var result))
                return result;

            throw new CpfCnpjInvalidoException(value);
        }

        public static bool TryParse(string value, out Cpf cpfCnpj)
        {
            cpfCnpj = new Cpf(value);
            return IsValid(value);
        }

        //public override string ToString() => _cpfCnpj;
        public override string ToString() => Numero;

        public static implicit operator Cpf(string cpfCnpj) => Parse(cpfCnpj);

        public static implicit operator string(Cpf cpfCnpj) => cpfCnpj.Numero;
        //public static implicit operator string(CpfCnpj cpfCnpj) => cpfCnpj._cpfCnpj;

        /// <summary>
        /// Valida elemento
        /// </summary>
        /// <param name="cpfCnpj">string</param>
        /// <returns>bool</returns>
        public static bool IsValid(string? cpfCnpj)
        {
            if (string.IsNullOrEmpty(cpfCnpj)) return true;

            return ValidarCPF(cpfCnpj);
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
