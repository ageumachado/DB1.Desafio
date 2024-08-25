using AutoMapper;

namespace DB1.Desafio.Tests.Utils
{
    internal class MapperGenerater
    {
        public static List<Profile>? ProfileMockList = null;

        public static IMapper ObterMapper(Profile profile)
        {
            // Auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(profile);
                if (ProfileMockList != null && ProfileMockList.Any())
                    cfg.AddProfiles(ProfileMockList);
            });
            return mockMapper.CreateMapper();
        }
    }
}
