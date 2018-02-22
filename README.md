# Microsoft Graph C# Console Connect Sample

## Table of contents

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Register the application](#Register-the-application )
* [Build and run the sample](#build-and-run-the-sample)
* [Questions and comments](#questions-and-comments)
* [Contributing](#contributing)
* [Additional resources](#additional-resources)

## Introduction

This sample shows how to connect a Windows console application to a Microsoft work or school (Azure Active Directory) or personal (Microsoft) account using the Microsoft Graph API. It uses the Microsoft Graph API to retrieve a user's profile picture, upload the picture to OneDrive, create a sharing link, and send an email that contains the photo as an attachment and the sharing link in its text. It uses the Microsoft Graph .NET Client Library to work with data returned by Microsoft Graph. The sample uses the Azure AD v2.0 endpoint, which enables users to sign in with either their personal or work or school Microsoft accounts.

The sample uses the Microsoft Authentication Library (MSAL) for authentication.

## Prerequisites

This sample requires the following:

- [Visual Studio](https://www.visualstudio.com/en-us/downloads) 

-  Either a [Microsoft](www.outlook.com) or [Office 365 for business account](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account).

<a name="Register-the-application"></a>
## Register the application 

1. Sign in to the [Application Registration Portal](https://apps.dev.microsoft.com/) using your Microsoft account.

2. Select **Add an app**, and enter a friendly name for the application (such as **Console App for Microsoft Graph**). Click **Create**.

3. On the application registration page, select **Add Platform**. Select the **Native App** tile and save your change.

4. Open the solution and then the Constants.cs file in Visual Studio. 

5. Make the **Application Id** value for this app the value of the **ClientId** string.

## Build and run the sample 

1. Open the sample solution in Visual Studio.
2. Press F5 to build and run the sample. This will restore the NuGet package dependencies and open the console application.
3. When prompted, authenticate with your Microsoft account and consent to the permissions that the application needs.
4. Follow the prompts to send a message from your account to yourself or someone else.
   
## Questions and comments

We'd love to get your feedback about the Microsoft Graph API Console App. You can send your questions and suggestions in the [Issues](https://github.com/microsoftgraph/console-csharp-connect-sample/issues) section of this repository.

Questions about Microsoft Graph development in general should be posted to [Stack Overflow](https://stackoverflow.com/questions/tagged/microsoftgraph). Make sure that your questions or comments are tagged with [microsoftgraph].

## Contributing ##

If you'd like to contribute to this sample, see [CONTRIBUTING.MD](/CONTRIBUTING.md).

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
  
## Additional resources

- [Other Microsoft Graph Connect samples](https://github.com/MicrosoftGraph?utf8=%E2%9C%93&query=-Connect)
- [Microsoft Graph](https://developer.microsoft.com/en-us/graph)

## Copyright
Copyright (c) 2017 Microsoft. All rights reserved.
