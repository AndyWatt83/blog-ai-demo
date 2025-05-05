#!/bin/bash

# Simple script to run the Blazor OpenAI Demo application

echo "Starting Blazor OpenAI Demo application..."
echo "==============================================="
echo "This application requires an OpenAI API key."
echo "If you haven't set one up yet, use the following command:"
echo "dotnet user-secrets set \"OpenAI:ApiKey\" \"your-api-key-here\" --project BlazorOpenAiDemo"
echo "==============================================="
echo ""

# Navigate to the project directory
cd "$(dirname "$0")"

# Run the application
dotnet run --project BlazorOpenAiDemo

# Note: The application will be available at https://localhost:5001 or http://localhost:5000