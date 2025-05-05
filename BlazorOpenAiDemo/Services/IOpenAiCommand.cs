namespace BlazorOpenAiDemo.Services;

/// <summary>
/// Simple interface for OpenAI command pattern.
/// Each AI capability (content generation, image creation, speech synthesis)
/// will implement this interface in its own file.
/// </summary>
/// <typeparam name="TRequest">The input type for the command (e.g., blog title)</typeparam>
/// <typeparam name="TResponse">The output type for the command (e.g., generated content)</typeparam>
public interface IOpenAiCommand<TRequest, TResponse>
{
    /// <summary>
    /// Execute the OpenAI command asynchronously.
    /// </summary>
    /// <param name="request">The input for the command</param>
    /// <returns>The result of the command execution</returns>
    Task<TResponse> ExecuteAsync(TRequest request);
}