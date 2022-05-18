using AeroHubBlazorServer.DbContexts;
using AeroHubBlazorServer.Models;
using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add Key Vault provider
//var uri = Environment.GetEnvironmentVariable("VaultUri");
//builder.Configuration.AddAzureKeyVault(
//    new Uri(uri),
//    new DefaultAzureCredential());


//Add the Entity Framework Core DBContext
builder.Services.AddDbContext<MFINContext>(_ =>
{
    _.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "MFINDBConnectionString"));
});

builder.Services.AddAzureClients(_ =>
{
    _.AddBlobServiceClient(
        builder.Configuration["AzureStorageConnectionString"]);
});
//Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


// Enables GET of all jobs
app.MapGet("/metafiles", async (MFINContext db) =>
    await db.MetaData.ToListAsync())
    .Produces<List<MetaData>>(StatusCodes.Status200OK)
    .WithName("GetAllMetaFiles");

// Enables GET of a specific job
app.MapGet("/metafiles/{id}",
    async (int id, MFINContext db) =>
        await db.MetaData
                .FirstOrDefaultAsync(_ =>
                    _.id == id)
        is MetaData fileLink
            ? Results.Ok(fileLink)
            : Results.NotFound()
)
.Produces<MetaData>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound)
.WithName("GetMetaFile");

// Enables creation of a new job
app.MapPost("/metafiles/", async (BlobServiceClient blobServiceClient, MetaData metaFile, MFINContext db, IBrowserFile browserFile) =>
{
    try
    {

        BlobClient blobClient = blobServiceClient.GetBlobContainerClient("initialqifin").GetBlobClient(Path.GetFileName(browserFile.Name));
        using Stream fs = browserFile.OpenReadStream();
        await blobClient.UploadAsync(fs);
    }
    catch (Exception)
    {

        throw;
    }


    try
    {
        db.MetaData.Add(metaFile);
        await db.SaveChangesAsync();
        return Results.Created($"/metafiles/{metaFile.id}", metaFile);
    }
    catch (Exception ex)
    {

        throw;
    }
})
    .Produces<MetaData>(StatusCodes.Status201Created)
    .WithName("CreateMetaFile");

app.Run();
