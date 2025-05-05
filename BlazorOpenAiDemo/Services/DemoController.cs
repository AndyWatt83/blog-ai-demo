using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorOpenAiDemo.Services;

/// <summary>
/// Simple service to control demo progression by enabling features in sequence.
/// </summary>
public class DemoController
{
    private readonly FeatureFlags _featureFlags;
    private int _currentStep = 0;

    public DemoController(FeatureFlags featureFlags)
    {
        _featureFlags = featureFlags;
        UpdateFeatureState();
    }

    /// <summary>
    /// Move to the next feature in the demo
    /// </summary>
    public void Next()
    {
        if (_currentStep < 4)
        {
            _currentStep++;
            UpdateFeatureState();
        }
    }

    /// <summary>
    /// Move to the previous feature in the demo
    /// </summary>
    public void Previous()
    {
        if (_currentStep > 0)
        {
            _currentStep--;
            UpdateFeatureState();
        }
    }

    /// <summary>
    /// Get the label for the current step
    /// </summary>
    public string GetCurrentStepLabel()
    {
        return _currentStep switch
        {
            0 => "Basic Blog Editor",
            1 => "Content Generation",
            2 => "Streaming Content",
            3 => "Image Generation",
            4 => "Text-to-Speech",
            _ => "Unknown Step"
        };
    }

    /// <summary>
    /// Check if there's a next step available
    /// </summary>
    public bool HasNext => _currentStep < 4;

    /// <summary>
    /// Check if there's a previous step available
    /// </summary>
    public bool HasPrevious => _currentStep > 0;

    // Update feature state based on current step
    private void UpdateFeatureState()
    {
        // Directly update properties
        _featureFlags.EnableStandardContentGeneration = _currentStep >= 1;
        _featureFlags.EnableStreamingContentGeneration = _currentStep >= 2;
        _featureFlags.EnableImageGeneration = _currentStep >= 3;
        _featureFlags.EnableSpeechGeneration = _currentStep >= 4;
    }
}