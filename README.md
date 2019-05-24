# Microsoft Graph C# Console Connect Sample

## Table of contents

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Register the application](#Register-the-application)
* [How to Run This sample](#how-to-run-this-sample)
* [Cloning or downloading this repository](#cloning-or-downloading-repo)
* [Configure your Azure AD tenant](#configuring-Azure-AD-tenant )
* [Configure the sample to use your Azure AD tenant](#configuring-sample-to-use-Azure-AD-tenant)
* [Build and run the sample](#build-and-run-the-sample)
* [Questions and comments](#questions-and-comments)
* [Contributing](#contributing)
* [Additional resources](#additional-resources)

<a name= "introduction"></a>
## Introduction

This sample shows how to connect a Windows console application to a Microsoft work or school (Azure Active Directory) or personal (Microsoft) account using the Microsoft Graph API. It uses the Microsoft Graph API to retrieve a user's profile picture, upload the picture to OneDrive, create a sharing link, and send an email that contains the photo as an attachment and the sharing link in its text. It uses the Microsoft Graph .NET Client Library to work with data returned by Microsoft Graph. The sample uses the Azure AD v2.0 endpoint, which enables users to sign in with either their personal or work or school Microsoft accounts.


The sample uses the Microsoft Authentication Library (MSAL) for authentication.

<a name= "prerequisites"></a>
## Prerequisites

This sample requires the following:
- [Visual Studio](htt ps://www.visualstudio.com/en-us/downloads) with C# version 7 and above. 
-  Either a [Microsoft](www.outlook.com) or [Office 365 for business account](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account).
- An Azure Active Directory (Azure AD) tenant. For more information on how to get an Azure AD tenant, please see [How to get an Azure AD tenant](https://azure.microsoft.com/en-us/documentation/articles/active-directory-howto-tenant/).

<a name="how-to-run-this-sample"></a>
## How To Run This Sample

<a name="cloning-or-downloading-repo"></a>
### Step 1:  Clone or download this repository

From your shell or command line:

`git clone https://github.com/microsoftgraph/console-csharp-connect-sample`

<a name="configuring-Azure-AD-tenant"></a>
### Step 2:  Configure your Azure AD tenant 

1. Sign in to the [Azure portal](https://portal.azure.com) using either a work or school account or a personal Microsoft account.
2. If your account gives you access to more than one tenant, select your account in the top right corner, and set your portal session to the desired Azure AD tenant
   (using **Switch Directory**).
3. In the left-hand navigation pane, select the **Azure Active Directory** service, and then select **App registrations**.

<a name="Register-the-application"></a>
#### Register the client app

![](https://github.com/nicolesigei/console-csharp-connect-sample/blob/master/readme-images/appregistrations.png)

1. In  **App registrations** page, select **Register an Application**.
2. When the **Register an application page** appears, enter your application's registration information:
   - In the **Name** section, enter a meaningful application name that will be displayed to users of the app, for example `Console App for Microsoft Graph`
   - In the **Supported account types** section, select **Accounts in any organizational directory and personal Microsoft accounts (e.g. Skype, Xbox, Outlook.com)**.
   - Select **Register** to create the application.
3. On the app **Overview** page, find the **Application (client) ID** value and record it for later. You'll need it to configure the Visual Studio configuration file for this project.
![](https://github.com/nicolesigei/console-csharp-connect-sample/blob/master/readme-images/client.png)

4. In the list of pages for the app, select **Authentication**
 Use *urn:ietf:wg:oauth:2.0:oob* in the Redirect URI text box and select the Type as Public Client (mobile and desktop)
 
 ![](https://github.com/nicolesigei/console-csharp-connect-sample/blob/master/readme-images/redirect.png)
 
<a name="configuring-sample-to-use-Azure-AD-tenant"></a>
### Step 3:  Configure the sample to use your Azure AD tenant

In the steps below, "ClientId" is the same as "Application ID" or "AppId".

Open the solution in Visual Studio to configure the projects.

#### Configure the client project

1. In the *console-csharp-connect-sample* folder, rename the `appsettings.json.example` file to `appsettings.json`
1. Open and edit the `appsettings.json` file to make the following change
    1. Find the line where `ClientId` is set as `YOUR_CLIENT_ID_HERE` and replace the existing value with the application (client) ID of the `Console App for Microsoft Graph` application copied from the Azure portal.
 ![](https://github.com/nicolesigei/console-csharp-connect-sample/blob/master/readme-images/ID.png)

<a name="build-and-run-the-sample"></a>
### Step 4: Build and run the sample 

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
Copyright (c) 2019 Microsoft. All rights reserved.
