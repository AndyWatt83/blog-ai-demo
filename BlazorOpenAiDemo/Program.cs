using BlazorOpenAiDemo.Components;
using BlazorOpenAiDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register feature flags service
builder.Services.AddSingleton<FeatureFlags>();
builder.Services.AddSingleton<DemoController>();

// Register OpenAI services
builder.Services.AddSingleton<ApiKeyProvider>();
builder.Services.AddTransient<GenerateBlogContentCommand>();
builder.Services.AddTransient<StreamBlogContentCommand>();
builder.Services.AddTransient<GenerateImageCommand>();
builder.Services.AddTransient<GenerateSpeechCommand>();

// Configure increased upload size limits for Blazor Server
builder.Services.Configure<Microsoft.AspNetCore.SignalR.HubOptions>(options =>
{
    options.MaximumReceiveMessageSize = 5 * 1024 * 1024; // 5MB
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
    options.KeepAliveInterval = TimeSpan.FromSeconds(15);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
