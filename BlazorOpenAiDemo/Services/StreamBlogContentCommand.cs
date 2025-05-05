using OpenAI;
using OpenAI.Chat;

namespace BlazorOpenAiDemo.Services;

/// <summary>
/// Command to generate blog content with real-time streaming from OpenAI.
/// This implementation demonstrates token-by-token streaming for an enhanced user experience.
/// </summary>
public class StreamBlogContentCommand
{
    private readonly ApiKeyProvider _apiKeyProvider;

    public StreamBlogContentCommand(ApiKeyProvider apiKeyProvider)
    {
        _apiKeyProvider = apiKeyProvider;
    }

    /// <summary>
    /// Generates blog content based on the provided title with real-time streaming.
    /// Returns an async stream where each element is a new token from the API.
    /// </summary>
    /// <param name="title">The blog title to generate content for</param>
    /// <returns>An async stream of text tokens</returns>
    public async IAsyncEnumerable<string> ExecuteAsync(string title)
    {
        // Get the API key
        string apiKey = _apiKeyProvider.GetApiKey();

        // Create the OpenAI chat client with GPT-4o model
        var client = new ChatClient(
            model: "gpt-4o",
            apiKey: apiKey
        );

        // Build the prompt with specific instructions for blog generation
        string prompt = $@"Write a professional blog post with the title: ""{title}""

Please follow these guidelines:
1. Write approximately 300-500 words
2. Include an introduction, 2-3 main points with subheadings, and a conclusion
3. Use a professional but engaging tone
4. Format the content with HTML tags like <h2>, <p>, <ul>, etc.
5. Do not include any meta text or notes - only the actual blog content
6. Start directly with the content, do not include the title (it will be displayed separately)";

        // Standard foreach is needed for the CollectionResult type
        var completionUpdates = client.CompleteChatStreamingAsync(prompt);

        await foreach (var chunk in completionUpdates)
        {
                if (chunk.ContentUpdate.Count > 0)
                {
                   yield return chunk.ContentUpdate[0].Text;
                }
        }
    }
}