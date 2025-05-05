using OpenAI;
using OpenAI.Images;

namespace BlazorOpenAiDemo.Services;

/// <summary>
/// Command to generate a header image for a blog using OpenAI's DALL-E model.
/// This enables AI-generated images that match the blog content.
/// </summary>
public class GenerateImageCommand : IOpenAiCommand<GenerateImageRequest, string>
{
    private readonly ApiKeyProvider _apiKeyProvider;

    public GenerateImageCommand(ApiKeyProvider apiKeyProvider)
    {
        _apiKeyProvider = apiKeyProvider;
    }

    /// <summary>
    /// Generates a header image based on the provided blog title and content.
    /// </summary>
    /// <param name="request">The request containing blog title and content</param>
    /// <returns>Generated image as a URL</returns>
    public async Task<string> ExecuteAsync(GenerateImageRequest request)
    {
        // Get the API key
        string apiKey = _apiKeyProvider.GetApiKey();

        // Create the OpenAI image client with DALL-E 3 model
        var client = new ImageClient("dall-e-3", apiKey);

        // Create a prompt for the image generation
        // Use both title and content to generate a relevant image
        string prompt = GeneratePrompt(request.Title, request.Content);

        try
        {
            // Configure image generation options
            var options = new ImageGenerationOptions
            {
                Quality = GeneratedImageQuality.Standard,
                Size = GeneratedImageSize.W1024xH1024,
                Style = GeneratedImageStyle.Natural,
                ResponseFormat = GeneratedImageFormat.Uri // We want a URL
            };

            // Call DALL-E to generate an image
            var result = await client.GenerateImageAsync(prompt, options);

            // Access the image URL from the result
            // The SDK returns a ClientResult<GeneratedImage>
            return result.Value.ImageUri.AbsoluteUri;
        }
        catch (Exception ex)
        {
            // In a real application, you'd want more robust error handling
            Console.WriteLine($"Image generation error: {ex.Message}");
            return string.Empty;
        }
    }

    /// <summary>
    /// Generates an optimized prompt for DALL-E based on the blog title and content.
    /// </summary>
    private string GeneratePrompt(string title, string content)
    {
        // Create a concise context from content (first ~100 chars)
        string contentSummary = content.Length > 100
            ? content.Substring(0, 100)
            : content;

        // Clean up HTML tags for better prompt
        contentSummary = contentSummary.Replace("<p>", "")
            .Replace("</p>", "")
            .Replace("<h2>", "")
            .Replace("</h2>", "");

        return $"Create a professional header image for a blog post titled '{title}'. " +
               $"The blog discusses: {contentSummary}... " +
               "The image should be high quality, suitable for a professional blog, " +
               "with a clean composition and subtle colors. No text should be included in the image.";
    }
}

/// <summary>
/// Request model for generating images, containing both title and content
/// </summary>
public class GenerateImageRequest
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}