# Blazor File Upload with JWT Authentication

## Overview

This sample demonstrates how to secure file upload operations in a Blazor application using the Syncfusion [Blazor File Upload](https://www.syncfusion.com/blazor-components/blazor-file-upload) component and authorization headers sent to server-side endpoints. The application shows how authenticated upload requests can be handled by ASP.NET Core controller actions that validate incoming authorization information before saving or removing files. This approach helps developers protect upload APIs while maintaining a seamless client-side upload experience.

## Key Features

- Configures `UploaderAsyncSettings` with `SaveUrl="api/FileAction/Save"` and `RemoveUrl="api/FileAction/Remove"` to connect the uploader with ASP.NET Core backend endpoints.
- Handles the `FileSelected` event through the `onFileSelect` method to add a custom `Authorization` header to upload requests.
- Handles the `BeforeRemove` event through the `onRemove` method to add a custom `Authorization` header to file removal requests.
- Uses the `CurrentRequest` property to send custom request headers from the client to the server during upload and delete operations.
- Implements a `Save(IList<IFormFile> UploadFiles)` API action to process uploaded files and validate incoming authorization headers.
- Implements a `Remove(IList<IFormFile> UploadFiles)` API action to process authenticated file deletion requests.
- Reads the `Authorization` header from `Request.Headers` on the server before performing file operations.
- Returns HTTP 401 responses when authorization validation fails.
- Saves uploaded files to the application's web root directory using `FileStream` and server-side file handling APIs.
- Prevents duplicate file creation by checking whether the target file already exists before saving.
- Deletes existing files from the server when authenticated remove requests are received.
- Provides server-side error handling and custom response messages for upload, duplicate file, authorization, and file removal scenarios.

## Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/)
- [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet) compatible with the project's target framework

## How to Run the Project

**Visual Studio 2022**

1. Clone the repository.
2. Open `BlazorFileUploadJWT.sln`.
3. Restore NuGet packages.
4. Build the solution.
5. Set the appropriate startup project if prompted.
6. Run the application using `Ctrl+F5`.

**Visual Studio Code**

1. Clone the repository.
2. Open the repository folder in Visual Studio Code.
3. Navigate to the project directory.

```bash
cd BlazorFileUploadJWT
dotnet restore
dotnet run
```

4. Open the URL displayed in the application output window.

## Project Structure

- `BlazorFileUploadJWT/Client/Pages/Index.razor` - Hosts the Syncfusion `SfUploader` component, configures upload and remove endpoints, and attaches authorization headers through the `FileSelected` and `BeforeRemove` events.
- `BlazorFileUploadJWT/Server/Controllers/FileActionController.cs` - Provides the `Save` and `Remove` API endpoints that validate authorization headers, save uploaded files, prevent duplicate uploads, and remove files from the server.

## Support and Feedback

- For general product questions, visit the [Syncfusion Community Forum](https://www.syncfusion.com/forums) or [Syncfusion Support](https://www.syncfusion.com/support).
- To report an issue specific to this sample, open a GitHub issue in this repository.
- Documentation: https://blazor.syncfusion.com/documentation/file-upload/getting-started-with-web-app

## License

This is a Syncfusion sample project provided to demonstrate product usage. Review the [Syncfusion license terms](https://www.syncfusion.com/sales/pricing?category=ui-components) before using Syncfusion components in your own applications.