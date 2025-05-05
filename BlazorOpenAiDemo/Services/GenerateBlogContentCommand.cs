using OpenAI;
using OpenAI.Chat;
using System.Text.Json;

namespace BlazorOpenAiDemo.Services;

/// <summary>
/// Command to generate blog content from a title using OpenAI's GPT model.
/// This is implemented as a standalone command for clear demonstration purposes.
/// </summary>
public class GenerateBlogContentCommand : IOpenAiCommand<string, string>
{
    private readonly ApiKeyProvider _apiKeyProvider;

    public GenerateBlogContentCommand(ApiKeyProvider apiKeyProvider)
    {
        _apiKeyProvider = apiKeyProvider;
    }

    /// <summary>
    /// Generates blog content based on the provided title.
    /// </summary>
    /// <param name="title">The blog title to generate content for</param>
    /// <returns>Generated blog content as an HTML string</returns>
    public async Task<string> ExecuteAsync(string title)
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

        try
        {
            // Send the request to OpenAI
            //var chatCompletion = client.CompleteChat(prompt);
            var chatCompletion = await client.CompleteChatAsync(prompt);

            return chatCompletion.Value.Content[0].Text;
        }
        catch (Exception ex)
        {
            // In a real application, you'd want more robust error handling
            return $"<p>Error generating content: {ex.Message}</p>";
        }
    }
}