using AeroHubBlazorServer.Interfaces;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Components.Forms;

namespace AeroHubBlazorServer.Data
{
    public class AzureBlobStorageService : IStorageInterface
    {
        public readonly BlobContainerClient _containerClient;


        public AzureBlobStorageService(BlobServiceClient blobServiceClient, string containerName)
        {
            _containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        }

        public async Task UploadMetaFile(IBrowserFile browserFile)
        {
            var blobClient = _containerClient.GetBlobClient(Path.GetFileName(browserFile.Name));

            using Stream fs = browserFile.OpenReadStream();
            await blobClient.UploadAsync(fs);


        }

    }
}
