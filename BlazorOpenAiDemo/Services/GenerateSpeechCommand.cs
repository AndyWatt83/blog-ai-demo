using OpenAI;
using OpenAI.Audio;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlazorOpenAiDemo.Services;

/// <summary>
/// Command to generate text-to-speech audio from blog content using OpenAI's TTS API.
/// This enables an audio version of the blog content for accessibility and convenience.
/// </summary>
public class GenerateSpeechCommand : IOpenAiCommand<GenerateSpeechRequest, byte[]>
{
    private readonly ApiKeyProvider _apiKeyProvider;

    public GenerateSpeechCommand(ApiKeyProvider apiKeyProvider)
    {
        _apiKeyProvider = apiKeyProvider;
    }

    /// <summary>
    /// Generates speech audio from the provided blog content.
    /// </summary>
    /// <param name="request">The request containing content to convert to speech</param>
    /// <returns>The generated audio as a byte array for browser playback</returns>
    public async Task<byte[]> ExecuteAsync(GenerateSpeechRequest request)
    {
        // Get the API key
        string apiKey = _apiKeyProvider.GetApiKey();

        // Create the OpenAI audio client with TTS-1 model and the API key
        var client = new AudioClient(model: "tts-1", apiKey: apiKey);

        // Clean the content (remove HTML tags for better speech)
        string cleanContent = StripHtmlTags(request.Content);

        try
        {
            // Configure TTS options
            var options = new SpeechGenerationOptions
            {
                // Default to MP3 format for browser compatibility
                ResponseFormat = GeneratedSpeechFormat.Mp3,
                SpeedRatio = 1.0f, // Normal speed
            };

            // Call the TTS API to generate speech audio
            var result = await client.GenerateSpeechAsync(cleanContent, GeneratedSpeechVoice.Alloy, options);

            // Return the audio as a byte array
            return result.Value.ToArray();
        }
        catch (Exception ex)
        {
            // Log the error
            Console.WriteLine($"Speech generation error: {ex.Message}");

            // Rethrow to handle in UI layer
            throw;
        }
    }

    /// <summary>
    /// Removes HTML tags from content for better speech synthesis.
    /// </summary>
    private string StripHtmlTags(string content)
    {
        // Simple regex to remove HTML tags
        string plainText = Regex.Replace(content, "<.*?>", string.Empty);

        // Replace common HTML entities
        plainText = plainText.Replace("&nbsp;", " ")
                             .Replace("&amp;", "&")
                             .Replace("&lt;", "<")
                             .Replace("&gt;", ">")
                             .Replace("&quot;", "\"");

        return plainText;
    }
}

/// <summary>
/// Request model for generating speech from text
/// </summary>
public class GenerateSpeechRequest
{
    /// <summary>
    /// The blog content to convert to speech
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// The voice to use for speech generation
    /// Default is 'alloy' which has a neutral tone
    /// Other options: echo, fable, onyx, nova, shimmer
    /// </summary>
    public string Voice { get; set; } = "alloy";
}