---
page_type: sample
products:
- office-outlook
- ms-graph
languages:
- csharp
description: "Este exemplo mostra como conectar um aplicativo de console do Windows a uma conta corporativa ou de estudante da Microsoft ou a uma conta pessoal (Microsoft) usando a API do Microsoft para enviar um email."
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
# Exemplo de conexão de aplicativo de console C# usando o Microsoft Graph 

## Sumário

* [Introdução](#introduction)
* [Pré-requisitos](#prerequisites)
* [Clonar ou baixar este repositório](#cloning-or-downloading-repo)
* [Configurar o seu locatário do Azure AD](#configuring-Azure-AD-tenant )
* [Configurar o exemplo para usaro seu locatário do Azure AD](#configuring-sample-to-use-Azure-AD-tenant)
* [Criar e executar o exemplo](#build-and-run-sample)
* [Perguntas e comentários](#questions-and-comments)
* [Colaboração](#contributing)
* [Recursos adicionais](#additional-resources)

## Introdução

Este exemplo mostra como conectar um aplicativo de console do Windows a uma conta corporativa ou de estudante da Microsoft (Azure Active Directory) ou uma conta pessoal (Microsoft) usando a API do Microsoft Graph. Ele usa a API do Microsoft Graph para recuperar a imagem de perfil de um usuário, carregar a imagem para o OneDrive, criar um link de compartilhamento e enviar um email que contenha a foto como um anexo e o link de compartilhamento no texto. O exemplo usa a Biblioteca de Clientes do Microsoft Graph para .NET para trabalhar com dados retornados pelo Microsoft Graph. O exemplo utiliza o ponto de extremidade do Azure AD versão 2.0, que permite que os usuários entrem com a conta pessoal, corporativa ou de estudante da Microsoft.

O exemplo usa a Biblioteca de Autenticação da Microsoft (MSAL) para autenticação.

## Pré-requisitos

Requisitos para o exemplo são os seguintes:

- [ Visual Studio](https://www.visualstudio.com/en-us/downloads) com a versão C# 7 e posterior. 
-  Uma conta [Microsoft](www.outlook.com) ou uma conta [Office 365 for business](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account).
- Um locatário do Active Directory do Azure (Azure AD). Para obter mais informações sobre como obter um locatário do Azure AD, confira [como obter um locatário do Azure AD](https://azure.microsoft.com/en-us/documentation/articles/active-directory-howto-tenant/).

## Como executar esse exemplo

<a name="cloning-or-downloading-repo"></a>
### Etapa 1: Clone ou baixe este repositório

A partir de seu shell ou linha de comando:

`git clone https://github.com/microsoftgraph/console-csharp-connect-sample`

<a name="configuring-Azure-AD-tenant"></a>
### Etapa 2: Configurar o seu locatário do Azure AD 

1. Entrar no [portal do Azure](https://portal.azure.com)usando uma conta corporativa, de estudante ou uma conta Microsoft pessoal.
2. Se sua conta permitir o acesso a mais de um locatário, clique na sua conta no canto superior direito e configure sua sessão do portal ao locatário do Azure AD desejado (usando **Mudar Diretório**).
3. No painel de navegação à esquerda, selecione o serviço **Azure Active Directory**e, em seguida, selecione **Registros de aplicativo (Visualização)**.

#### Registro do aplicativo cliente
![](https://github.com/nicolesigei/console-csharp-connect-sample/blob/master/readme-images/registrations.png)
1. Na página **Registros de aplicativo**, selecione **Registrar um aplicativo**.
1. Na página **Registros de aplicativo (Visualização)**, selecione **Registrar um aplicativo**.
2. Quando a **página Registrar um aplicativo** aparecer, insira as informações de registro do seu aplicativo:
- Na seção **Nome**, insira a nome significativo para o aplicativo que irá ser exibido para os usuários do aplicativo, por exemplo `Aplicativo de console para o Microsoft Graph`
- Na seção **Tipos de contas com suporte**, selecione**Contas em qualquer diretório organizacional ou contas pessoais da Microsoft (por exemplo, Skype, Xbox, Outlook.com)**.
- Selecione **Registrar** para criar o aplicativo.
3. Na página **Visão geral** do aplicativo, encontre o valor de **ID do aplicativo (cliente)** e guarde-o para usar mais tarde. Será necessário configurar o arquivo de configuração do Visual Studio para este projeto.
4. Na lista de páginas do aplicativo, selecione **Autenticação**

    - Use *urn:ietf:wg:oauth:2.0:oob* na caixa de texto **Redirecionar URI** e selecione o **Type** como Cliente Público (celular e computador)
![](https://github.com/nicolesigei/console-csharp-connect-sample/blob/master/readme-images/redirect.png) 
- Em *URIs de redirecionamento sugeridas para clientes públicos(celular,computador)*, marque a segunda caixa para que o aplicativo possa trabalhar com a biblioteca do MSAL usada no aplicativo. (A caixa dever conter a opção *urn:ietf:wg:oauth:2.0:oob*).
5. Na lista de páginas do aplicativo, selecione **API de permissões**
- Clique no botão **Adicionar uma permissão** e, em seguida,
- Verifique se a guia **APIs da Microsoft** está selecionada.
-Na seção *APIs mais usadas da Microsoft*, clique em **Microsoft Graph**.
-Na seção **Permissões delegadas**, certifique-se de que as permissões corretas estejam marcadas: **User.Read**, **Mail.Send** and **Files.ReadWrite**. Use a caixa de pesquisa, se necessário.
- Marque o botão **Adicionar permissões**.

<a name="configuring-sample-to-use-Azure-AD-tenant"></a>
### Etapa 3: Configurar o exemplo para usaro seu locatário do Azure AD

Nas etapas abaixo, *ID de cliente* é o mesmo que *ID de aplicativo* ou *ID de app*.

Abra a solução no Visual Studio para configurar os projetos.

#### Configurar o projeto cliente

1. Na pasta *console-csharp-connect-sample*, renomeie o arquivo `appsettings.json.example` para `appsettings.json`
1. Abra e edite o arquivo `appsettings.json` para fazer a seguinte alteração
    1. Localize a linha em que `ClientId` está definida como `YOUR_CLIENT_ID_HERE` e substitua o valor existente pelo ID de aplicativo (cliente) ` Aplicativo de Console para o Microsoft Graph` copiado do portal do Azure.

<a name="build-and-run-sample"></a>
### Etapa 4: Criar e executar o exemplo 

1. Abra a solução de exemplo no Visual Studio.
2. Pressione F5 para criar e executar o exemplo. Isso restaurará as dependências do pacote NuGet e abrirá o aplicativo de console.
3. Quando solicitado, autentique com a conta da Microsoft e concorde com as permissões necessárias para o aplicativo.
4. Siga as instruções para enviar uma mensagem a partir da sua conta para você mesmo ou outra pessoa.
   
## Perguntas e comentários

Gostaríamos muito de saber sua opinião sobre ao Aplicativo de Console da API do Microsoft Graph. Você pode enviar perguntas e sugestões na seção [Problemas](https://github.com/microsoftgraph/console-csharp-connect-sample/issues) deste repositório.

As perguntas sobre o desenvolvimento do Microsoft Graph em geral devem ser postadas no [Stack Overflow](https://stackoverflow.com/questions/tagged/microsoftgraph). Não deixe de marcar as perguntas ou comentários com \[microsoftgraph].

## Colaboração ##

Se quiser contribuir para esse exemplo, confira [CONTRIBUTING.MD](/CONTRIBUTING.md).

Este projeto adotou o [Código de Conduta de Código Aberto da Microsoft](https://opensource.microsoft.com/codeofconduct/).  Para saber mais, confira as [Perguntas frequentes sobre o Código de Conduta](https://opensource.microsoft.com/codeofconduct/faq/) ou entre em contato pelo [opencode@microsoft.com](mailto:opencode@microsoft.com) se tiver outras dúvidas ou comentários.
  
## Recursos adicionais

- [Outros exemplos de conexão usando o Microsoft Graph](https://github.com/MicrosoftGraph?utf8=%E2%9C%93&query=-Connect)
- [Microsoft Graph](https://developer.microsoft.com/en-us/graph)

## Direitos autorais
Copyright (c) 2019 Microsoft. Todos os direitos reservados.
