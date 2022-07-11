namespace ChatworkApi.Tester.Domain.Repositories
{
    using System.IO;
    using Configurations;
    using Newtonsoft.Json;

    /// <summary>
    /// アプリケーションの構成情報のリポジトリ インターフェースです。
    /// </summary>
    public sealed class ConfigurationRepository : IConfigurationRepository
    {
        public void Save(string                   fileName
                       , ApplicationConfiguration config)
        {
            var json = JsonConvert.SerializeObject(config);

            var directory = Directory.GetParent(fileName).FullName;
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            File.WriteAllText(fileName, json);
        }

        public ApplicationConfiguration Load(string fileName)
        {
            var json = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<ApplicationConfiguration>(json);
        }
    }
}