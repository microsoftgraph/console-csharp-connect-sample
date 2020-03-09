---
page_type: sample
products:
- office-outlook
- ms-graph
languages:
- csharp
description: "此示例演示如何使用 Microsoft Graph API 将 Windows 控制台应用程序连接到 Microsoft 工作或学校帐户 (Azure Active Directory) 或个人帐户 (Microsoft) 以发送电子邮件。"
extensions:
  contentType: samples
  technologies:
  - Microsoft Graph 
  - Microsoft identity platform
  services:
  - Microsoft identity platform
  - Outlook
  createdDate: 12/11/2017 1:55:47 PM
---
# Microsoft Graph C# 控制台连接示例

## 目录

* [简介](#introduction)
* [先决条件](#prerequisites)
* [克隆或下载此存储库](#cloning-or-downloading-repo)
* [配置 Azure AD 租户](#configuring-Azure-AD-tenant )
* [将示例配置为使用 Azure AD 租户](#configuring-sample-to-use-Azure-AD-tenant)
* [生成并运行示例](#build-and-run-sample)
* [问题和意见](#questions-and-comments)
* [参与](#contributing)
* [其他资源](#additional-resources)

## 简介

此示例演示如何使用 Microsoft Graph API 将 Windows 控制台应用程序连接到 Microsoft 工作或学校帐户 (Azure Active Directory) 或个人帐户 (Microsoft)。它使用 Microsoft Graph API 检索用户的个人资料图片，将该图片上传到 OneDrive，创建一个共享链接，然后发送一封以该照片为附件且在正文中包含该共享链接的电子邮件。它使用 Microsoft Graph .NET 客户端库来处理 Microsoft Graph 返回的数据。本示例使用 Azure AD 2.0 版终结点，支持用户通过其个人或工作/学校 Microsoft 帐户进行登录。

本示例使用 Microsoft 身份验证库 (MSAL) 进行身份验证。

## 先决条件

此示例要求如下：

- [Visual Studio](https://www.visualstudio.com/en-us/downloads)（支持 C# 7 和更高版本）。 
-  [Microsoft](www.outlook.com) 或 [Office 365 商业版帐户](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account)。
- Azure Active Directory (Azure AD) 租户。有关如何获取 Azure AD 租户的详细信息，请参阅[如何获取 Azure AD 租户](https://azure.microsoft.com/en-us/documentation/articles/active-directory-howto-tenant/)。

## 如何运行此示例

<a name="cloning-or-downloading-repo"></a>
### 步骤 1：克隆或下载此存储库

在 shell 或命令行中键入：

`git clone https://github.com/microsoftgraph/console-csharp-connect-sample`

<a name="configuring-Azure-AD-tenant"></a>
### 步骤 2：配置 Azure AD 租户 

1. 使用工作或学校帐户或个人 Microsoft 帐户登录到 [Azure 门户](https://portal.azure.com)。
2. 如果你的帐户有权访问多个租户，请在右上角选择该帐户，并将门户会话设置为所需的 Azure AD 租户（使用“**切换目录**”）。
3. 在左侧导航窗格中选择“**Azure Active Directory**”服务，然后选择“**应用注册(预览版)**”。

#### 注册客户端应用
![](https://github.com/nicolesigei/console-csharp-connect-sample/blob/master/readme-images/registrations.png)
1.在“**应用注册**”页面中，选择“**注册应用程序**”。
1.在“**应用注册(预览版)**”页面中，选择“**注册应用程序**”。
2.屏幕上出现“**注册应用程序**”页面后，输入应用程序的注册信息：
- 在“**名称**”部分中，输入要向应用程序用户显示的有意义的应用程序名称，例如`适用于 Microsoft Graph 的控制台应用`
- 在“**支持的帐户类型**”部分中，选择“**任意组织目录中的帐户和个人 Microsoft 帐户(例如 Skype、Xbox、Outlook.com)**”。
- 选择“**注册**”以创建应用程序。
3.在应用的“**概述**”页面上，查找“**应用程序(客户端) ID**”值，记录下来以供稍后使用。为此项目配置 Visual Studio 配置文件时将会用到该值。
4.在应用的页面列表中，选择“**身份验证**”

    - 在**重定向 URI**文本框中使用*urn:ietf:wg:oauth:2.0:oob*，并在**类型**中选择“公共客户端(移动和桌面)”
![](https://github.com/nicolesigei/console-csharp-connect-sample/blob/master/readme-images/redirect.png) 
- 在“*适用于公共客户端(移动、桌面)的建议重定向 URI*”中，选中第二个框，以便应用可以使用应用程序中使用的 MSAL 库。（此框应包含选项“*urn:ietf:wg:oauth:2.0:oob*”）。
5.在应用的页面列表中，选择“**API 权限**”
- 单击“**添加权限**”按钮
- 然后确保选中“**Microsoft API**”选项卡。
- 在“*常用的 Microsoft API*”部分中，单击“**Microsoft Graph**”。
- 在“**委派的权限**”部分中，确保选中正确的权限：“**User.Read**”、“**Mail.Send**”和“**Files.ReadWrite**”。如有必要，请使用搜索框。
- 选择“**添加权限**”按钮。

<a name="configuring-sample-to-use-Azure-AD-tenant"></a>
### 步骤 3：将示例配置为使用 Azure AD 租户

在下面的步骤中，*客户端 ID* 与*应用程序 ID* 或*应用 ID* 相同。

在 Visual Studio 中打开解决方案以配置项目。

#### 配置客户端项目

1. 在 *console-csharp-connect-sample* 文件夹中，将 `appsettings.json.example` 文件重命名为 `appsettings.json`
1. 打开并编辑 `appsettings.json` 文件以进行以下更改
    1. 找到将 `ClientId` 设置为 `YOUR_CLIENT_ID_HERE` 的行，然后将现有值替换为从 Azure 门户复制的`适用于 Microsoft Graph 的控制台应用`这一应用程序的“应用程序(客户端) ID”。

<a name="build-and-run-sample"></a>
### 步骤 4：生成并运行示例 

1. 在 Visual Studio 中打开示例解决方案。
2. 按 F5 生成并运行此示例。这将还原 NuGet 包依赖项，并打开控制台应用程序。
3. 出现提示时，使用 Microsoft 帐户进行身份验证并同意应用程序所需的权限。
4. 按照提示从你的帐户向你自己或其他人发送一封邮件。
   
## 问题和意见

我们乐意倾听你有关 Microsoft Graph API 控制台应用的反馈。你可以在该存储库中的[问题](https://github.com/microsoftgraph/console-csharp-connect-sample/issues)部分将问题和建议发送给我们。

与 Microsoft Graph 开发相关的一般问题应发布到 [Stack Overflow](https://stackoverflow.com/questions/tagged/microsoftgraph)。请确保你的问题或意见标记有 \[microsoftgraph]。

## 参与 ##

如果想要参与本示例，请参阅 [CONTRIBUTING.MD](/CONTRIBUTING.md)。

此项目已采用 [Microsoft 开放源代码行为准则](https://opensource.microsoft.com/codeofconduct/)。有关详细信息，请参阅[行为准则常见问题解答](https://opensource.microsoft.com/codeofconduct/faq/)。如有其他任何问题或意见，也可联系 [opencode@microsoft.com](mailto:opencode@microsoft.com)。
  
## 其他资源

- [其他 Microsoft Graph 连接示例](https://github.com/MicrosoftGraph?utf8=%E2%9C%93&query=-Connect)
- [Microsoft Graph](https://developer.microsoft.com/en-us/graph)

## 版权信息
版权所有 (c) 2019 Microsoft。保留所有权利。
