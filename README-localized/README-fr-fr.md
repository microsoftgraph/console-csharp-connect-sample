---
page_type: sample
products:
- office-outlook
- ms-graph
languages:
- csharp
description: "Cet exemple présente la connexion entre une application de console Windows et un compte professionnel, scolaire (Azure Active Directory) ou personnel (Microsoft) utilisant l’API Microsoft Graph pour envoyer un e-mail."
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
# Exemple de connexion avec la console Microsoft Graph C#

## Table des matières

* [Introduction](#introduction)
* [Conditions préalables](#prerequisites)
* [Clonage ou téléchargement de ce référentiel](#cloning-or-downloading-repo)
* [Configuration de votre client Azure AD](#configuring-Azure-AD-tenant )
* [Configuration de l’échantillon pour utiliser votre client Azure AD](#configuring-sample-to-use-Azure-AD-tenant)
* [Créer et exécuter l’exemple](#build-and-run-sample)
* [Questions et commentaires](#questions-and-comments)
* [Contribution](#contributing)
* [Ressources supplémentaires](#additional-resources)

## Introduction

Cet exemple montre comment connecter une application de console Windows à un compte professionnel ou scolaire Microsoft (Azure Active Directory) ou un compte personnel (Microsoft) à l’aide de l’API Microsoft Graph. Il utilise l’API Microsoft Graph pour récupérer l’image de profil d’un utilisateur, charger l’image sur OneDrive, créer un lien de partage et envoyer un courrier e-mail contenant la photo sous forme de pièce jointe et le lien de partage dans son texte. Il utilise la bibliothèque cliente Microsoft Graph .NET pour exploiter les données renvoyées par Microsoft Graph. L’exemple utilise le point de terminaison Azure AD v 2.0, qui permet aux utilisateurs de se connecter avec leurs comptes Microsoft personnels, professionnels ou scolaires.

L’exemple utilise la Bibliothèque d’authentification Microsoft (MSAL) pour l’authentification.

## Conditions préalables

Cet exemple nécessite les éléments suivants :

- [Visual Studio](https://www.visualstudio.com/en-us/downloads) avec C# version 7 et les versions ultérieures. 
-  Soit un compte [Microsoft](www.outlook.com), soit un [compte Office 365 pour entreprise](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account).
- Un client Azure Active Directory (Azure AD). Pour plus d’informations sur la façon d’obtenir un client Azure AD, voir [Obtention d’un client Azure AD](https://azure.microsoft.com/en-us/documentation/articles/active-directory-howto-tenant/).

## Exécution de cet exemple

<a name="cloning-or-downloading-repo"></a>
### Étape 1 : Clonage ou téléchargement de ce référentiel.

À partir de votre shell ou de la ligne de commande :

`git clone https://github.com/microsoftgraph/console-csharp-connect-sample`

<a name="configuring-Azure-AD-tenant"></a>
### Étape 2 : [Configuration de votre client Azure AD](#configuring-Azure-AD-tenant )
 

1. Connectez-vous au [portail Microsoft Azure](https://portal.azure.com) à l’aide d’un compte professionnel ou scolaire, ou d’un compte Microsoft personnel.
2. Si votre compte vous propose un accès à plusieurs locataires, sélectionnez votre compte en haut à droite et définissez votre session de portail sur le client Azure AD souhaité (à l’aide de **Changer de répertoire**).
3. Dans le volet de navigation gauche, sélectionnez le service **Azure Active Directory**, puis sélectionnez **Inscriptions d’applications (préversion)**.

#### Inscription de l’application cliente
![](https://github.com/nicolesigei/console-csharp-connect-sample/blob/master/readme-images/registrations.png)
1. À la page **Inscriptions des applications**, sélectionnez **Inscrire une application**.
1. À la page **Inscriptions des applications (préversion)**, sélectionnez **Inscrire une application**.
2. Lorsque la **page Inscrire une application** s’affiche, entrez les informations relatives à l’inscription de votre application :
- dans la **section Nom**, entrez un nom d’application significatif qui sera présenté aux utilisateurs de l’application, par exemple `Application console pour Microsoft Graph`
- dans la section **Types de comptes pris en charge**, sélectionnez **Comptes dans un annuaire organisationnel et comptes personnels Microsoft (par exemple, Skype, Xbox, Outlook.com)**.
- Sélectionnez **Inscrire** pour créer l’application.
3. Sur la page **Vue d’ensemble** de l’application, notez la valeur **ID d’application (client)** et conservez-la pour plus tard. Vous en aurez besoin pour paramétrer le fichier de configuration de Visual Studio pour ce projet.
4. Dans la liste des pages de l’application, sélectionnez **Authentification**.

    - Utilisez *urn:ietf:wg:oauth:2.0:oob* dans la zone de texte **Redirect URI** et sélectionnez le **Type** en tant que client public (mobile et ordinateur de bureau)
![](https://github.com/nicolesigei/console-csharp-connect-sample/blob/master/readme-images/redirect.png)
- Dans la boîte de dialogue *URI de redirection suggérés pour les clients publics (mobile, bureau)*, cochez la deuxième zone pour que l’application puisse utiliser les bibliothèques MSAL utilisées dans l’application. (La zone doit contenir l’option *urn:ietf:wg:oauth:2.0:oob*).
5. Dans la liste des pages de l’application, sélectionnez **Autorisations de l’API**
- Cliquez sur le bouton **Ajouter une autorisation**, puis :
- assurez-vous que l’onglet **API Microsoft** est sélectionné.
- Dans la section *API Microsoft couramment utilisées*, cliquez sur **Microsoft Graph**. Dans la section **Autorisations déléguées**, assurez
-vous que les autorisations appropriées sont cochées : **User.Read**, **Mail.Send** et **Files.ReadWrite**. Utilisez la zone de recherche, le cas échéant.
- Sélectionnez le bouton **Ajouter des autorisations**.

<a name="configuring-sample-to-use-Azure-AD-tenant"></a>
### Étape 3 : Configuration de l’échantillon pour utiliser votre client Azure AD

Dans les étapes ci-dessous, *ID client* est identique à *ID de l’application*.**

Ouvrez la solution dans Visual Studio pour configurer les projets.

#### Configurer le projet client

1. Dans le dossier *console-csharp-connect-sample*, renommez le fichier `appsettings.json.example` en `appsettings.json`.
1. Ouvrez et modifiez le fichier `appSettings.json` pour apporter la modification suivante :
    1. Recherchez la ligne dans laquelle `ClientId` est défini comme `YOUR_CLIENT_ID_HERE` et remplacez la valeur existante par l’ID (client) de `l’application console pour Microsoft Graph` copié à partir du portail Azure.

<a name="build-and-run-sample"></a>
### Étape 4 : Créer et exécuter l’exemple 

1. Ouvrez l’exemple de solution dans Visual Studio.
2. Appuyez sur F5 pour créer et exécuter l’exemple. Cela entraîne la restauration des dépendances du package NuGet et l’ouverture de l’application de la console.
3. À l’invite, authentifiez-vous avec votre compte Microsoft et acceptez les autorisations requises par l’application.
4. Suivez les invites pour envoyer un message de votre compte à vous-même ou à un autre utilisateur.
   
## Questions et commentaires

Nous serions ravis de connaître votre opinion sur l'application de console API Microsoft Graph. Vous pouvez nous faire part de vos questions et suggestions dans la rubrique [Problèmes](https://github.com/microsoftgraph/console-csharp-connect-sample/issues) de ce référentiel.

Les questions générales sur le développement de Microsoft Graph doivent être publiées sur [Stack Overflow](https://stackoverflow.com/questions/tagged/microsoftgraph). Veillez à poser vos questions ou à rédiger vos commentaires en utilisant la balise \[microsoftgraph].

## Contribution ##

Si vous souhaitez contribuer à cet exemple, voir [CONTRIBUTING.MD](/CONTRIBUTING.md).

Ce projet a adopté le [code de conduite Open Source de Microsoft](https://opensource.microsoft.com/codeofconduct/). Pour en savoir plus, reportez-vous à la [FAQ relative au code de conduite](https://opensource.microsoft.com/codeofconduct/faq/) ou contactez [opencode@microsoft.com](mailto:opencode@microsoft.com) pour toute question ou tout commentaire.
  
## Ressources supplémentaires

- [Autres exemples de connexion avec Microsoft Graph](https://github.com/MicrosoftGraph?utf8=%E2%9C%93&query=-Connect)
- [Microsoft Graph](https://developer.microsoft.com/en-us/graph)

## Copyright
Copyright (c) 2019 Microsoft. Tous droits réservés.
