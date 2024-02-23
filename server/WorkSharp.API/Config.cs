public class Config : IConfig
{
    private readonly IConfiguration _Configuration;

    public Config(IConfiguration configuration)
    {
        _Configuration = configuration;
    }

    public string StoreFolderPath
    {
        get
        {
            var userPath = _Configuration["StoreSettings:FolderPath"];
            var tempPath = Path.GetTempPath();

            if (string.IsNullOrEmpty(userPath))
            {
                throw new ArgumentException("StoreSettings:FolderPath is not set");
            }

            var path = Path.Combine(tempPath, userPath);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Console.WriteLine($"StoreFolderPath: {path}");

            return path;
        }
    }
}

public interface IConfig
{
    string StoreFolderPath { get; }
}