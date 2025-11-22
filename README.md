# Knowledge Sharing Recorder

A Windows desktop application (.NET 8, WinForms) for recording and tracking knowledge-sharing posts with URLs and timestamps.

## Features

- **Record Sharing URLs**: Enter and save knowledge-sharing post URLs (e.g., Teams chat links) with automatic timestamp recording
- **CSV Storage**: All records are saved to `KnowledgeSharingRecords.csv` in the same folder as the executable
- **Date Range Statistics**: Calculate how many sharings were made within a selected date range
- **Open CSV**: Open the CSV file directly in Excel or your default CSV application

## Requirements

- .NET 8 SDK or later
- Windows operating system
- Visual Studio 2022 (recommended) or Visual Studio Code with C# extension

## Project Structure

```
KnowledgeSharingRecorder/
├── KnowledgeSharingRecorder.csproj  # Project configuration file
├── Program.cs                        # Application entry point
├── MainForm.cs                       # Main form logic and event handlers
├── MainForm.Designer.cs              # UI layout and controls
└── README.md                         # This file
```

## Building and Running the Application

### Option 1: Using Visual Studio

1. **Open the Project**
   - Launch Visual Studio 2022
   - Click "Open a project or solution"
   - Navigate to the project folder and select `KnowledgeSharingRecorder.csproj`
   - Click "Open"

2. **Build the Project**
   - Press `F6` or go to **Build** → **Build Solution**
   - Alternatively, use `Ctrl+Shift+B`

3. **Run the Application**
   - Press `F5` or click the **Start** button
   - The application window will open

4. **Debug vs Release**
   - **Debug mode** (default): Includes debugging symbols, larger file size
   - **Release mode**: Optimized build, smaller file size
   - Switch using the dropdown at the top of Visual Studio (Debug/Release)

### Option 2: Using Command Line (dotnet CLI)

1. **Navigate to Project Directory**
   ```powershell
   cd C:\code\Sharing_Records
   ```

2. **Restore Dependencies** (if needed)
   ```powershell
   dotnet restore
   ```

3. **Build the Project**
   ```powershell
   dotnet build
   ```
   For Release build:
   ```powershell
   dotnet build -c Release
   ```

4. **Run the Application**
   ```powershell
   dotnet run
   ```
   Or run the compiled executable:
   ```powershell
   dotnet run --project KnowledgeSharingRecorder.csproj
   ```

## Publishing as a Single .exe File

To create a single, self-contained executable file that can be run on any Windows machine without requiring .NET to be installed:

### Option 1: Using Visual Studio

1. **Right-click** on the project in Solution Explorer
2. Select **Publish...**
3. In the Publish dialog:
   - **Target**: Folder
   - **Specific target**: Folder
   - Click **Next**
4. Choose a publish location (e.g., `bin\Release\net8.0-windows\publish`)
5. Click **Finish**
6. In the Publish profile settings:
   - **Deployment mode**: Self-contained
   - **Target runtime**: win-x64 (or win-x86, win-arm64 as needed)
   - **File publish options**: 
     - ✅ **Produce single file** (check this)
   - Click **Save**
7. Click **Publish**
8. The single .exe file will be located in the publish folder you specified

### Option 2: Using Command Line (dotnet CLI)

Run the following command from the project directory:

```powershell
dotnet publish -c Release -r win-x64 /p:PublishSingleFile=true /p:SelfContained=true
```

**Command Parameters Explained:**
- `-c Release`: Build in Release configuration (optimized)
- `-r win-x64`: Target Windows x64 platform
  - Alternative runtimes: `win-x86` (32-bit), `win-arm64` (ARM64)
- `/p:PublishSingleFile=true`: Create a single .exe file
- `/p:SelfContained=true`: Include .NET runtime (no need to install .NET separately)

**Output Location:**
The published single .exe file will be located at:
```
bin\Release\net8.0-windows\win-x64\publish\KnowledgeSharingRecorder.exe
```

### Additional Publish Options

**To reduce file size** (optional, but may increase startup time):
```powershell
dotnet publish -c Release -r win-x64 /p:PublishSingleFile=true /p:SelfContained=true /p:PublishTrimmed=true
```

**To enable ReadyToRun compilation** (faster startup, larger file):
```powershell
dotnet publish -c Release -r win-x64 /p:PublishSingleFile=true /p:SelfContained=true /p:PublishReadyToRun=true
```

## Project Configuration

The `.csproj` file already includes the following settings for single-file publishing:

```xml
<PublishSingleFile>true</PublishSingleFile>
<SelfContained>true</SelfContained>
<RuntimeIdentifier>win-x64</RuntimeIdentifier>
<PublishReadyToRun>true</PublishReadyToRun>
```

These settings ensure that when you publish, a single .exe file is created.

## Usage

1. **Save a Record**:
   - Enter a URL in the "Sharing URL" text box
   - Click "Save Record"
   - The record is saved with the current timestamp

2. **Open CSV File**:
   - Click "Open CSV" to open the CSV file in Excel or your default CSV application

3. **Calculate Sharing Count**:
   - Select a start date and end date using the date pickers
   - Click "Calculate Sharing Count"
   - The result will show the number of records within the selected date range

## CSV File Format

The CSV file (`KnowledgeSharingRecords.csv`) uses the following format:

```csv
timestamp,url
2024-01-15 14:30:00,https://teams.microsoft.com/l/message/...
2024-01-16 09:15:00,https://teams.microsoft.com/l/message/...
```

- **Location**: Same folder as the executable
- **Timestamp format**: `yyyy-MM-dd HH:mm:ss` (local time)
- **No duplicate checking**: The same URL can be saved multiple times

## Notes

- The CSV file is created automatically when you save your first record
- The application window is resizable
- All UI text and messages are in English
- Date range calculation includes the entire end date (up to 23:59:59.9999999)

## Troubleshooting

**Issue**: "CSV file not found" when trying to open or calculate
- **Solution**: Save at least one record first to create the CSV file

**Issue**: Build errors related to .NET 8
- **Solution**: Ensure you have .NET 8 SDK installed. Check with `dotnet --version`

**Issue**: Single-file publish creates multiple files
- **Solution**: Verify that `PublishSingleFile=true` is set in the .csproj or use the `/p:PublishSingleFile=true` parameter in the publish command

## License

This project is provided as-is for personal or internal use.

