using Microsoft.Extensions.Configuration;

namespace BlazorOpenAiDemo.Services;

/// <summary>
/// Service providing access to feature flags for the demo application.
/// Allows enabling/disabling various AI features for demonstration purposes.
/// </summary>
public class FeatureFlags
{
    private readonly IConfiguration _configuration;
    
    public FeatureFlags(IConfiguration configuration)
    {
        _configuration = configuration;
        
        // Initialize from configuration
        EnableStandardContentGeneration = _configuration.GetValue<bool>("Features:EnableStandardContentGeneration", false);
        EnableStreamingContentGeneration = _configuration.GetValue<bool>("Features:EnableStreamingContentGeneration", false);
        EnableImageGeneration = _configuration.GetValue<bool>("Features:EnableImageGeneration", false);
        EnableSpeechGeneration = _configuration.GetValue<bool>("Features:EnableSpeechGeneration", false);
    }

    /// <summary>
    /// Gets or sets whether standard content generation is enabled.
    /// </summary>
    public bool EnableStandardContentGeneration { get; set; }

    /// <summary>
    /// Gets or sets whether streaming content generation is enabled.
    /// </summary>
    public bool EnableStreamingContentGeneration { get; set; }

    /// <summary>
    /// Gets or sets whether image generation is enabled.
    /// </summary>
    public bool EnableImageGeneration { get; set; }

    /// <summary>
    /// Gets or sets whether speech generation is enabled.
    /// </summary>
    public bool EnableSpeechGeneration { get; set; }
}