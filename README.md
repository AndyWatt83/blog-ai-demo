# AI-Enhanced Blazor Blog Demo

A demo application showcasing OpenAI API integration with a Blazor Server blogging platform. The application demonstrates progressive enhancement with multiple AI capabilities using a clean, instructive architecture.

## Features

The application implements four key AI features using OpenAI APIs:

1. **Content Generation** - Generate blog content from a title using GPT-4o
2. **Streaming Content Generation** - Generate content with real-time token-by-token delivery
3. **Image Generation** - Create AI-generated header images using DALL-E 3
4. **Text-to-Speech** - Convert blog content to spoken audio using OpenAI's TTS API

Each feature is implemented in its own dedicated file for clarity, following a consistent command pattern that makes the code easy to understand and present.

## Getting Started

### Prerequisites

- .NET 9.0 SDK or later
- An OpenAI API key

### Obtaining an OpenAI API Key

1. Visit [OpenAI API Keys](https://platform.openai.com/settings) (you'll need to create an account or sign in)
2. Navigate to the API keys section
3. Click "Create new secret key"
4. Give your key a name (e.g., "Blazor Blog Demo")
5. Copy the API key (you won't be able to view it again after closing the dialog)

### Running the Application

1. Clone the repository
   ```
   git clone https://github.com/yourusername/blog-ai-demo.git
   cd blog-ai-demo
   ```

2. Configure your OpenAI API key using .NET User Secrets
   ```
   dotnet user-secrets init --project BlazorOpenAiDemo
   dotnet user-secrets set "OpenAI:ApiKey" "your-api-key-here" --project BlazorOpenAiDemo
   ```

3. Run the application
   ```
   dotnet run --project BlazorOpenAiDemo
   ```

4. Navigate to https://localhost:5001 in your browser

### Demo Mode

The application includes a special presentation mode with Next/Previous buttons in the bottom-right corner. This allows you to progressively reveal features during a demonstration:

- **Step 1**: Basic Blog Editor - Just the editor functionality
- **Step 2**: Content Generation - Adds the "Generate Content" button
- **Step 3**: Streaming Content - Adds the "Stream Content" button
- **Step 4**: Image Generation - Adds the "Generate Image" button
- **Step 5**: Text-to-Speech - Adds the "Generate Audio" button

This makes it easy to demonstrate each capability separately without overwhelming your audience.

## Project Structure

### Core Components
- `BlazorOpenAiDemo/Components/Pages/BlogEditor.razor` - Main blog editor interface
- `BlazorOpenAiDemo/Models/BlogPost.cs` - Blog post data model

### OpenAI Integration
- `BlazorOpenAiDemo/Services/IOpenAiCommand.cs` - Command interface for all AI features
- `BlazorOpenAiDemo/Services/ApiKeyProvider.cs` - Secure API key management

### AI Feature Implementations
- `BlazorOpenAiDemo/Services/GenerateBlogContentCommand.cs` - Content generation
- `BlazorOpenAiDemo/Services/StreamBlogContentCommand.cs` - Streaming content generation
- `BlazorOpenAiDemo/Services/GenerateImageCommand.cs` - Image generation
- `BlazorOpenAiDemo/Services/GenerateSpeechCommand.cs` - Text-to-speech functionality

### Demo Mode
- `BlazorOpenAiDemo/Services/FeatureFlags.cs` - Feature flag management
- `BlazorOpenAiDemo/Services/DemoController.cs` - Demo progression control

## Technologies Used

- **.NET 9.0** - Latest .NET framework
- **Blazor Server** - For interactive web UI
- **OpenAI SDK** - Official .NET client library for OpenAI
- **OpenAI API Models**:
  - GPT-4o for content generation
  - DALL-E 3 for image generation
  - TTS-1 for text-to-speech
- **Bootstrap 5** - For responsive UI components
- **.NET User Secrets** - For secure API key storage
- **JavaScript Interop** - For audio playback functionality

## Additional Resources

- For a deeper understanding of the implementation, see [CLAUDE.md](CLAUDE.md)
- The project was built with assistance from Claude Code

## License

This project is licensed under the MIT License - see the LICENSE file for details.