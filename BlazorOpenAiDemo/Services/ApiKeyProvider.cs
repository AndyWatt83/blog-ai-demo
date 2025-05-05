namespace BlazorOpenAiDemo.Services;

/// <summary>
/// Service to provide the OpenAI API key from configuration.
/// This centralizes API key management for all OpenAI commands.
/// </summary>
public class ApiKeyProvider
{
    private readonly IConfiguration _configuration;
    
    public ApiKeyProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    /// <summary>
    /// Gets the OpenAI API key from user secrets or other configuration sources.
    /// </summary>
    /// <returns>The API key as a string</returns>
    public string GetApiKey()
    {
        // Get key from User Secrets (configured with: dotnet user-secrets set "OpenAI:ApiKey" "your-key-here")
        string? apiKey = _configuration["OpenAI:ApiKey"];
        
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException(
                "OpenAI API key not found. Please set it using dotnet user-secrets set \"OpenAI:ApiKey\" \"your-key-here\"");
        }
        
        return apiKey;
    }
}