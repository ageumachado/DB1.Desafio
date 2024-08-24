using AutoMapper;

namespace DB1.Desafio.Application.Mappings.Converters
{
    internal class EnumToBooleanConverter<TEnum> : IValueConverter<TEnum, bool>
    {
        private readonly TEnum enumDefault;

        public EnumToBooleanConverter(TEnum enumDefault)
        {
            this.enumDefault = enumDefault;
        }

        public bool Convert(TEnum sourceMember, ResolutionContext context) => Equals(sourceMember, enumDefault);
    }
}
