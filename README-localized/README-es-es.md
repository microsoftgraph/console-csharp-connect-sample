---
page_type: sample
products:
- office-outlook
- ms-graph
languages:
- csharp
description: "En este ejemplo se muestra cómo conectar una aplicación de consola de Windows a una cuenta de Microsoft profesional o educativa (Azure Active Directory) o a una cuenta personal (Microsoft) usando la API de Microsoft Graph para enviar un correo electrónico"
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
# Ejemplo de conexión de consola con Microsoft Graph y C#

## Tabla de contenido

* [Introducción](#introduction)
* [Requisitos previos](#prerequisites)
* [Clonar o descargar el repositorio](#cloning-or-downloading-repo)
* [Configurar el inquilino de Azure AD](#configuring-Azure-AD-tenant )
* [Configurar el ejemplo para usar el inquilino de Azure AD](#configuring-sample-to-use-Azure-AD-tenant)
* [Compilar y ejecutar el ejemplo](#build-and-run-sample)
* [Preguntas y comentarios](#questions-and-comments)
* [Colaboradores](#contributing)
* [Recursos adicionales](#additional-resources)

## Introducción

En este ejemplo se muestra cómo conectar una aplicación de consola de Windows a una cuenta de Microsoft profesional o educativa (Azure Active Directory) o a una cuenta personal (Microsoft) usando la API de Microsoft Graph. Se usa la API de Microsoft Graph para recuperar la imagen de perfil de un usuario, cargarla a OneDrive, crear un vínculo de uso compartido y enviar un correo electrónico que contenga la foto como archivo adjunto y el vínculo de uso compartido en el texto. Usa la biblioteca cliente de .NET de Microsoft Graph para trabajar con los datos devueltos por Microsoft Graph. En el ejemplo se usa el punto de conexión de Azure AD v2.0, que permite a los usuarios iniciar sesión con sus cuentas Microsoft personales, o bien con sus cuentas Microsoft profesionales o educativas.

El ejemplo usa la Biblioteca de autenticación de Microsoft (MSAL) para la autenticación.

## Requisitos previos

Este ejemplo necesita lo siguiente:

- [Visual Studio](https://www.visualstudio.com/en-us/downloads) con la versión 7 de C# o superior. 
-  Una cuenta [Microsoft](www.outlook.com) o una [cuenta de Office 365 para empresas](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account).
- Inquilino de Azure Active Directory (Azure AD) Para obtener más información sobre cómo conseguir un inquilino de Azure AD, consulte [cómo obtener un inquilino de Azure AD](https://azure.microsoft.com/en-us/documentation/articles/active-directory-howto-tenant/).

## Cómo ejecutar este ejemplo

<a name="cloning-or-downloading-repo"></a>
### Paso 1: Clonar o descargar el repositorio.

Desde la línea de comandos o shell:

`git clone https://github.com/microsoftgraph/console-csharp-connect-sample`

<a name="configuring-Azure-AD-tenant"></a>
### Paso 2: Configurar el inquilino de Azure AD 

1. Inicie sesión en [Microsoft Azure Portal](https://portal.azure.com) con una cuenta personal, profesional o educativa de Microsoft.
2. Si la cuenta proporciona acceso a más de un inquilino, haga clic en la cuenta en la esquina superior derecha y establezca la sesión del portal en el inquilino de Azure AD deseado (mediante **Cambiar directorio**).
3. En el panel de navegación izquierdo, seleccione el servicio **Azure Active Directory** y, después, seleccione **Registros de aplicaciones
(versión preliminar)**.

#### Registro de la aplicación cliente
![](https://github.com/nicolesigei/console-csharp-connect-sample/blob/master/readme-images/registrations.png)
1. En la página **Registros de aplicaciones**, seleccione **Registrar una aplicación**.
1. En la página **Registros de aplicaciones (versión preliminar)**, seleccione **Registrar una aplicación**.
2. Cuando aparezca la **página Registrar una aplicación**, escriba la información de registro de la aplicación:
- En la sección **Nombre**, escriba un nombre de aplicación significativo, que será el que se muestre a los usuarios. Por ejemplo: `Aplicación de consola para Microsoft Graph` 
- En la sección **Tipos de cuenta admitidos**, seleccione **Cuentas en cualquier directorio de organización y cuentas de Microsoft personales (por ejemplo, Skype, Xbox, Outlook.com)**.
- Seleccione **Registrar** para crear la aplicación.
3. En la página **Información general** de la aplicación, busque el valor **Id. de la aplicación (cliente)** y guárdelo para más tarde. Lo necesitará para configurar el archivo de configuración de Visual Studio para este proyecto.
4. En la lista de páginas de la aplicación, seleccione **Autenticación**.

    - Use *urn:ietf:wg:oauth:2.0:oob* en el cuadro de texto **URI de redirección** y seleccione el **Tipo** como cliente público (móvil y escritorio)
![](https://github.com/nicolesigei/console-csharp-connect-sample/blob/master/readme-images/redirect.png) 
- En los *URI de redirección sugeridos para clientes públicos (móvil, escritorio)*, marque el segundo cuadro para que la aplicación pueda funcionar con las bibliotecas de MSAL utilizadas en la aplicación. (El cuadro debe contener la opción *urn:ietf:wg:oauth:2.0:oob*).
5. En la lista de páginas de la aplicación, seleccione **Permisos de API** 
- Haga clic en el botón **Agregar un permiso** y
- Asegúrese de está seleccionada la pestaña **API de Microsoft**.
- En la sección *API de Microsoft más usadas*, haga clic en **Microsoft Graph**.
- En la sección **Permisos delegados**, asegúrese de que están marcados los permisos correctos: **User.Read**, **Mail.Send** y **Files.ReadWrite**. Use el cuadro de búsqueda si es necesario.
- Seleccione el botón **Agregar permisos**.

<a name="configuring-sample-to-use-Azure-AD-tenant"></a>
### Paso 3: Configurar el ejemplo para usar el inquilino de Azure AD

En los pasos que se indican a continuación, *Id. de cliente* es lo mismo que *Id. de la aplicación* o *Id. de aplicación*.

Abra la solución en Visual Studio para configurar los proyectos.

#### Configurar la aplicación cliente

1. En la carpeta *console-csharp-connect-sample*, cambie el nombre del archivo `appsettings.json.example` a `appsettings.json`.
1. Abra y edite el archivo `appsettings.json` para hacer el siguiente cambio:
    1. Busque la línea en la que `ClientId` se ha establecido como `YOUR_CLIENT_ID_HERE` y reemplace el valor existente con el Id. de aplicación (cliente) de la aplicación `Aplicación de consola para Microsoft Graph` que se había copiado desde Microsoft Azure Portal.

<a name="build-and-run-sample"></a>
### Paso 4: Compilar y ejecutar el ejemplo 

1. Abra la solución del ejemplo en Visual Studio.
2. Pulse F5 para compilar y ejecutar el ejemplo. Esto restaurará las dependencias del paquete de NuGet y abrirá la aplicación de consola.
3. Cuando se le solicite, autentíquese con su cuenta de Microsoft y conceda los permisos que necesita la aplicación.
4. Siga las indicaciones para enviar un mensaje desde su cuenta a su propio correo o al de otra persona.
   
## Preguntas y comentarios

Nos encantaría recibir sus comentarios acerca de la aplicación de consola para la API de Microsoft Graph. Puede enviar sus preguntas y sugerencias en la sección de [Problemas](https://github.com/microsoftgraph/console-csharp-connect-sample/issues) de este repositorio.

Las preguntas sobre el desarrollo de Microsoft Graph en general deben enviarse a [Stack Overflow](https://stackoverflow.com/questions/tagged/microsoftgraph). Asegúrese de que sus preguntas o comentarios estén etiquetados con \[microsoftgraph].

## Colaboradores ##

Si quiere hacer su aportación a este ejemplo, vea [CONTRIBUTING.MD](/CONTRIBUTING.md).

Este proyecto ha adoptado el [Código de conducta de código abierto de Microsoft](https://opensource.microsoft.com/codeofconduct/). Para obtener más información, vea [Preguntas frecuentes sobre el código de conducta](https://opensource.microsoft.com/codeofconduct/faq/) o póngase en contacto con [opencode@microsoft.com](mailto:opencode@microsoft.com) si tiene otras preguntas o comentarios.
  
## Recursos adicionales

- [Otros ejemplos de Microsoft Graph Connect](https://github.com/MicrosoftGraph?utf8=%E2%9C%93&query=-Connect)
- [Microsoft Graph](https://developer.microsoft.com/en-us/graph)

## Derechos de autor
Copyright (c) 2019 Microsoft. Todos los derechos reservados.
