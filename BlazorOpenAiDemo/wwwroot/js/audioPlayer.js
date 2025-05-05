// Audio player functions for the blog TTS functionality

// Create or update the audio player with new audio data
function createOrUpdateAudioPlayer(elementId, base64AudioData) {
    // Convert base64 to Blob
    const byteCharacters = atob(base64AudioData);
    const byteArrays = [];
    
    for (let offset = 0; offset < byteCharacters.length; offset += 512) {
        const slice = byteCharacters.slice(offset, offset + 512);
        
        const byteNumbers = new Array(slice.length);
        for (let i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }
        
        const byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
    }
    
    const blob = new Blob(byteArrays, { type: 'audio/mp3' });
    const audioUrl = URL.createObjectURL(blob);
    
    // Find or create audio element
    let audioElement = document.getElementById(elementId);
    
    if (!audioElement) {
        // Create new audio element if it doesn't exist
        audioElement = document.createElement('audio');
        audioElement.id = elementId;
        audioElement.controls = true;
        audioElement.className = 'w-100 mt-2 mb-3';
        
        // Find the target container
        const targetContainer = document.getElementById('audioPlayerContainer');
        if (targetContainer) {
            targetContainer.appendChild(audioElement);
            targetContainer.style.display = 'block';
        }
    }
    
    // Update audio source
    audioElement.src = audioUrl;
    
    // Clean up previous URL if exists
    if (audioElement.dataset.objectUrl) {
        URL.revokeObjectURL(audioElement.dataset.objectUrl);
    }
    
    // Store the new URL for future cleanup
    audioElement.dataset.objectUrl = audioUrl;
    
    return true;
}

// Hide the audio player
function hideAudioPlayer(elementId) {
    const container = document.getElementById('audioPlayerContainer');
    if (container) {
        container.style.display = 'none';
    }
    
    const audioElement = document.getElementById(elementId);
    if (audioElement) {
        // Clean up URL
        if (audioElement.dataset.objectUrl) {
            URL.revokeObjectURL(audioElement.dataset.objectUrl);
        }
        
        // Reset audio
        audioElement.pause();
        audioElement.src = '';
    }
    
    return true;
}