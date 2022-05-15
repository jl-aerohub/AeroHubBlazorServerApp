namespace AeroHubBlazorServer.Interfaces
{
    public interface IStorageInterface
    {
        Task UploadFile(string fileName);
        Task<Stream> GetFileStream(string fileName);

    }
}
