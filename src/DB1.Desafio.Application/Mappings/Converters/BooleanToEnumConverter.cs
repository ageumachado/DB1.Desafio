using AutoMapper;

namespace DB1.Desafio.Application.Mappings.Converters
{
    internal class BooleanToEnumConverter<TEnum> : IValueConverter<bool, TEnum>
    {
        private readonly TEnum enumActive;
        private readonly TEnum enumDeactive;

        public BooleanToEnumConverter(TEnum enumActive, TEnum enumDeactive)
        {
            this.enumActive = enumActive;
            this.enumDeactive = enumDeactive;
        }

        public TEnum Convert(bool sourceMember, ResolutionContext context) => sourceMember ? enumActive : enumDeactive;
    }
}
