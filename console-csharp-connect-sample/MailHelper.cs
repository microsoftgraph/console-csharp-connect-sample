//Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
//See LICENSE in the project root for license information.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using console_csharp_connect_sample.Helpers;
using Microsoft.Graph;

namespace console_csharp_connect_sample
{
	class MailHelper
	{
		private static GraphServiceClient _graphServiceClient = null;

		public MailHelper(GraphServiceClient graphServiceClient)
		{
			_graphServiceClient = graphServiceClient;
		}

		/// <summary>
		/// Compose and send a new email.
		/// </summary>
		/// <param name="subject">The subject line of the email.</param>
		/// <param name="bodyContent">The body of the email.</param>
		/// <param name="recipients">A semicolon-separated list of email addresses.</param>
		/// <returns></returns>
		public static async Task ComposeAndSendMailAsync(string subject,
															string bodyContent,
															string recipients)
		{

			// Get current user photo
			Stream photoStream = await GetCurrentUserPhotoStreamAsync();


			// If the user doesn't have a photo, or if the user account is MSA, we use a default photo

			if (photoStream == null)
			{
				photoStream = new FileStream("test.jpg", FileMode.Open);
			}

			MemoryStream photoStreamMS = new MemoryStream();
			// Copy stream to MemoryStream object so that it can be converted to byte array.
			await photoStream.CopyToAsync(photoStreamMS);
			photoStream.Close();

			DriveItem photoFile = await UploadFileToOneDriveAsync(photoStreamMS.ToArray());

			

			MessageAttachmentsCollectionPage attachments = new MessageAttachmentsCollectionPage();
			attachments.Add(new FileAttachment
			{
				ODataType = "#microsoft.graph.fileAttachment",
				ContentBytes = photoStreamMS.ToArray(),
				ContentType = "image/png",
				Name = "me.png"
			});

			// Get the sharing link and insert it into the message body.
			Task<Permission> sharingLinkTask = GetSharingLinkAsync(photoFile.Id);
			sharingLinkTask.Wait();

			Permission sharingLink = sharingLinkTask.Result;

			string bodyContentWithSharingLink = String.Format(bodyContent, sharingLink.Link.WebUrl);

			// Prepare the recipient list
			string[] splitter = { ";" };
			var splitRecipientsString = recipients.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
			List<Recipient> recipientList = new List<Recipient>();

			foreach (string recipient in splitRecipientsString)
			{
				recipientList.Add(new Recipient { EmailAddress = new EmailAddress { Address = recipient.Trim() } });
			}

			try
			{
				var email = new Message
				{
					Body = new ItemBody
					{
						Content = bodyContentWithSharingLink,
						ContentType = BodyType.Html,
					},
					Subject = subject,
					ToRecipients = recipientList,
					Attachments = attachments
				};

				try
				{
					await _graphServiceClient.Me.SendMail(email, true).Request().PostAsync();
				}
				catch (ServiceException exception)
				{
					throw new Exception("We could not send the message: " + exception.Error == null ? "No error message returned." : exception.Error.Message);
				}
			}

			catch (Exception e)
			{
				throw new Exception("We could not send the message: " + e.Message);
			}
		}


		// Gets the stream content of the signed-in user's photo. 
		// This snippet doesn't work with consumer accounts.
		public static async Task<Stream> GetCurrentUserPhotoStreamAsync()
		{
			Stream currentUserPhotoStream = null;

			try
			{
				currentUserPhotoStream = await _graphServiceClient.Me.Photo.Content.Request().GetAsync();

			}
			// If the user account is MSA (not work or school), the service will throw an exception.
			catch (Exception)
			{
				return null;
			}

			return currentUserPhotoStream;

		}

		// Uploads the specified file to the user's root OneDrive directory.
		public static async Task<DriveItem> UploadFileToOneDriveAsync(byte[] file)
		{
			DriveItem uploadedFile = null;

			try
			{
				MemoryStream fileStream = new MemoryStream(file);
				uploadedFile = await _graphServiceClient.Me.Drive.Root.ItemWithPath("me.png").Content.Request().PutAsync<DriveItem>(fileStream);
			}
			catch (ServiceException)
			{
				return null;
			}

			return uploadedFile;
		}

		public static async Task<Permission> GetSharingLinkAsync(string Id)
		{
			Permission permission = null;

			try
			{
				permission = await _graphServiceClient.Me.Drive.Items[Id].CreateLink("view").Request().PostAsync();
			}
			catch (ServiceException)
			{
				return null;
			}

			return permission;
		}
	}
}
