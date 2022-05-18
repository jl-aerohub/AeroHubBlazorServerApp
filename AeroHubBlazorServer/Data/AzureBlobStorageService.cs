using AeroHubBlazorServer.Interfaces;
using AeroHubBlazorServer.Models;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Components.Forms;
using MiNET.Utils;

namespace AeroHubBlazorServer.Data
{
    public class AzureBlobStorageService : IStorageInterface
    {
        public readonly BlobContainerClient _containerClient;


        public AzureBlobStorageService(BlobServiceClient blobServiceClient, string containerName)
        {
            _containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        }

        public async Task<string?> UploadMetaFile(IBrowserFile browserFile)
        {
            string? uri = null;
            var blobClient = _containerClient.GetBlobClient(Path.GetFileName(browserFile.Name));
            try
            {
                using Stream fs = browserFile.OpenReadStream();
                await blobClient.UploadAsync(fs);
                uri = blobClient.Uri.AbsoluteUri;
            }
            catch (RequestFailedException ex)
            {
               //log the error
            }
            catch(IOException ex)
            {
                //logg this one too
            }

            return uri;
        }
    }
}
