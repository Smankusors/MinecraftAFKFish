# MinecraftAFKFish

Smankusors' automated Minecraft fishing that simulates mouse clicks based on audio cues

## Features
- Detects Minecraft fishing events by monitoring system audio levels
- Simulates right-clicks to automatically reel in and cast the fishing rod
- Configurable number of fishing cycles
- Console-based interface

## How It Works
- The app locates the running Minecraft process (`javaw`)
- Monitors the audio session for fishing sounds
- When a sound above a threshold is detected, it simulates a right-click to reel in and cast
- If no sound is detected for 30 seconds, it triggers a right-click to prevent inactivity

## Requirements
- Windows OS
- Minecraft running (Java Edition)
- [CoreAudioApi.dll](lib/CoreAudioApi.dll) (included)
- Alternatively, you can download the latest self-contained executable from the GitHub Actions artifact (no .NET SDK required).

## Usage
1. **Option A:** Build and run from source (requires .NET SDK)
   ```cmd
   dotnet build MinecraftAFKFish.csproj
   dotnet run --project MinecraftAFKFish.csproj
   ```
2. **Option B:** Download the latest release artifact from GitHub Actions (no .NET SDK required)
   - Go to the GitHub repository's Actions tab
   - Find the latest workflow run for your platform
   - Download the `MinecraftAFKFish` artifact from the run
   - Extract and run `MinecraftAFKFish.exe`
3. Run Minecraft (Java Edition)
4. Enter the number of fishing cycles when prompted
5. Place your mouse cursor over the fishing spot in Minecraft
6. The app will automate fishing until the specified cycles are complete

## Notes

**Tip:** For best results, set Minecraft's in-game Music volume to 0%. This reduces background noise and improves fishing sound detection.
