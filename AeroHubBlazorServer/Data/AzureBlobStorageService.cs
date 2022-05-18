using AeroHubBlazorServer.Interfaces;
using Azure.Storage.Blobs;

namespace AeroHubBlazorServer.Data
{
    public class AzureBlobStorageService : IStorageInterface
    {
        private readonly BlobServiceClient blobServiceClient;
        public readonly BlobContainerClient containerClient;
        private BlobClient? blobClient;

        public AzureBlobStorageService(BlobServiceClient blobServiceClient,BlobContainerClient containerClient)
        {
            this.blobServiceClient = blobServiceClient;
            this.containerClient = containerClient;
        }

        public Task<Stream> GetFileStream(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task UploadFile(string localFilePath)
        {
            await blobClient.UploadAsync(localFilePath, true);
        }
    }
}
