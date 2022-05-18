using AeroHubBlazorServer.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AeroHubStorageAPI.DbContexts
{
    public class MFINDbContextFactory : IDesignTimeDbContextFactory<MFINContext>
    {
        public MFINContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MFINContext>();
            optionsBuilder.UseSqlServer("Server=tcp:aerohubmfinsqlserver.database.usgovcloudapi.net,1433;Initial Catalog=MFIN;Persist Security Info=False;User ID=scaerohubmfinadmin;Password=&HZc8m8SzA@y38;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            return new MFINContext(optionsBuilder.Options);
        }
    }
}
