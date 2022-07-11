namespace ChatworkApi.Tester.Domain.Repositories
{
    using Configurations;

    public interface IConfigurationRepository
    {
        void Save(string                   fileName
                , ApplicationConfiguration config);

        ApplicationConfiguration Load(string fileName);
    }
}