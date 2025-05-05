# AI-Enhanced Blazor Blog Demo

## Project Overview
This is a demo application showcasing AI integration with a Blazor blogging platform. The application will demonstrate progressive enhancement with OpenAI APIs:

1. AI-generated content from a user-provided title
2. AI-generated header images based on blog content
3. Text-to-speech functionality to read the blog content aloud

## Current Status: All Phases Complete!

We have successfully implemented all planned features:
- Content generation (standard and streaming)
- Image generation
- Text-to-speech functionality

### Base Application
- Single-page blog editor interface with a clean, focused design
- Input fields for blog title and content with real-time preview
- Image upload capability with proper error handling and loading indicators
- Live preview panel showing exactly how the blog will appear
- Optimized for Blazor Server with responsive UI updates

### Standard AI Content Generation
- Implemented command pattern for OpenAI integration
- Created `IOpenAiCommand<TRequest, TResponse>` interface
- Implemented `ApiKeyProvider` for secure API key management
- Added `GenerateBlogContentCommand` that calls OpenAI's GPT-4o model
- Integrated "Generate Content" button in the UI

### Streaming AI Content Generation
- Created `StreamBlogContentCommand` with IAsyncEnumerable return type
- Implemented OpenAI streaming API using `CompleteChatStreamingAsync`
- Used direct async enumeration with `await foreach` for proper async behavior
- Added "Stream Content" button in the UI
- Created real-time animated typing effect as tokens arrive in real-time
- Optimized the UX with proper loading states and error handling
- Implemented concise, efficient streaming code with minimal error handling

### AI Image Generation
- Created `GenerateImageCommand` implementing the command interface
- Integrated DALL-E 3 model with proper configuration options
- Added intelligent prompt generation based on blog title and content
- Used `GeneratedImageSize.W1024xH1024` and `GeneratedImageStyle.Natural` settings
- Implemented proper error handling with fallbacks
- Integrated "Generate Image" button that appears next to image upload
- Added proper loading states and UI feedback during generation
- Implemented accessing image URL via `result.Value.ImageUri.AbsoluteUri`

### Text-to-Speech Audio Generation
- Created `GenerateSpeechCommand` implementing the command interface
- Integrated with OpenAI's Audio TTS API endpoint
- Implemented HTML tag stripping for better speech quality
- Used JavaScript interop to create and control the HTML5 audio element
- Added "Generate Audio" button below the content textarea
- Implemented proper loading states and UI feedback during generation
- Added auto-hiding audio player that appears under blog title
- Built intelligent state management that resets audio when content changes

This implementation demonstrates a clean architecture approach with:
- Separation of concerns (each AI capability in its own file)
- User Secrets integration for secure API key storage
- Proper error handling and loading state indicators

The application is now in a fully functional state where users can:

1. Enter a blog title manually or generate content from it with AI
2. Upload a header image from their device or generate one with AI
3. Write or edit blog content (with basic HTML support)
4. See a live preview of their blog post as they work
5. Generate audio versions of their content for accessibility

## Completed: Phase 5 (Text-to-Speech)

We have successfully implemented text-to-speech functionality to read the blog content aloud:

### Text-to-Speech Implementation
- Created `GenerateSpeechCommand.cs` implementing the command interface
- Integrated with OpenAI's Audio API TTS endpoint
- Configured voice options (using "alloy" by default)
- Return audio as byte array for browser playback
- Implemented JavaScript interop for audio element control

### UI Integration
- Positioned audio player directly under the title in the preview section
  - Audio player is hidden until audio is successfully generated
  - Clean, minimal design that fits with the blog preview aesthetic
- Added "Generate Audio" button below the content text area
  - Button is disabled when content is empty or during other operations
  - Includes loading state indicator during generation
- Implemented appropriate state management
  - Audio is reset when content changes substantially
  - Clear error messaging for any TTS failures

This implementation follows the same command pattern architecture as the other features, with:
1. A dedicated command class for OpenAI TTS API interaction
2. Clear separation of API integration from UI concerns
3. User-friendly UI integration with appropriate feedback

## OpenAI Configuration

The application uses User Secrets for secure API key storage:

```bash
# Initialize user secrets (only needed once)
dotnet user-secrets init --project BlazorOpenAiDemo

# Set your OpenAI API key
dotnet user-secrets set "OpenAI:ApiKey" "your-api-key-here" --project BlazorOpenAiDemo
```

## Commands
- Build: `dotnet build`
- Run: `dotnet run --project BlazorOpenAiDemo`
- Test: `dotnet test`

## Architecture

The application follows a lightweight command pattern to make each AI capability clear and distinct:

### Core Components

1. **IOpenAiCommand<TRequest, TResponse>** - Simple interface for all OpenAI commands to implement
   ```csharp
   public interface IOpenAiCommand<TRequest, TResponse>
   {
       Task<TResponse> ExecuteAsync(TRequest request);
   }
   ```

2. **ApiKeyProvider** - Service to securely retrieve OpenAI API keys from User Secrets
   ```csharp
   public class ApiKeyProvider
   {
       private readonly IConfiguration _configuration;
       
       public ApiKeyProvider(IConfiguration configuration)
       {
           _configuration = configuration;
       }
       
       public string GetApiKey()
       {
           string apiKey = _configuration["OpenAI:ApiKey"];
           if (string.IsNullOrEmpty(apiKey))
           {
               throw new InvalidOperationException("OpenAI API key not found...");
           }
           return apiKey;
       }
   }
   ```

3. **Command Implementations**:
   - `GenerateBlogContentCommand.cs` - Standard content generation
   - `StreamBlogContentCommand.cs` - Streaming content generation
   - `GenerateImageCommand.cs` - Image generation
   - `GenerateSpeechCommand.cs` - Text-to-speech

4. **Demo Mode**:
   - The application includes a progressive demo mode with Next/Previous buttons
   - Features are revealed sequentially for presentation purposes
   - Located in the bottom-right corner of the interface
   - Steps through each AI capability one at a time

## For Claude Code Users

This project was built with assistance from Claude Code. If you're using Claude Code to explore or modify this project, here are some tips:

1. **Key Files to Understand**:
   - `/BlazorOpenAiDemo/Services/IOpenAiCommand.cs` - Core interface
   - `/BlazorOpenAiDemo/Services/ApiKeyProvider.cs` - API key management
   - `/BlazorOpenAiDemo/Services/GenerateBlogContentCommand.cs` - Content generation
   - `/BlazorOpenAiDemo/Services/StreamBlogContentCommand.cs` - Streaming content
   - `/BlazorOpenAiDemo/Services/GenerateImageCommand.cs` - Image generation
   - `/BlazorOpenAiDemo/Services/GenerateSpeechCommand.cs` - Text-to-speech
   - `/BlazorOpenAiDemo/Components/Pages/BlogEditor.razor` - Main UI component

2. **Implementing New Features**:
   - Follow the command pattern with a new class implementing `IOpenAiCommand`
   - Register your new service in `Program.cs`
   - Update the UI in `BlogEditor.razor` to use your new feature

3. **Common Tasks**:
   - Modifying prompts: Edit the prompt strings in respective command classes
   - Adding UI elements: Update the BlogEditor.razor file
   - Changing model parameters: Modify the options in command classes

This project structure is designed to be clear and educational, making it easy to understand how each OpenAI feature is implemented.