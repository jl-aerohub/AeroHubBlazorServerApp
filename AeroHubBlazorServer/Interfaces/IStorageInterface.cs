using Azure;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace AeroHubBlazorServer.Interfaces
{
    public interface IStorageInterface
    {
        public Task<string?> UploadMetaFile(IBrowserFile browserFile);

    }
}
