using System.Text.Json;

namespace DessertRate.Services;

#nullable enable

public interface IConfigService
{
    Task<List<string>> GetImageLinksAsync();
}

public class ConfigService : IConfigService
{
    private readonly HttpClient _httpClient;
    private List<string>? _cachedImageLinks;
    private const string ConfigUrl = "https://raw.githubusercontent.com/wannabegood/DessertRate/version-2025/wwwroot/config.json";
    private const string LocalConfigPath = "config.json";

    public ConfigService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<string>> GetImageLinksAsync()
    {
        // Return cached links if available
        if (_cachedImageLinks != null)
        {
            return _cachedImageLinks;
        }

        try
        {
            // Try to fetch from GitHub first
            var response = await _httpClient.GetAsync(ConfigUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var config = JsonSerializer.Deserialize<ConfigData>(jsonContent);
                _cachedImageLinks = config?.ImageLinks ?? [];
                return _cachedImageLinks;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to fetch config from GitHub: {ex.Message}. Falling back to local config.");
        }

        // Fallback to local config.json
        try
        {
            var jsonContent = await _httpClient.GetStringAsync(LocalConfigPath);
            var config = JsonSerializer.Deserialize<ConfigData>(jsonContent);
            _cachedImageLinks = config?.ImageLinks ?? [];
            return _cachedImageLinks;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to fetch local config: {ex.Message}");
            return [];
        }
    }

    private class ConfigData
    {
        public List<string> ImageLinks { get; set; } = [];
    }
}
