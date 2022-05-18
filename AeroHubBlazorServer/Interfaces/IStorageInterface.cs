using Microsoft.AspNetCore.Components.Forms;

namespace AeroHubBlazorServer.Interfaces
{
    public interface IStorageInterface
    {
        Task UploadMetaFile(IBrowserFile browserFile);

    }
}
