using Moq.AutoMock;

namespace DB1.Desafio.Tests.Fixtures
{
    public abstract class BaseFixture
    {
        protected Guid EmpresaFakeId = Guid.Parse("{55A2DC1A-CF16-434C-9F3C-520C3ADD5B80}");
        public AutoMocker? AutoMocker;
        
        /*
        protected List<Profile>? ProfileMockList = null;

        public IMapper ObterMapper()
        {
            // Auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile());
                if (ProfileMockList != null && ProfileMockList.Any())
                    cfg.AddProfiles(ProfileMockList);
            });
            return mockMapper.CreateMapper();
        }
        */

        public T ObterInstanciaAutoMocker<T>() where T : class
        {
            //AutoMocker ??= new AutoMocker();
            AutoMocker = new AutoMocker();
            return AutoMocker.CreateInstance<T>();
        }
    }
}
