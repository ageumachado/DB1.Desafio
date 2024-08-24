namespace DB1.Core.Exceptions
{
    public class CpfCnpjInvalidoException : Exception
    {
        public CpfCnpjInvalidoException(string cpfCnpj) : base($"Cpf ou CNPJ '{cpfCnpj}' é inválido.")
        {
        }
    }
}
