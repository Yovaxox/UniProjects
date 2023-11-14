using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace LindaSonrisa.Models
{
    public class GoogleApiDrive
    {
        protected static string[] scopes = { DriveService.Scope.Drive };
        protected readonly UserCredential credential;
        static string ApplicationName = "LindaSonrisa";
        protected readonly DriveService service;
        protected readonly FileExtensionContentTypeProvider fileExtensionProvider;

        public GoogleApiDrive()
        {
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token";

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "YOUR_GMAIL_EMAIL", // use a const or read it from a config file
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;

                fileExtensionProvider = new FileExtensionContentTypeProvider();
            }

            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public IList<Google.Apis.Drive.v3.Data.File> GetFiles(string id = "root")
        {
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 100;
            listRequest.Fields = "nextPageToken, files(id, name, parents, createdTime, modifiedTime, mimeType, permissions, webViewLink)";
            listRequest.Q = $"'{id}' in parents";

            return listRequest.Execute().Files;
        }

        public async Task<Google.Apis.Drive.v3.Data.File> UploadFile(IFormFile file, string name)
        {
            var mimeType = file.ContentType;

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name,
                MimeType = mimeType,
            };

            FilesResource.CreateMediaUpload request;
            using (var stream = file.OpenReadStream())
            {
                request = service.Files.Create(
                    fileMetadata, stream, mimeType);
                request.Fields = "id, name, parents, createdTime, modifiedTime, mimeType, thumbnailLink";
                await request.UploadAsync();
            }

            return request.ResponseBody;
        }

        public async Task<Google.Apis.Drive.v3.Data.File> UploadFileInFolder(IFormFile file, string name, string folderId)
        {
            var mimeType = file.ContentType;

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name,
                MimeType = mimeType,
                Parents = new[] { folderId }
            };

            FilesResource.CreateMediaUpload request;
            using (var stream = file.OpenReadStream())
            {
                request = service.Files.Create(
                    fileMetadata, stream, mimeType);
                request.Fields = "id, name, parents, createdTime, modifiedTime, mimeType, thumbnailLink";
                await request.UploadAsync();
            }

            string fileId = request.ResponseBody.Id;

            Permission permission = new Permission()
            {
                Role = "reader",
                Type = "anyone",
            };

            service.Permissions.Create(permission, fileId).Execute();


            return request.ResponseBody;
        }

        public Google.Apis.Drive.v3.Data.File GetFolder(string name)
        {
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.Q = "mimeType='application/vnd.google-apps.folder'";
            listRequest.Fields = "nextPageToken, files(*)";

            IList<Google.Apis.Drive.v3.Data.File> folders = listRequest.Execute().Files;

            Google.Apis.Drive.v3.Data.File folder = folders.Where(x => x.Name == name).FirstOrDefault();

            return folder;
        }

        public Google.Apis.Drive.v3.Data.File CreateFolder(string name, string id = "root")
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new[] { id }
            };

            var request = service.Files.Create(fileMetadata);
            request.Fields = "id, name, parents, createdTime, modifiedTime, mimeType";

            return request.Execute();
        }

        public void DeleteFile(string id)
        {
            try
            {
                service.Files.Delete(id).Execute();
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public Google.Apis.Drive.v3.Data.File GetFile(string id)
        {
            return service.Files.Get(id).Execute(); 
        }

        public async Task<Stream> DownloadFile(string fileId)
        {
            Stream outputstream = new MemoryStream();
            var request = service.Files.Get(fileId);

            await request.DownloadAsync(outputstream);

            outputstream.Position = 0;

            return outputstream;
        }
    }    
}