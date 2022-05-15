using AeroHubBlazorServer.DbContexts;
using Microsoft.Extensions.Azure;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using AeroHubBlazorServer.Models;
using Azure.Storage.Blobs;

var builder = WebApplication.CreateBuilder(args);

// Add Key Vault provider
var uri = Environment.GetEnvironmentVariable("VaultUri");
builder.Configuration.AddAzureKeyVault(
    new Uri(uri),
    new DefaultAzureCredential());

// Add the Entity Framework Core DBContext
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

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

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
app.MapPost("/metafiles/", async (BlobServiceClient blobServiceClient, MetaData metaFile, MFINContext db,string localFilePath) =>
{
    BlobClient blobClient = blobServiceClient.GetBlobContainerClient("initialqifin").GetBlobClient(localFilePath);

    db.MetaData.Add(metaFile);
    await db.SaveChangesAsync();
    return Results.Created($"/metafiles/{metaFile.id}", metaFile);
})
    .Produces<MetaData>(StatusCodes.Status201Created)
    .WithName("CreateMetaFile");



app.Run();
