---
page_type: sample
products:
- office-outlook
- ms-graph
languages:
- csharp
description: "このサンプルでは、Microsoft Graph API を使用して Windows コンソール アプリケーションを Microsoft の職場または学校 (Azure Active Directory) アカウントまたは個人 (Microsoft) アカウントに接続してメールを送信する方法を示します。"
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
# Microsoft Graph C# コンソール Connect のサンプル

## 目次

* [はじめに](#introduction)
* [前提条件](#prerequisites)
* [このリポジトリの複製またはダウンロード](#cloning-or-downloading-repo)
* [Azure AD テナントを構成する](#configuring-Azure-AD-tenant )
* [Azure AD テナントを使用するようにサンプルを構成する](#configuring-sample-to-use-Azure-AD-tenant)
* [サンプルのビルドと実行](#build-and-run-sample)
* [質問とコメント](#questions-and-comments)
* [投稿](#contributing)
* [その他のリソース](#additional-resources)

## 概要

このサンプルは、Microsoft Graph API を使用して、Microsoft の職場または学校 (Azure Active Directory) アカウント、あるいは個人用 (Microsoft) アカウントに Windows コンソール アプリケーションを接続する方法を示します。Microsoft Graph API を使用してユーザーのプロファイル画像を取得し、その画像を OneDrive にアップロードし、共有リンクを作成し、画像を添付ファイルおよびテキスト内の共有リンクとして含むメールを送信します。Microsoft Graph が返すデータを操作するために、Microsoft Graph .NET クライアント ライブラリを使用します。このサンプルは Azure AD v2.0 エンドポイントを使用します。このエンドポイントにより、ユーザーは個人用か、職場または学校の Microsoft アカウントでサインインできます。

サンプルでは Microsoft 認証ライブラリ (MSAL) を使用して認証を行います。

## 前提条件

このサンプルを実行するには次のものが必要です。

- C# バージョン 7 以降の [Visual Studio](https://www.visualstudio.com/en-us/downloads)。 
-  [Microsoft](www.outlook.com) または [Office 365 for Business アカウント](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account)のいずれか。
- Azure Active Directory (Azure AD) テナント。Azure AD テナントを取得する方法の詳細については、「[Azure AD テナントを取得する方法](https://azure.microsoft.com/en-us/documentation/articles/active-directory-howto-tenant/)」を参照してください。

## このサンプルの実行方法

<a name="cloning-or-downloading-repo"></a>
### 手順 1: このリポジトリのクローンを作成するか、ダウンロードします

シェルまたはコマンド ラインから:

`git clone https://github.com/microsoftgraph/console-csharp-connect-sample`

<a name="configuring-Azure-AD-tenant"></a>
### 手順 2:Azure AD テナントを構成する 

1. 職場または学校のアカウントか、個人の Microsoft アカウントを使用して、[Azure portal](https://portal.azure.com)にサインインします。
2. ご利用のアカウントで複数のテナントにアクセスできる場合は、右上隅でアカウントを選択し、ポータルのセッションを目的の Azure AD テナントに設定します (**Switch Directory** を使用)。
3. 左側のナビゲーション ウィンドウで、[**Azure Active Directory**] サービスを選択し、[**アプリの登録 (プレビュー)**] を選択します。

#### クライアント アプリの登録
![](https://github.com/nicolesigei/console-csharp-connect-sample/blob/master/readme-images/registrations.png)
1.[**アプリの登録**] ページで、[**アプリケーションを登録する**] を選択します。
1.[**アプリの登録 (プレビュー)**] ページで、[**アプリケーションを登録する**] を選択します。
2.[**アプリケーションの登録] ページ**が表示されたら、アプリケーションの登録情報を入力します。
- [**名前**] セクションで、アプリのユーザーに表示される意味のあるアプリケーション名を入力します。たとえば、`Microsoft Graph 用のコンソール アプリ`などです
- [**サポートされているアカウントの種類**] セクションで、**任意の組織のディレクトリ内のアカウントと個人用の Microsoft アカウント (例: Skype、 Xbox、Outlook.com)** を選択します。
- [**登録**] を選択して、アプリケーションを作成します。
3.アプリの [**概要**] ページで、[**Application (client) ID**] (アプリケーション (クライアント) ID) の値を確認し、後で使用するために記録します。この情報は、このプロジェクトで Visual Studio 構成ファイルを設定するのに必要になります。
4.アプリのページの一覧から [**認証**] を選択します

    - **リダイレクト URI** テキスト ボックスで *urn:ietf:wg:oauth:2.0:oob* を使用し、**種類** をパブリック クライアント (モバイルおよびデスクトップ) として選択します
![](https://github.com/nicolesigei/console-csharp-connect-sample/blob/master/readme-images/redirect.png) 
- [*パブリック クライアント (モバイル、デスクトップ) に推奨されるリダイレクト URI*] で、アプリケーションで使用されている MSAL ライブラリをアプリが利用できるように、2 番目のボックスをオンにします。(ボックスには、オプション *urn:ietf:wg:oauth:2.0:oob* を含める必要があります)。
5.アプリのページの一覧で、[**API アクセス許可**] を選択し、[**アクセス許可を追加する**] ボタンをクリックしてから、[**Microsoft API**] タブが選択されていることを確認します。
- [*よく使用される Microsoft API*] セクションで、
- [**Microsoft Graph**] をクリックします。
- [**委任されたアクセス許可**] セクションで、適切なアクセス許可がチェックされていることを確認します。**User.Read**、**Mail.Send** および **Files.ReadWrite**。必要に応じて検索ボックスを使用します。
- [**アクセス許可を追加する**] ボタンを選択します。

<a name="configuring-sample-to-use-Azure-AD-tenant"></a>
### 手順 3:Azure AD テナントを使用するようにサンプルを構成する

次の手順では、*クライアント ID* は*アプリケーション ID* または*アプリ ID* と同じです。

Visual Studio でソリューションを開き、プロジェクトを構成します。

#### クライアント プロジェクトを構成する

1. *console-csharp-connect-sample* フォルダーで、`appsettings.json.example` ファイルの名前を `appsettings.json` に変更します
1. `appsettings.json` ファイルを開いて編集し、次の変更を加えます
    1. `ClientId` が `YOUR_CLIENT_ID_HERE` として設定されている行を見つけ、既存の値を Azure ポータルからコピーされた `Microsoft Graph アプリケーションのコンソール アプリ`のアプリケーション (クライアント) ID に置き換えます。

<a name="build-and-run-sample"></a>
### 手順 4: サンプルのビルドと実行 

1. サンプル ソリューションを Visual Studio で開きます。
2. F5 キーを押して、サンプルをビルドして実行します。これにより、NuGet パッケージの依存関係が復元され、コンソール アプリケーションが開きます。
3. プロンプトが表示されたら、Microsoft アカウントで認証し、アプリケーションが必要とするアクセス許可に同意します。
4. プロンプトに従って、アカウントから自分自身または他のユーザーにメッセージを送信します。
   
## 質問とコメント

Microsoft Graph API コンソール アプリに関するフィードバックをぜひお寄せください。質問や提案は、このリポジトリの「[問題](https://github.com/microsoftgraph/console-csharp-connect-sample/issues)」セクションで送信できます。

Microsoft Graph 開発全般の質問については、「[Stack Overflow](https://stackoverflow.com/questions/tagged/microsoftgraph)」に投稿してください。質問やコメントには、必ず "microsoftgraph" とタグを付けてください。

## 投稿 ##

このサンプルに投稿する場合は、[CONTRIBUTING.MD](/CONTRIBUTING.md) を参照してください。

このプロジェクトでは、[Microsoft Open Source Code of Conduct (Microsoft オープン ソース倫理規定)](https://opensource.microsoft.com/codeofconduct/) が採用されています。詳細については、「[Code of Conduct の FAQ](https://opensource.microsoft.com/codeofconduct/faq/)」を参照してください。また、その他の質問やコメントがあれば、[opencode@microsoft.com](mailto:opencode@microsoft.com) までお問い合わせください。
  
## その他の技術情報

- [その他の Microsoft Graph Connect のサンプル](https://github.com/MicrosoftGraph?utf8=%E2%9C%93&query=-Connect)
- [Microsoft Graph](https://developer.microsoft.com/en-us/graph)

## 著作権
Copyright (c) 2019 Microsoft.All rights reserved.
